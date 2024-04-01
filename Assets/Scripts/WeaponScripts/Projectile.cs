using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void Start() 
    {
        Destroy(gameObject, 7f);    
    }
}
