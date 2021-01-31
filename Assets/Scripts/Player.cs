using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public UnityEngine.UI.Slider hungerBar;
    public InputActionAsset actions;

    public TMPro.TextMeshProUGUI woodUI;
    public TMPro.TextMeshProUGUI metalUI;
    public TMPro.TextMeshProUGUI ropeUI;

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
        RefreshHungerDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        hungerTimer += Time.deltaTime;
        if (hungerTimer > 1)
        {
            hunger -= hungerLoss;
            RefreshHungerDisplay();
            --hungerTimer;
        }
        if (hunger <= 0)
        {
            Debug.Log("Hunger death");
            GameObject.FindObjectOfType<GameManager>().Lose();
        }

        woodUI.text = woodStock.ToString();
        metalUI.text = metalStock.ToString();
        ropeUI.text = ropeStock.ToString();

        // :(
        var tmp = transform.position;
        tmp.y = 0.1f;
        transform.position = tmp;
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
            hunger = hunger > 50 ? 50 : hunger;
            reachableFish.GetComponent<SpawnedLink>().Destroy();
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

    void RefreshHungerDisplay()
    {
        hungerBar.maxValue = 50;
        hungerBar.value = hunger;
    }
}
