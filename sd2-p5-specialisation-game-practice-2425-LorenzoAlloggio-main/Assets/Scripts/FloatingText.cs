using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{

    public Vector3 Offset = new Vector3(0f, 0.1f, 0f);
    public float DestroyTime = 3f;
    public Vector3 RandomizeIntensity = new Vector3(0.5f, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DestroyTime);

        // If this is a UI element, we need to convert the enemy's world position to screen space
        Vector3 worldPosition = transform.position; // Assuming this script is attached to the floating text prefab

        // Adjust the position based on the enemy's world position
        transform.position = worldPosition + Offset;

        // Apply randomization
        Vector3 randomOffset = new Vector3(
            Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
            Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
            Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z)
        );

        transform.position += randomOffset;

        // Log the final position
        Debug.Log("Floating text position: " + transform.position);
    }

}
