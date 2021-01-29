using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject woodPrefab; //Todo array?
    public GameObject metalPrefab;
    public GameObject ropePrefab;

    public float spawnMinTimer;
    public float spawnMaxTimer;
    private float currentTimer;
    private float zAxisValue;
    private List<GameObject> goTracker; //TODO pool
    private List<GameObject> goDestroyed;

    void Start()
    {
        zAxisValue = transform.position.z;
        goTracker = new List<GameObject>();
        goDestroyed = new List<GameObject>();
        currentTimer = Random.Range(spawnMinTimer, spawnMaxTimer);
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer < 0)
        {
            goTracker.Add(GameObject.Instantiate(PickPrefab(), transform.position, transform.rotation));
            currentTimer += Random.Range(spawnMinTimer, spawnMaxTimer);
        }

        /*foreach(var go in goTracker)
        {
            if (go.transform.position.z < -5)
            {
                goDestroyed.Add(go);
                GameObject.Destroy(go);
            }
        }
        foreach(var go in goDestroyed)
            goTracker.Remove(go);
        goDestroyed.Clear();*/
    }

    GameObject PickPrefab()
    {
        var pick = Random.Range(0, 3);
        switch(pick)
        {
            case 0:
            return woodPrefab;
            case 1:
            return metalPrefab;
            case 2:
            return ropePrefab;
        }
        return woodPrefab;
    }
}
