using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShootingWeapon: MonoBehaviour
{
    public Weapon currentWeapon = null;
    private bool isCooldown = false;
    private float cooldownTimer = 0.0f;
    private float cooldownTime = 0f;
    [SerializeField] private Image shootCooldown;
    [SerializeField] private InputManager inputManager;

    [Header("WeaponPickup")]
    public TextMeshProUGUI pickUpText;
    public Transform player, weaponHolder;
    public Camera playerCamera;

    public float pickUpRange = 100f;
    public float dropForwardForce, dropUpwardForce;

    public bool isEquipped;
    public bool slotFull;

    private bool isRaycasted;
    [Header("Sway variables")]
    [SerializeField] private float smooth;
    [SerializeField] private float swayMultiplier;

    [Header("Audio")]
    public AudioSource audioSrc;
    public AudioClip equipGun_sfx;
    public AudioClip dropGun_sfx;
    

    private void Start() 
    {
        shootCooldown.fillAmount = 0.0f;    
    }

    private void Update() 
    {
        PickupLogic();
        ShootCurrentWeapon();
        if(isCooldown)
        {
            ApplyCooldown();
        }
        SwayWeapon();
    }

    private void SwayWeapon()
    {
        float mouseX = inputManager.input.CameraActions.MouseX.ReadValue<float>() * swayMultiplier;
        float mouseY = inputManager.input.CameraActions.MouseY.ReadValue<float>() * swayMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }

    private void PickupLogic()
    {
        isRaycasted = Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, pickUpRange);
        if(!isEquipped)
        {
            if (isRaycasted)
            {
                if(hit.transform.Equals("PickUpWeapon"))
                {    
                    pickUpText.gameObject.SetActive(true);
                    if(inputManager.input.ShootingActions.PickUpWeapon.WasPressedThisFrame() && !slotFull)
                        PickUpWeapon(hit.transform.gameObject);
                }
            }
            else
            {
                pickUpText.gameObject.SetActive(false);
            }
        }
        
        if(isEquipped && inputManager.input.ShootingActions.DropWeapon.WasPressedThisFrame())
            DropWeapon(); 
    }

    private void PickUpWeapon(GameObject weaponToBeEquipped)
    {
        isEquipped = true;
        slotFull = true;
        pickUpText.gameObject.SetActive(false);

        WeaponPickup newWeapon = weaponToBeEquipped.GetComponent<WeaponPickup>();
        Instantiate(newWeapon.weaponPrefab, weaponHolder);
        audioSrc.PlayOneShot(equipGun_sfx);
        Weapon weaponScript = weaponHolder.GetComponentInChildren<Weapon>();
        currentWeapon = weaponScript;
        newWeapon.DestroyPickupAfterEquip();
    }

    private void DropWeapon()
    {
        isEquipped = false;
        slotFull = false;

        GameObject pickupObj = Instantiate(currentWeapon.currentProperties.pickup_prefab);
        pickupObj.transform.position = playerCamera.transform.position;
        Rigidbody piObjRb = pickupObj.GetComponent<Rigidbody>();
        audioSrc.PlayOneShot(dropGun_sfx);

        piObjRb.velocity = player.GetComponent<CharacterController>().velocity;
        piObjRb.AddForce(playerCamera.transform.forward * dropForwardForce, ForceMode.Impulse);
        piObjRb.AddForce(playerCamera.transform.up * dropUpwardForce, ForceMode.Impulse);
        float rndm = Random.Range(-1f, 1f);
        piObjRb.AddTorque(new Vector3(rndm, rndm, rndm) * 10f);

        currentWeapon = null;
        Destroy(weaponHolder.transform.GetChild(0).gameObject);
    }

    private void ApplyCooldown()
    {
        cooldownTimer -= Time.deltaTime;
        if(cooldownTimer < 0.0f)
        {
            isCooldown = false;
            shootCooldown.fillAmount = 0.0f;
        }
        else
        {
            shootCooldown.fillAmount = cooldownTimer / cooldownTime;
        }
    }

    public void FillAmmoIcon()
    {
        if(!isCooldown)
        {
            isCooldown = true;
            cooldownTimer = cooldownTime;
        }
    }

    public void ShootCurrentWeapon()
    {
        if(currentWeapon != null)
        {
            if(inputManager.input.ShootingActions.ShootButton.WasPressedThisFrame() && currentWeapon.readyToShoot)
            {
                currentWeapon.ShootWeapon();
                cooldownTime = currentWeapon.currentProperties.timeBetweenShots;
                FillAmmoIcon();
            }
        }
        else
        {
            //Debug.Log("No gun equipped");
        }
    }
}
