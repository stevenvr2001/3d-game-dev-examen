using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [Header("References")]
    public PlayerStats playerStats; // Sleep hier de speler in
    public Slider energySlider;

    private void Start()
    {
        // Zorg ervoor dat de slider goed staat
        energySlider.maxValue = playerStats.maxEnergy;
        energySlider.value = playerStats.currentEnergy;
    }

    private void Update()
    {
        // Update de slider waarde op basis van de energy
        energySlider.value = playerStats.currentEnergy;
    }
}
