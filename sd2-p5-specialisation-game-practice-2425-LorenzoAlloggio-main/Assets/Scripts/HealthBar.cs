using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;  // Reference to the UI slider that shows health
    public Characters Player;
    //public Characters character; // Reference to the character object

    // Start is called before the first frame update
    void Start()
    {
        // Set the slider's max value to the character's max health

        healthSlider.maxValue = Player.maxHealth;
        healthSlider.value = Player.maxHealth;  // Initialize the slider to full health
    }

    // Update is called once per frame
    void Update()
    {
        // Update the slider value to reflect the current health
        healthSlider.value = GetHealth();
    }

    // Get current health from the character
    float GetHealth()
    {
        return Player.GetCurrentHealth();
    }
}