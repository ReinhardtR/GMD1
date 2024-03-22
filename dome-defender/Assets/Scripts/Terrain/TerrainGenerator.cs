using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Tooltip("The prefab to use for the rocks.")]
    public GameObject RockPrefab;

    [Header("Rocks")]
    public Rock Stone;
    public List<Rock> Ores;

    [Header("Generation Settings")]
    [Tooltip("The size of each chunk in the terrain.")]
    public int ChunkSize = 8;
    public int LeftBedrockX = -8;
    public int RightBedrockX = 8;
    public int BottomBedrockY = -17;
    public int StartY = -1;

    [Header("Noise Settings")]
    [Tooltip("The seed to use for random generation. If 0, a random seed will be used.")]
    public int Seed = 0;
    [Tooltip("Scale of the noise function."), Range(0.01f, 1f)]
    public float NoiseScale = 0.1f;
    [Tooltip("Threshold for spawning veins of ores."), Range(0f, 1f)]
    public float VeinThreshold = 0.5f;

    [Header("Testing")]
    public TerrainData Terrain;

    void Awake()
    {
        if (Seed <= 0)
        {
            Seed = Random.Range(1, 1000000);
        }

        Random.InitState(Seed);

        if (Terrain == null)
        {
            Terrain = ScriptableObject.CreateInstance<TerrainData>();
        }
    }

    public GameObject GenerateChunk(Vector2 chunkCoords)
    {
        if (Terrain.TryGetChunk(chunkCoords, out var existingChunk))
        {
            return existingChunk;
        }

        if (
            chunkCoords.x < LeftBedrockX || chunkCoords.x > RightBedrockX ||
            chunkCoords.y > StartY || chunkCoords.y < BottomBedrockY
        )
        {
            throw new System.Exception("Chunk is out of bounds!");
        }

        GameObject chunk = new($"Chunk ({chunkCoords.x}, {chunkCoords.y})");

        chunk.transform.parent = transform;
        chunk.transform.position = chunkCoords * ChunkSize;

        PlaceRocksInChunk(chunk.transform, chunkCoords);
        Terrain.AddChunk(chunkCoords, chunk);

        return chunk;
    }

    private void PlaceRocksInChunk(Transform chunk, Vector2 chunkCoords)
    {
        float[,] noiseMap = GenerateChunkNoiseMap(chunkCoords);
        for (int x = 0; x < ChunkSize; x++)
        {
            for (int y = 0; y < ChunkSize; y++)
            {
                Rock rock = GetRandomRockType();
                Vector2 position = new(x, y);

                if (noiseMap[x, y] >= VeinThreshold && rock != Stone)
                {
                    PlaceOresInVein(chunk, rock, position);
                }
                else
                {
                    PlaceRock(chunk, Stone, position);
                }
            }
        }
    }

    private void PlaceOresInVein(Transform chunk, Rock rock, Vector2 position)
    {
        // Determine number of ores in the vein (adjust as needed)
        int veinSize = Random.Range(2, 5);
        for (int k = 0; k < veinSize; k++)
        {
            // Adjust offset range based on desired vein size
            int oreX = (int)position.x + Random.Range(-1, 2);
            int oreY = (int)position.y + Random.Range(-1, 2);

            // Ensure placement stays within chunk bounds 
            oreX = Mathf.Clamp(oreX, 0, ChunkSize - 1);
            oreY = Mathf.Clamp(oreY, 0, ChunkSize - 1);

            PlaceRock(chunk, rock, new(oreX, oreY));
        }
    }

    private void PlaceRock(Transform chunk, Rock rock, Vector2 position)
    {
        Vector2 worldPosition = (Vector2)chunk.position + position;
        if (Terrain.HasRockAt(worldPosition))
        {
            return;
        }

        GameObject rockObject = Instantiate(RockPrefab, worldPosition, Quaternion.identity, chunk);
        rockObject.SetActive(false);

        RockController rockController = rockObject.GetComponent<RockController>();
        rockController.Terrain = Terrain;
        rockController.Rock = rock;

        if (rock.DropItem && rock.DropAmount > 0)
        {
            DropsItem dropsItem = rockObject.GetComponent<DropsItem>();
            dropsItem.Item = rock.DropItem;
            dropsItem.Amount = rock.DropAmount;
        }

        rockObject.SetActive(true);

        Terrain.AddRock(worldPosition);
    }

    private float[,] GenerateChunkNoiseMap(Vector2 chunkCoords)
    {
        float[,] noiseMap = new float[ChunkSize, ChunkSize];
        for (int x = 0; x < ChunkSize; x++)
        {
            for (int y = 0; y < ChunkSize; y++)
            {
                noiseMap[x, y] = Mathf.PerlinNoise(
                    (x + chunkCoords.x * ChunkSize) * NoiseScale,
                    (y + chunkCoords.y * ChunkSize) * NoiseScale
                );
            }
        }
        return noiseMap;
    }

    private Rock GetRandomRockType()
    {
        float roll = Random.Range(0f, 1f);
        float total = 0f;
        foreach (Rock rock in Ores)
        {
            total += rock.SpawnChance;
            if (roll <= total)
            {
                return rock;
            }
        }

        return Stone;
    }
}
