using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Characters : MonoBehaviour
{

    public GameObject projectilePrefab;

    public float maxHealth = 100f;  // Maximum health the object can have
    private float currentHealth;    // Current health
    public int scoreValue = 10;
    public GameObject FloatingTextPrefab;
    public Transform EnemyTransform;

    public AudioClip BulletHitSound;
    private AudioSource audioSource;

    protected virtual void Start()
    {
        // Initialize current health to max health at the start
        currentHealth = maxHealth;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = BulletHitSound;
    }
    #region  HEALTH
    public void TakeDamage(float damageAmount)
    {
        // Reduce the current health by the damage amount
        currentHealth -= damageAmount;

        // Log the damage for debugging
        Debug.Log(gameObject.name + " took " + damageAmount + " damage.");

        audioSource.Play();

        // Show floating text if the prefab is assigned
        if (FloatingTextPrefab)
        {
            ShowFloatingText(damageAmount);
        }

        // Check if health is depleted
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void ShowFloatingText(float damageAmount)
    {
        // Instantiate the floating text prefab at the enemy's position
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity);

        // Offset the position slightly upwards so it doesn't overlap with the enemy
        go.transform.position += new Vector3(0, 1, 0); // Adjust '1' as needed for visibility

        // Set the text to display the damage amount
        go.GetComponent<TMP_Text>().text = damageAmount.ToString();
    }


    private void Die()
    {
        // Log the death for debugging
        Debug.Log(gameObject.name + " has died!");
    
        if (gameObject.name == "EnemyTurret" || gameObject.name ==  "EnemyTurret1" || gameObject.name == "EnemyTurret2")
        {
            ScoreManager.Instance.AddScore(scoreValue);
        }

        if (gameObject.name == "Player")
        {
            SceneManager.LoadScene(2);
        }

        // Destroy the game object
        Destroy(gameObject);
    }

    public void Heal(float healAmount)
    {
        // Increase current health by the heal amount
        currentHealth += healAmount;

        // Clamp current health to not exceed max health
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        // Log the healing for debugging
        Debug.Log(gameObject.name + " healed by " + healAmount + ". Current health: " + currentHealth);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        Debug.Log(gameObject.name + " health reset to full.");
    }


    //FOR HEALTH BAR
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Win"))
        {
            SceneManager.LoadScene(3);
        }
    }

    #endregion
}