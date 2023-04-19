using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ink : MonoBehaviour
{
    public int life = 1;

    [SerializeField] private Rigidbody2D rb;

    private float inkSpeed = 15f;
    private float lifespan = 4f;
    private float timer = 0f;

    private Vector2 inkDirection;
    private Vector3Int inkEndPosition;

    private void Update()
    {
        // Increment timer and move ink
        if (gameObject.activeSelf)
        {
            timer += Time.deltaTime;
            transform.Translate(inkDirection * Time.deltaTime * inkSpeed);
            //transform.position = Vector2.MoveTowards(transform.position, inkDirection, inkSpeed * Time.deltaTime);
        }
        // Set as inavative if it has reached lifespan
        if (timer >= lifespan)
        {
            RemoveInk();
        }
    }

    // Handling ink collisions
    private void OnCollisionEnter2D(Collision2D other)
    {
        // If ink collides with tile and has life remaining, destroy tile
        if (other.gameObject.CompareTag("Tiles") && life > 0)
        {
            life--;
            Destroy(other.gameObject);
        }
        // If ink collides with enemy and has life remaining, damage enemy
        else if (other.gameObject.CompareTag("Enemies") && life > 0)
        {
            life--;
            other.collider.GetComponent<EnemyController>().TakeDamage(10);
        }

        // If ink has no life left, return to object pool
        if (life == 0)
        {
            RemoveInk();
        }
    }

    // Reset ink in object pool
    private void RemoveInk()
    {
        gameObject.SetActive(false);
        timer = 0f;
        life = 1;
    }

    // Set ink direction
    public void SetDirection(Vector2 direction)
    {
        inkDirection = direction;
    }
}
