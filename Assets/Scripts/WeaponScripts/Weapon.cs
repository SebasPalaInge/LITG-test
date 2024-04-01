using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform shootingPoint;
    public Animator weaponAnimController;
    public AudioSource audioSrc;
    public WeaponProperties currentProperties;
    public bool readyToShoot = true;

    public virtual void ShootWeapon()
    {
        //Debug.Log("Weapon is fired");
    }

    protected void ResetShoot()
    {
        readyToShoot = true;
    }

    public void PlaySound(AudioClip _sfx)
    {
        audioSrc.PlayOneShot(_sfx);
    }
}
