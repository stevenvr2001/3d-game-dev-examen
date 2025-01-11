using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Energy Settings")]
    public float maxEnergy = 100f;
    public float energyDecay = 10f;

    [Header("Speed Settings")]
    public float baseSpeed = 7;  // Basis snelheid
    public float minSpeed = 2;   // Minimale snelheid

    public float currentEnergy { get; private set; }
    public float currentSpeed { get; private set; }

    void Start()
    {
        currentEnergy = maxEnergy;
        UpdateSpeed();
    }

    private void Update()
    {
        DecayEnergy();
        UpdateSpeed();
    }

    private void DecayEnergy()
    {
        currentEnergy -= energyDecay * Time.deltaTime;
        currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergy);
    }

    private void UpdateSpeed()
    {
        // Bereken de snelheid op basis van de vermoeidheid
        float speedFactor = currentEnergy / maxEnergy;
        currentSpeed = Mathf.Lerp(minSpeed, baseSpeed, speedFactor);

        // Als de vermoeidheid onder de 20% ligt, vertraag de speler extra
        if (currentEnergy < maxEnergy * 0.2f)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, minSpeed, 0.5f); // Vertraag de snelheid extra
        }
    }

    public void RestoreEnergy(float amount)
    {
        currentEnergy = Mathf.Clamp(currentEnergy + amount, 0f, maxEnergy);
    }


}


