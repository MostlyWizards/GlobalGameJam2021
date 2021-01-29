using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -5) //TODO global controller
            Object.Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Trigger");
        if (collider.GetComponent<ShipMovement>())
            Object.Destroy(gameObject);
    }
}
