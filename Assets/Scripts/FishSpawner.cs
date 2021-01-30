using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] spawnAreas;
    private bool[] spawnedAreas;
    public GameObject smallFish;
    public GameObject mediumFish;
    public GameObject bigFish;

    public float spawnTimerMin;
    public float spawnTimerMax;

    private float currentTimer;

    void Start()
    {
        spawnedAreas = new bool[spawnAreas.Length];
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer < 0)
        {
            Spawn();
            currentTimer += Random.Range(spawnTimerMin, spawnTimerMax + 1);
        }
    }

    GameObject PickUp()
    {
        var pick = Random.Range(0,3);
        switch(pick)
        {
            case 0:
            return smallFish;
            case 1:
            return mediumFish;
            case 2:
            return bigFish;
        }
        return smallFish;
    }

    public void RemoveOccupation(int id)
    {
        spawnedAreas[id] = false;
    }

    void Spawn()
    {
        int spawnedCount = 0;
        for (int i = 0; i < spawnedAreas.Length; ++i)
            if (spawnedAreas[i])
            spawnedCount++;

        if (spawnedCount > 2)
            return;

        var fish = PickUp();
        var areaId = Random.Range(0, spawnAreas.Length);
        while (spawnedAreas[areaId])
            areaId = Random.Range(0, spawnAreas.Length);

        var go = GameObject.Instantiate(fish, spawnAreas[areaId].position, spawnAreas[areaId].rotation);
        var fishGO = go.GetComponent<Fish>();
        fishGO.SetPositionID(areaId);
        fishGO.SetSpawner(this);
        spawnedAreas[areaId] = true;
    }
}
