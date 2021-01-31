using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public struct materials
{
    public int wood;
    public int metal;
    public int rope;
}

public class ShipStatus : MonoBehaviour
{
    public UnityEngine.UI.Slider healthBar;
    public TMPro.TextMeshProUGUI healthNumber;

    public TMPro.TextMeshProUGUI woodRequirements;
    public TMPro.TextMeshProUGUI metalRequirements;
    public TMPro.TextMeshProUGUI ropeRequirements;

    public int[] lifeMax;

    public materials[] upgradeRequierements;

    public GameObject[] renderers;

    private int currentLife;
    private int currentUpgrade;

    private Player player;
    private IEnumerator corroutine;

    // Start is called before the first frame update
    void Start()
    {
        corroutine = WasHitted();
        currentLife = lifeMax[0];
        currentUpgrade = 0;
        healthBar.maxValue = lifeMax[0];
        healthBar.value = currentLife;
        healthNumber.text = currentLife.ToString();
        renderers[0].SetActive(true);
        for (int i = 1; i < 4; ++i)
            renderers[i].SetActive(false);
        player = GetComponent<Player>();

        var map = player.actions.FindActionMap("Ship");
        map["UpgradeShip"].performed += OnUpgrade;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentUpgrade > 0 && currentLife < lifeMax[currentUpgrade - 1])
            Downgrade();

        if (currentLife <= 0)
        {
            Debug.Log("Ship destroyed");
            GameObject.FindObjectOfType<GameManager>().Lose();
        }

        RefreshHealthDisplay();
        RefreshRequirements();
    }

    void OnCollisionEnter(Collision collision)
    {
        var obstacle = collision.gameObject.GetComponent<Obstacle>();
        if (obstacle)
        {
            currentLife -= obstacle.size;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            StartCoroutine(corroutine);
        }
    }

    IEnumerator WasHitted()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(2);
        GetComponent<BoxCollider>().enabled = true;
    }

    public void OnUpgrade(InputAction.CallbackContext context)
    {
        Upgrade();
    }

    void RefreshHealthDisplay()
    {
        healthBar.maxValue = lifeMax[currentUpgrade];
        healthBar.value = currentLife;
        healthNumber.text = currentLife.ToString();
    }

    void Upgrade()
    {
        if (!player.HasMaterials(upgradeRequierements[currentUpgrade+1]))
        return;

        renderers[currentUpgrade].SetActive(false);
        ++currentUpgrade;
        renderers[currentUpgrade].SetActive(true);
        player.RemoveMaterials(upgradeRequierements[currentUpgrade]);
        currentLife = lifeMax[currentUpgrade];

        RefreshHealthDisplay();
    }

    void Downgrade()
    {
        renderers[currentUpgrade].SetActive(false);
        --currentUpgrade;
        renderers[currentUpgrade].SetActive(true);
        GameObject.FindObjectOfType<EnvironmentDamage>().ResetDamageTimer();

        RefreshHealthDisplay();
    }

    public int GetCurrentUpgrade() { return currentUpgrade; }
    public void Damage(int value) { currentLife -= value; RefreshHealthDisplay(); }

    void RefreshRequirements()
    {
        woodRequirements.text = upgradeRequierements[currentUpgrade + 1].wood.ToString();
        metalRequirements.text = upgradeRequierements[currentUpgrade + 1].metal.ToString();
        ropeRequirements.text = upgradeRequierements[currentUpgrade + 1].rope.ToString();
    }
}
