using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float speedMultiplier;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.forward * speed * speedMultiplier * Time.deltaTime);
    }
}
