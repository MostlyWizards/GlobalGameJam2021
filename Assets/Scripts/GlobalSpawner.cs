using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] spawnAreas;
    private bool[] spawnedAreas;

    public GameObject[] prefabs;
    public int[] rnd;

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
        var pick = Random.Range(0, 100);
        for (int i = 0; i < rnd.Length; ++i)
            if (pick < rnd[i])
                return prefabs[i];
        return prefabs[0];
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

        var go = GameObject.Instantiate(fish);
        var position = spawnAreas[areaId].position;
        go.transform.position = position;
        var fishGO = go.GetComponent<SpawnedLink>();
        fishGO.SetPositionID(areaId);
        fishGO.SetSpawner(this);
        spawnedAreas[areaId] = true;
    }
}
