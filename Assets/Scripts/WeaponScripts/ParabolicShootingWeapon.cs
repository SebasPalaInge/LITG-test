using UnityEngine;

public class ParabolicShootingWeapon : Weapon
{
    public override void ShootWeapon()
    {
        base.ShootWeapon();
        this.readyToShoot = false;
        
        GameObject grenadeProj = Instantiate(currentProperties.ammo_instantiate_object, shootingPoint.position, currentProperties.ammo_instantiate_object.transform.rotation);
        PlaySound(currentProperties.projectileSfx);
        grenadeProj.transform.localScale = new Vector3(currentProperties.projectileSize, currentProperties.projectileSize, currentProperties.projectileSize);
        grenadeProj.GetComponent<Rigidbody>().velocity = currentProperties.projectileSpeed * shootingPoint.forward;

        Invoke("ResetShoot", currentProperties.timeBetweenShots);
    }
}
