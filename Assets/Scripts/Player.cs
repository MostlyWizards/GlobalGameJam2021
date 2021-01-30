using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputActionAsset actions;

    
    public int startHunger;

    public int hungerLoss;
    private int hunger;
    private float hungerTimer;

    private int woodStock;
    private int metalStock;
    private int ropeStock;


    private Fish reachableFish;
    
    // Start is called before the first frame update
    void Start()
    {
        var map = actions.FindActionMap("Ship");
        map["Eat"].performed += OnEatFish;

        hunger = startHunger;
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
    }

    void OnTriggerEnter(Collider collider)
    {
        reachableFish = collider.GetComponent<Fish>();
    }

    void OnTriggerExit(Collider collider)
    {
        reachableFish = null;
    }

    public void OnEatFish(InputAction.CallbackContext context)
    {
        if (reachableFish)
        {
            hunger += reachableFish.hungerValue;
            reachableFish.Eated();
        }
    }

    public bool HasMaterials(materials mat)
    {
        return woodStock >= mat.wood && metalStock >= mat.metal && ropeStock >= mat.rope;
    }

    public void RemoveMaterials(materials mat)
    {
        woodStock -= mat.wood;
        metalStock -= mat.metal;
        ropeStock -= mat.rope;
    }

}
