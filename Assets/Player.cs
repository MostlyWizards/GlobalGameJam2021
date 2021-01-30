using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputActionAsset actions;

    public int lifeMax1;
    public int lifeMax2;
    public int lifeMax3;
    public int startHunger;

    public int hungerLoss;
    private int hunger;
    private float hungerTimer;

    private int currentLife;
    private int woodStock;
    private int metalStock;
    private int ropeStock;

    private IEnumerator corroutine;
    private Fish reachableFish;
    
    // Start is called before the first frame update
    void Start()
    {
        var map = actions.FindActionMap("Ship");
        map["Eat"].performed += OnEatFish;

        currentLife = lifeMax1;
        hunger = startHunger;
        corroutine = WasHitted();
        }

    // Update is called once per frame
    void Update()
    {
        hungerTimer += Time.deltaTime;
        if (hungerTimer > 1)
        {
            hunger -= hungerLoss;
            Debug.Log(hunger);
            --hungerTimer;
        }
        if (hunger <= 0)
            UnityEditor.EditorApplication.ExitPlaymode();
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

    void OnTriggerEnter(Collider collider)
    {
        reachableFish = collider.GetComponent<Fish>();
    }

    void OnTriggerExit(Collider collider)
    {
        reachableFish = null;
    }

    IEnumerator WasHitted()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(2);
        GetComponent<BoxCollider>().enabled = true;
    }

    public void OnEatFish(InputAction.CallbackContext context)
    {
        if (reachableFish)
        {
            hunger += reachableFish.hungerValue;
            reachableFish.Eated();
        }
    }
}
