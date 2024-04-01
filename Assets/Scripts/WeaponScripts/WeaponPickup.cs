using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponPrefab;
    public Rigidbody rb;
    public BoxCollider coll;

    public void DestroyPickupAfterEquip()
    {
        Destroy(this.gameObject);
    }
}
