using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public AudioClip bombAudio;

    private AudioSource effectSource;
    private int bombRange = 3;

    private void Start() {
        effectSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>().effectsSource;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Play sound
        AudioManager.Instance.PlaySound(bombAudio);
        Explode();
    }

    private void Explode()
    {
        // Detect tiles in range and destroy them
        Collider2D[] hitTiles = Physics2D.OverlapCircleAll(transform.position, bombRange);
        foreach (Collider2D enemy in hitTiles)
        {
            // Destroy gameobjects
            if (enemy.gameObject.CompareTag("Tiles") || enemy.gameObject.CompareTag("Bombs"))
            {
                Destroy(enemy.gameObject);
            }
        }

        // Update score
        ScoreManager.Instance.AddScore(1000);
    }
}
