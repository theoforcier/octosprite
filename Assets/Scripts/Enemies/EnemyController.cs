using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 2f;
    public int maxHealth = 100;
    public Animator animator;
    public AudioClip deathAudio;
    public GameObject enemyPrefab;

    private GameObject player;
    private SpriteRenderer enemyRenderer;
    private Vector2 movement;
    private int currentHealth;
    private Color baseColor = new Color(255f, 255f, 255f, 255f);
    private Color dmgColor = new Color(1, 1, 1, 0.3f);
    private float dmgTimer = 0f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        currentHealth = maxHealth;
        enemyRenderer = gameObject.GetComponent<SpriteRenderer>();
        movement = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
    }

    private void Update()
    {
        if (dmgTimer > 0)
        {
            enemyRenderer.color = dmgColor;
            dmgTimer -= Time.deltaTime;
        }
        else
        {
            enemyRenderer.color = baseColor;
            // Randomly change movement in random direction every 500 calls
            if (Random.Range(0, 500) == 0)
            {
                movement = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            }
        }

        // Spawn new puffer if ignored
        if (transform.position.y < player.transform.position.y - 100)
        {
            NewSpawn();
        }
    }

    // Moving rigibody
    private void FixedUpdate()
    {
        transform.position = rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime;
    }

    private void NewSpawn()
    {
        //Spawn new puffer ~150 tiles up
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y + 150, 0);
        var newEnemy = Instantiate(enemyPrefab, newPos, Quaternion.identity);
        Destroy(gameObject);
    }

    // Update health and switch color
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        dmgTimer = 0.1f;

        // Death check
        if (currentHealth <= 0)
        {
            AudioManager.Instance.PlaySound(deathAudio);
            ScoreManager.Instance.AddScore(500);
            NewSpawn();
        }
    }
}
