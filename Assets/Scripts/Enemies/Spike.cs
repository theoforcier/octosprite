using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private Vector2 spikeDirection;
    private float spikeSpeed = 8f, lifespan = 6f, timer = 0f;

    private void Update()
    {
        if (timer > lifespan)
        {
            Destroy(gameObject);
        }
        else
        {
            timer += Time.deltaTime;
            transform.Translate(spikeDirection * Time.deltaTime * spikeSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Tiles"))
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        spikeDirection = direction;
    }
}
