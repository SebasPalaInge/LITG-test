using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField]private Vector3 originalPos;

    private void Start() 
    {
        originalPos = transform.localPosition;
    }

    public IEnumerator Shake(float duration = 0.45f, float magnitude = 0.12f)
    {
        for (float elapsed = 0; elapsed < duration; elapsed+=Time.deltaTime)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(0.6f, 0.75f);

            transform.localPosition = new Vector3(x, y, originalPos.z);
            yield return null;
        }
        
        transform.localPosition = originalPos;
    }
}
