using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeProjectile : MonoBehaviour
{
    public GameObject spikePrefab;

    private Vector2 projectileVector;
    private float radius = 5f;
    private int numProjectiles = 10;

    private void Start()
    {
        InvokeRepeating("Shoot", 3f, 3f);
    }

    private void Shoot()
    {
        float angleStep = 360f / numProjectiles;
        float angle = 0f;
        Vector2 enemyPosition = transform.position;

        // For each projectile...
        for (int i = 0; i < numProjectiles; i++)
        {
            // Calculate vector
            float projectileX = enemyPosition.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileY = enemyPosition.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;
            Vector2 projectileVector = new Vector2(projectileX, projectileY);
            Vector2 movement = (projectileVector - enemyPosition);

            // Spawn spike and set direction
            var spike = Instantiate(spikePrefab, enemyPosition, Quaternion.identity);
            spike.GetComponent<Spike>().SetDirection(movement.normalized);

            // Update spawn angle for next spike
            angle += angleStep;
        }
    }
}
