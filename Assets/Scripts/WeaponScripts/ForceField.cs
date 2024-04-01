using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public GameObject vortexObj;
    [SerializeField] private float influenceRange;
    [SerializeField] private float distanceToObject;
    [SerializeField] private float intensity; 
    [SerializeField] private float rotationIntensity; 
    private Vector3 pullForce;
    private List<GameObject> attractedObjects = new List<GameObject>();

    private void FixedUpdate() 
    {
        for (int i = 0; i < attractedObjects.Count; i++)
        {
            distanceToObject = Vector3.Distance(attractedObjects[i].transform.position, vortexObj.transform.position);
            
            if(distanceToObject <= influenceRange)
            {
                pullForce = (vortexObj.transform.position - attractedObjects[i].transform.position).normalized / distanceToObject * intensity;
                attractedObjects[i].GetComponent<Rigidbody>().AddForce(pullForce, ForceMode.Acceleration);
                attractedObjects[i].transform.RotateAround(transform.position, Vector3.forward, rotationIntensity * Time.deltaTime);
                attractedObjects[i].transform.RotateAround(transform.position, Vector3.left, rotationIntensity * Time.deltaTime);
            }
        }    
    }

    private void OnTriggerEnter(Collider coll) 
    {
        if(coll.tag.Equals("Interactable"))
        {
            Debug.Log("Detected interactable");
            attractedObjects.Add(coll.gameObject);
        }    
    }
}
