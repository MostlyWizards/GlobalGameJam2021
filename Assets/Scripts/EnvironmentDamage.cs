using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnvironmentDamage : MonoBehaviour
{
    public UnityEngine.AudioClip[] musics;
    private AudioSource audioSource;
    ShipStatus shipStatus;
    public VisualEffect effect;
    public GameObject[] lights;
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
        effect.SetFloat("Intensity", 0);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = musics[0];
        audioSource.Play();
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
                switch(currentStep)
                {
                    case 1:
                    effect.SetFloat("Intensity", 1000);
                    break;

                    case 2:
                    effect.SetFloat("Intensity", 10000);
                    break;

                    case 3:
                    lights[0].SetActive(false);
                    lights[1].SetActive(true);
                    break;
                }
                audioSource.clip = musics[currentStep % musics.Length];
                audioSource.Play();
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
