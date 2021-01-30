using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int hungerValue;
    private int positionID;
    private FishSpawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPositionID(int id) { positionID = id; }
    public void SetSpawner(FishSpawner fspawner) { spawner = fspawner; }
    public void Eated()
    {
        spawner.RemoveOccupation(positionID);
        GameObject.Destroy(gameObject);
    }
}
