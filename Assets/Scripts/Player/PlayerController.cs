using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed, rotationOffset;
    public Animator animator;
    public AudioClip moveAudio, deathAudio;

    private AudioSource effectSource;
    private float inkTimer = 0f;
    private float fireRate = 0.02f;
    private Vector2 mousePos, playerPos;
    private float xClamp;
    private float playerWidth, playerHeight;
    private bool collisionOccured = false;

    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        // Set effect source and screen bounds
        effectSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>().effectsSource;
        xClamp = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).x;
        playerWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        playerHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    private void Update()
    {
        inkTimer += Time.deltaTime;

        // Follow mouse while holding left click, rotating sprite accordingly
        if (Input.GetMouseButton(0) && !PauseMenu.isPaused)
        {
            // Update animation and SFX
            animator.speed = 2;
            if (!effectSource.isPlaying)
            {
                AudioManager.Instance.PlaySound(moveAudio);
            }

            // Get positions for squid 
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            playerPos = transform.position;

            // Mouse position relative the player
            // Vector from squid to mouse --> mousePos - squidPos
            Vector2 mouseVector;
            mouseVector.x = mousePos.x - playerPos.x;
            mouseVector.y = mousePos.y - playerPos.y;

            // Code adjusted from: https://www.youtube.com/watch?v=7c68z05vaX4&ab_channel=JacksonAcademy
            // Calculate rotation angle and rotate sprite accordingly
            float angle = Mathf.Atan2(mouseVector.y, mouseVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffset));

            // Move sprite
            transform.position = Vector2.MoveTowards(transform.position, mousePos, playerSpeed * Time.deltaTime);

            // Shoot ink depending on fireRate
            if (inkTimer >= fireRate)
            {
                Shoot(-mouseVector);
                inkTimer = 0f;
            }
        }
        else
        {
            // Reset animation speed and SFX
            animator.speed = 1;
            effectSource.GetComponent<AudioSource>().clip = null;
        }
    }

    // Keep player within screen bounds
    private void LateUpdate()
    {
        Vector3 updatedPos = transform.position;
        updatedPos.x = Mathf.Clamp(updatedPos.x, xClamp * -1 + playerWidth, xClamp - playerWidth);
        transform.position = updatedPos;
    }

    // Kill player on contact with terrain
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!collisionOccured)
        {
            if (other.gameObject.CompareTag("Tiles") || other.gameObject.CompareTag("Enemies") || other.gameObject.CompareTag("Spike"))
            {
                AudioManager.Instance.PlaySound(deathAudio);
                ScoreManager.Instance.EndScore();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                collisionOccured = true;
            }
        }
    }

    // Retrieve ink from object pool, setting as active, then firing in opposite direction
    private void Shoot(Vector2 inkTarget)
    {
        GameObject inkObject = ObjectPool.instance.GetPooledObject();

        // If there is an ink object available, set its position and target then enable it
        if (inkObject != null)
        {
            inkObject.transform.position = transform.position;
            // Adding target variance to our target
            float xNoise = Random.Range(-10, 10);
            //float yNoise = Random.Range(-115, 115);
            inkTarget.x += xNoise;
            //inkTarget.y += yNoise; 
            inkObject.GetComponent<Ink>().SetDirection(inkTarget.normalized);
            inkObject.SetActive(true);
        }
    } 
}
