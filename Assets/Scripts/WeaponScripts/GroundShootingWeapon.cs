using UnityEngine;

public class GroundShootingWeapon : Weapon
{
    public override void ShootWeapon()
    {
        base.ShootWeapon();
        this.readyToShoot = false;
        
        GameObject discProj = Instantiate(currentProperties.ammo_instantiate_object, shootingPoint.position, currentProperties.ammo_instantiate_object.transform.rotation);
        PlaySound(currentProperties.projectileSfx);
        discProj.transform.localScale = new Vector3(currentProperties.projectileSize, 0.01f, currentProperties.projectileSize);
        discProj.GetComponent<Rigidbody>().velocity = currentProperties.projectileSpeed * shootingPoint.forward;

        Invoke("ResetShoot", currentProperties.timeBetweenShots);
    }
}
