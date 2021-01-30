using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedLink : MonoBehaviour
{
    private int positionID;
    private GlobalSpawner spawner;

    public void SetPositionID(int id) { positionID = id; }
    public void SetSpawner(GlobalSpawner fspawner) { spawner = fspawner; }

    public void Destroy()
    {
        spawner.RemoveOccupation(positionID);
        GameObject.Destroy(gameObject);
    }
}
