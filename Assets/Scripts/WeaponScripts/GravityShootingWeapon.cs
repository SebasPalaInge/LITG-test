using UnityEngine;

public class GravityShootingWeapon : Weapon
{
    public override void ShootWeapon()
    {
        base.ShootWeapon();
        this.readyToShoot = false;

        GameObject plasmaShot = Instantiate(currentProperties.ammo_instantiate_object, shootingPoint.position, currentProperties.ammo_instantiate_object.transform.rotation);
        PlaySound(currentProperties.projectileSfx);
        LeanTween.scale(plasmaShot, new Vector3(currentProperties.projectileSize, currentProperties.projectileSize, currentProperties.projectileSize), 0.8f).setEaseOutCirc();
        plasmaShot.GetComponent<Rigidbody>().velocity = currentProperties.projectileSpeed * shootingPoint.forward;

        Invoke("ResetShoot", currentProperties.timeBetweenShots);
    }
}
