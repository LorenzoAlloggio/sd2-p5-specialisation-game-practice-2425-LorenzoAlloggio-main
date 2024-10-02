using TMPro;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public float damage = 10f;  // Damage dealt by the projectile
    public GameObject FloatingTextPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object hit has a Health component
        Characters CharactersHealth = collision.gameObject.GetComponent<Characters>();

        if (CharactersHealth != null)
        {
            // Deal damage to the object
            CharactersHealth.TakeDamage(damage);

            if (FloatingTextPrefab)
            {
                ShowFloatingText();
            }
        }

        // Destroy the projectile after collision with any object
        Destroy(gameObject);

        if (collision.gameObject.CompareTag("DestroyableWall"))
        {
            Destroy(collision.gameObject);
        }
    }
    void ShowFloatingText()
    {
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TMP_Text>().text = damage.ToString();
    }
}