using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum E_ScrollDirection
{
    Forward,
    Right
}
public class ScrollingAutoDestroy : MonoBehaviour
{
    public E_ScrollDirection direction;
    public float speedMultiplier;
    private float speed;
    public float zLimits;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        var directionVec = direction == E_ScrollDirection.Forward ? Vector3.forward : -Vector3.right;
        transform.Translate(-directionVec * speed * speedMultiplier * Time.deltaTime);
        if (transform.position.z < zLimits)
            GetComponent<SpawnedLink>().Destroy();
    }
}
