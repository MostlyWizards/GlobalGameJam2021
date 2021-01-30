using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int lifeMax1;
    public int lifeMax2;
    public int lifeMax3;

    private int currentLife;
    private int woodStock;
    private int metalStock;
    private int ropeStock;

    private IEnumerator corroutine;
    // Start is called before the first frame update
    void Start()
    {
        currentLife = lifeMax1;
        corroutine = WasHitted();
        }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        var collectable = collision.gameObject.GetComponent<Collectable>();
        if (collectable)
        {
            switch(collectable.type)
            {
                case E_Collectable.Wood:
                ++woodStock;
                break;

                case E_Collectable.Metal:
                ++metalStock;
                break;

                case E_Collectable.Rope:
                ++ropeStock;
                break;
            }
            Debug.Log("w : " + woodStock + " / m : " + metalStock + " / r : " + ropeStock);
        }
        var obstacle = collision.gameObject.GetComponent<Obstacle>();
        if (obstacle)
        {
            currentLife -= obstacle.size;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            StartCoroutine(corroutine);
        }
        if (currentLife < 0)
            UnityEditor.EditorApplication.ExitPlaymode();
    }

    IEnumerator WasHitted()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(2);
        GetComponent<BoxCollider>().enabled = true;
        Debug.Log("Done!");
    }
}
