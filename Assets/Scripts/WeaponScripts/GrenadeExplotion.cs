using UnityEngine;

public class GrenadeExplotion : MonoBehaviour
{
    public float explotionForceValue = 50f;
    public float explotionRange = 18f;
    public Rigidbody rb;
    public MeshRenderer mshRenderer;
    public AudioSource audioSrc;
    public AudioClip expl_sfx;
    public ParticleSystem particleExplotionEffect;

    private void Start() {}

    private void OnCollisionEnter(Collision other) 
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        if (mshRenderer.enabled)
        {
            Collider[] surroudingObjcts = Physics.OverlapSphere(transform.position, explotionRange);

            foreach (Collider item in surroudingObjcts)
            {
                Rigidbody objRb = item.GetComponent<Rigidbody>();
                if(objRb == null) continue;

                objRb.AddExplosionForce(explotionForceValue, transform.position, explotionRange);
                Instantiate(particleExplotionEffect, transform.position, Quaternion.identity);
                StartCoroutine(FindObjectOfType<CameraShaker>().Shake());
            }

            mshRenderer.enabled = false;
            audioSrc.PlayOneShot(expl_sfx);
        }
    }
}
