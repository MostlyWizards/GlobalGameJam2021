using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_Collectable
{
    Wood = 0,
    Metal,
    Rope
}

public class Collectable : MonoBehaviour
{
    public E_Collectable type;
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.GetComponent<Player>() || collision.rigidbody.GetComponent<Obstacle>())
            Object.Destroy(gameObject);
    }
}
