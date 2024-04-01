using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Properties", menuName = "New weapon properties")]
public class WeaponProperties : ScriptableObject
{
    public float projectileSpeed;
    public float projectileSize;
    public float timeBetweenShots;
    public GameObject pickup_prefab;
    public AudioClip projectileSfx;
    public GameObject ammo_instantiate_object;
}
