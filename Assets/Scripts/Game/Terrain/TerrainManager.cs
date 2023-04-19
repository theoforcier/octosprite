using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates 2D Tilemaps Programatically
public class TerrainManager : MonoBehaviour
{
    public GameObject tilePrefab, bombPrefab;
    public MapGenerator mapGen;
    public Camera mainCam;

    private GameObject player;
    private int randomSeed;
    private bool spawnBomb = true;
    private int mapHeight = 0;

    private void Awake()
    {
        // Create seed
        randomSeed = Random.Range(0, 1000);
        // Create initial tilemap
        CreateTilemap(randomSeed);
        // Get player reference
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        // If player is reaching tilemap limits, create new tilemap and allow another bomb to be spawned
        if (player.transform.position.y >= mapHeight - 100)
        {
            CreateTilemap(randomSeed);
            spawnBomb = true;
        }
    }

    // Create tilemap according to difficulty
    private void CreateTilemap(int seed)
    {
        // Generate noise
        float[,] noiseMap = mapGen.GenerateTerrainNoise(seed, mapHeight);
        GameObject chunk = new GameObject("Chunk");
        chunk.transform.SetParent(gameObject.transform);

        // Create tile for each noiseMap position where height > waterLevel
        for (int y = 0; y < noiseMap.GetLength(1); y++)
        {
            for (int x = 0; x < noiseMap.GetLength(0); x++)
            {
                float currentHeight = noiseMap[x, y];
                // Spawn bombs at specific height
                if (currentHeight > 0.97 && spawnBomb)
                {
                    GameObject bomb = Instantiate(bombPrefab, new Vector3(x - noiseMap.GetLength(0) / 2, y + mapHeight, 0), Quaternion.identity);
                    bomb.transform.SetParent(chunk.transform);
                    // Spawn only 1 per chunk
                    spawnBomb = false;
                }
                // Spawn regular terrain
                else
                {
                    for (int i = 0; i < mapGen.regions.Length - 1; i++)
                    {
                        if (currentHeight > mapGen.regions[i].height)
                        {
                            GameObject tile = Instantiate(tilePrefab, new Vector3(x - noiseMap.GetLength(0) / 2, y + mapHeight, 0), Quaternion.identity);
                            tile.transform.SetParent(chunk.transform);
                            tile.GetComponent<SpriteRenderer>().color = mapGen.regions[i+1].color;
                        }
                    }
                }
            }
        }

        // Update map height
        mapHeight += noiseMap.GetLength(1);
    }
}
