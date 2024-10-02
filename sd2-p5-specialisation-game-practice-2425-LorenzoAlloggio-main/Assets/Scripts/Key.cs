using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // List of unique IDs for this key
    public List<string> keyIDs = new List<string>();

    // Detect collision with the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is touching the key
        Movement player = collision.collider.GetComponent<Movement>();
        if (player != null)
        {
            // Add all the key's IDs to the player's list
            player.CollectKeys(keyIDs);

            // Destroy the key object
            Destroy(gameObject);
        }
    }
}