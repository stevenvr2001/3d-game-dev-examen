using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public enum PowerUpType
    {
        coffee,
        energyDrink
    }
    // Variabele voor de powerup type
    public PowerUpType powerUpType;

    private PlayerStats playerStats; // Voeg een referentie toe naar PlayerStats

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>(); // Vind de PlayerStats component
    }

    void OnTriggerEnter(Collider other) // Correcte schrijfwijze van OnTriggerEnter
    {
        if (other.CompareTag("Player"))
        {
            pickUpPowerUp(); // Correcte schrijfwijze van pickUpPowerUp
            LogPowerUpCollected(); // Correcte schrijfwijze van LogPowerUpCollected
        }
        else
        {
            Console.Write("Kwni");
        }
    }

    void pickUpPowerUp()
    {
        switch (powerUpType)
        {
            case PowerUpType.coffee:
                playerStats.RestoreEnergy(30);
                break;
            case PowerUpType.energyDrink:
                playerStats.RestoreEnergy(50); // Correct gebruik van de juiste variabele voor energie
                break;
        }
        Destroy(gameObject); // Verwijder de powerup na het oppakken
    }
   
   void LogPowerUpCollected()
    {
        Debug.Log("Powerup collected: " + powerUpType);
    }
}
