using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    public UIDocument UIDocument; // Verwijzing naar de UI Document
    public PlayerStats playerStats; // Verwijzing naar PlayerStats
    public float maxTime = 5; // Maximale tijd in minuten
    private Label Timer; // Timer Label
    private ProgressBar EnergyBar; // Energy Bar als ProgressBar
    private float timer; // Interne timer in seconden

    private void Start()
    {
        if (UIDocument == null || playerStats == null)
        {
            Debug.LogError("UIDocument of PlayerStats is niet ingesteld.");
            return;
        }

        // Verkrijg de UI-elementen uit het Visual Tree Asset
        var root = UIDocument.rootVisualElement;

        // Zoek het Timer Label en de Energy Bar
        Timer = root.Q<Label>("Timer");
        EnergyBar = root.Q<ProgressBar>("EnergyBar");

        // Controleer of elementen succesvol gevonden zijn
        if (Timer == null)
        {
            Debug.LogError("Timer UI-element niet gevonden.");
        }

        if (EnergyBar == null)
        {
            Debug.LogError("EnergyBar UI-element niet gevonden.");
        }

        // Stel de timer in op de maximale tijd
        timer = maxTime * 60;

        // Stel de EnergyBar in als deze bestaat
        if (EnergyBar != null)
        {
            EnergyBar.highValue = playerStats.maxEnergy; // Stel maximumwaarde in
            EnergyBar.value = Mathf.Clamp(playerStats.currentEnergy, 0, playerStats.maxEnergy); // Stel huidige waarde in
        }
    }

    private void Update()
    {
        // Controleer of de timer actief moet zijn
        if (timer > 0)
        {
            // Update de timer
            timer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);

            if (Timer != null)
            {
                Timer.text = $"{minutes:00}:{seconds:00}";
            }
        }
        else if (timer <= 0 && Timer != null)
        {
            Timer.text = "00:00";
            Gameover();
        }

        // Update de Energy Bar met de huidige energie van de speler
        if (EnergyBar != null && playerStats != null)
        {
            EnergyBar.value = Mathf.Clamp(playerStats.currentEnergy, 0, playerStats.maxEnergy);
        }
    }


//Gameover

    private void Gameover()
    {
        SceneManager.LoadScene("Died");
    }

}