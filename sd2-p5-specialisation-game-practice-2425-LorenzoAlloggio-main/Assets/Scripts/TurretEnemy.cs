using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Characters
{

    // THE COMMENTED VARIABLES ARE THE ONES THAT I INHERITED


    public float shootingInterval = 1f;    // Time between shots
                                           // Projectile prefab
    public float projectileSpeed = 10f;    // Speed of the projectile
    public float health = 3f;              // Health of the enemy
    public float detectionRange = 10f;     // Range within which the enemy can detect the player
    public Transform muzzle;               // Muzzle for shooting projectiles

    private Transform playerTransform;     // Reference to the player's transform
    private float nextShootTime = 0f;      // Time when the next shot is allowed

    protected override void Start()
    {
        base.Start();
        // Find the player by tag
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            // Check if the player is within detection range and in line of sight
            if (IsPlayerInSight())
            {
                // Aim at the player
                AimAtPlayer();

                // Shoot at regular intervals
                if (Time.time >= nextShootTime)
                {
                    Shoot();
                    nextShootTime = Time.time + shootingInterval;
                }
            }
        }
    }

    private void AimAtPlayer()
    {
        // Calculate the direction to the player
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // Calculate the angle in degrees and rotate the turret to face the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation on the Z axis (since this is a 2D object)
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Shoot()
    {
        // Instantiate the projectile and shoot it towards the player
        GameObject projectile = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

        // Calculate the direction towards the player
        Vector2 direction = (playerTransform.position - muzzle.position).normalized;

        // Set projectile velocity towards the player
        projectileRb.velocity = direction * projectileSpeed;

        Debug.Log("Enemy shoots!");
    }

    private bool IsPlayerInSight()
    {
        // Calculate the direction to the player from the muzzle
        Vector2 directionToPlayer = (playerTransform.position - muzzle.position).normalized;

        // Calculate the distance to the player
        float distanceToPlayer = Vector2.Distance(muzzle.position, playerTransform.position);

        // Draw the debug line (for visualization in the Scene view)
        Debug.DrawLine(muzzle.position, playerTransform.position, Color.red);

        // Check if the player is within detection range
        if (distanceToPlayer > detectionRange)
        {
            Debug.Log("Player out of range");
            return false;
        }

        // Raycast from the muzzle instead of the turret's main body, to avoid hitting the turret itself
        RaycastHit2D hit = Physics2D.Raycast(muzzle.position, directionToPlayer, detectionRange);

        // Debug information about the raycast
        if (hit.collider != null)
        {
            Debug.Log("Raycast hit: " + hit.collider.name);

            // Return true if the player is hit and no other object is blocking the way
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
            else
            {
                Debug.Log("Blocked by: " + hit.collider.name);
            }
        }
        else
        {
            Debug.Log("Raycast hit nothing");
        }

        return false;
    }

    private void Die()
    {
        // Handle death (e.g., play animation, destroy object)
        Destroy(gameObject);
        Debug.Log("Enemy destroyed!");
    }
}
