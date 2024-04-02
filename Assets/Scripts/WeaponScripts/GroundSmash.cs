using UnityEngine;

public class GroundSmash : MonoBehaviour
{
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip upwardForce_sfx;

    public ParticleSystem particleSmashEffect;

    public float upwardForceValue = 1f;
    public float explotionForceIntensity = 50f;
    public float explotionForceRange = 10f;  

    private void OnCollisionEnter(Collision coll) 
    {
        if(coll.transform.tag.Equals("Ground"))
        {
            Debug.Log("Toqué el piso");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Instantiate(particleSmashEffect, transform.position, Quaternion.identity);
            audioSrc.PlayOneShot(upwardForce_sfx);

            Collider[] surroudingObjcts = Physics.OverlapSphere(transform.position, explotionForceRange);
        
            foreach (Collider item in surroudingObjcts)
            {
                Rigidbody objRb = item.GetComponent<Rigidbody>();
                if(objRb == null || item.tag.Equals("Projectile")) continue;

                objRb.AddExplosionForce(upwardForceValue, transform.position, explotionForceRange);
                objRb.AddForce(new Vector3(0f, upwardForceValue, 0f), ForceMode.Impulse);
                StartCoroutine(FindObjectOfType<CameraShaker>().Shake(0.45f, 0.15f));
            }
        }    
    }
}
