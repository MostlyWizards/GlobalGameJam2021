using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int hungerValue;

    public float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            GetComponent<SpawnedLink>().Destroy();
    }
}
