using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // The key ID required to open this door
    public string requiredKeyID;

    // The door's state (true if open, false if closed)
    private bool isOpen = false;

    // Reference to the door's collider (to enable/disable it)
    private Collider2D doorCollider;

    // Reference to the door's sprite renderer or animator to change appearance (optional)
    private SpriteRenderer doorSprite;

    private void Awake()
    {
        // Get the collider and sprite renderer components
        doorCollider = GetComponent<Collider2D>();
        doorSprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is colliding with the door
        Movement player = collision.collider.GetComponent<Movement>();
        if (player != null)
        {
            // Check if the player has the required key ID
            if (player.collectedKeyIDs.Contains(requiredKeyID))
            {
                // Open the door
                OpenDoor();
            }
            else
            {
                Debug.Log("Door is locked. You need the key: " + requiredKeyID);
            }
        }
    }

    // Method to open the door
    private void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;

            // Disable the door's collider to allow the player to pass through
            if (doorCollider != null)
            {
                doorCollider.enabled = false;
            }

            // Optional: Change the door's appearance to indicate it's open
            if (doorSprite != null)
            {
                doorSprite.color = Color.green; // Change to a different color, or play an animation
            }

            Debug.Log("Door opened!");
        }
    }
}