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
    public GameObject RaycasterObj;
    public GameObject pickUpUI;
    public Transform player, weaponHolder;
    public Camera playerCamera;

    public float pickUpRange = 100f;
    public float dropForwardForce, dropUpwardForce;

    public bool isEquipped;
    public bool slotFull;

    public bool isRaycasted;
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
        ShootCurrentWeapon();
        if(isCooldown)
        {
            ApplyCooldown();
        }
        SwayWeapon();
        PickupLogic();
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
        isRaycasted = Physics.Raycast(RaycasterObj.transform.position, RaycasterObj.transform.forward, out RaycastHit hit, pickUpRange);
        if(!isEquipped)
        {
            if (isRaycasted)
            {
                //Debug.Log(hit.transform.gameObject.name);
                if(hit.transform.tag.Equals("PickUpWeapon"))
                {    
                    Debug.Log("Detected");
                    pickUpUI.SetActive(true);
                    if(inputManager.input.ShootingActions.PickUpWeapon.WasPressedThisFrame() && !slotFull)
                        PickUpWeapon(hit.transform.gameObject);
                }
                else
                {
                    pickUpUI.SetActive(false);
                }
            }
            else
            {
                pickUpUI.SetActive(false);
            }
        }
        
        if(isEquipped && inputManager.input.ShootingActions.DropWeapon.WasPressedThisFrame())
            DropWeapon(); 
    }

    private void PickUpWeapon(GameObject weaponToBeEquipped)
    {
        isEquipped = true;
        slotFull = true;
        pickUpUI.SetActive(false);

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

    private void OnDrawGizmosSelected() 
    {
        if(!isRaycasted)
        Gizmos.color = Color.red;
        else
        Gizmos.color = Color.green;

        Vector3 dir = RaycasterObj.transform.TransformDirection(Vector3.forward) * pickUpRange;
        Gizmos.DrawRay(RaycasterObj.transform.position, dir);   
    }
}
