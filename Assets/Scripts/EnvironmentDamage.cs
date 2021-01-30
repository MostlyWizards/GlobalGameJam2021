using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentDamage : MonoBehaviour
{
    ShipStatus shipStatus;

    public float[] timeBeforeUpgradeNeeded;
    public float damageTimer;
    private float currentDamageTimer;
    private int currentStep;
    private float currentTime;


    // Start is called before the first frame update
    void Start()
    {
        shipStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipStatus>();
        currentDamageTimer = damageTimer;
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentStep < timeBeforeUpgradeNeeded.Length
            && currentTime > timeBeforeUpgradeNeeded[currentStep])
            {
                ResetDamageTimer();
                currentStep++;
            }

        currentDamageTimer -= Time.deltaTime;
        if (currentDamageTimer < 0)
        {
            // Check requirements
            if (shipStatus.GetCurrentUpgrade() < currentStep)
            {
                shipStatus.Damage(1);
                Debug.Log("Env damage");
            }
            currentDamageTimer += damageTimer;
        }
    }

    public void ResetDamageTimer()
    {
        currentDamageTimer = damageTimer;
    }
}
