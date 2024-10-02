using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : Characters
{

    public float moveSpeed = 5f;            // Movement speed for the player
                                            // Prefab of the projectile to spawn
    public float projectileSpeed = 10f;     // Speed of the projectile
    private Vector2 movementInput;          // Store the player's movement input

    private Rigidbody2D rb;                 // Reference to the player's Rigidbody2D
    private Vector3 mouseWorldPosition;     // Position of the mouse in world coordinates
    public Transform muzzle;
    public Transform playerTransform;

    public List<string> collectedKeyIDs = new List<string>();
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Update the mouse position
        UpdateMousePosition();

        // Rotate the player to look at the mouse
        RotatePlayerToMouse();
    }

    private void FixedUpdate()
    {
        // Move the player based on WASD input
        MovePlayer();
    }

    // Update the mouse position in world coordinates
    private void UpdateMousePosition()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseWorldPosition.z = 0; // Ensure the z position is 0 as we are working in 2D
    }

    // Move the player based on WASD input
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    // Rotate the player to look at the mouse
    private void RotatePlayerToMouse()
    {
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    // Called when the shooting action is triggered (e.g., spacebar or gamepad button)
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)

        {
            Shoot();
        }
    }

    private void MovePlayer()
    {
        // Calculate the movement direction based on input
        Vector2 movement = movementInput * moveSpeed * Time.fixedDeltaTime;

        // Apply the movement to the player's Rigidbody2D to move the sprite
        rb.MovePosition(rb.position + movement);
    }

    private void Shoot()
    {
        // Ensure the muzzle is assigned
        if (muzzle == null)
        {
            Debug.LogError("Muzzle is not assigned.");
            return;
        }

        // Instantiate the projectile prefab at the muzzle's position
        GameObject projectile = Instantiate(projectilePrefab, muzzle.position, Quaternion.identity);

        // Calculate the direction from the muzzle to the player
        Vector2 direction = ((Vector2)playerTransform.position - (Vector2)muzzle.position).normalized;

        // Optionally, flip the direction to account for the z-axis inversion if needed
        direction = new Vector2(-direction.x, -direction.y);

        // Set the projectile's velocity to move towards the target position
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = direction * projectileSpeed;

        Debug.Log("Enemy shoots!");
    }

    public void CollectKeys(List<string> keyIDs)
    {
        foreach (string keyID in keyIDs)
        {
            if (!collectedKeyIDs.Contains(keyID))
            {
                collectedKeyIDs.Add(keyID);
                Debug.Log("Collected key ID: " + keyID);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player is touching a key object
        Key key = other.GetComponent<Key>();
        if (key != null)
        {
            // Collect the keys from the key object
            CollectKeys(key.keyIDs);

            // Destroy the key object after collection
            Destroy(other.gameObject);
        }
    }
}
