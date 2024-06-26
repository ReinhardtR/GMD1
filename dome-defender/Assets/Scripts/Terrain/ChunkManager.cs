using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public GameObject Camera;
    public GameObject Player;
    [Tooltip("These chunks will always be loaded.")]
    public Vector2[] spawnChunks;

    private Camera playerCamera;
    private int renderDistance;
    private TerrainGenerator generator;
    private Vector2 lastPlayerPos;
    private HashSet<GameObject> activeChunks;

    void Awake()
    {
        generator = GetComponent<TerrainGenerator>();
        playerCamera = Camera.GetComponent<Camera>();
    }

    void Start()
    {
        activeChunks = new();
        lastPlayerPos = Player.transform.position;
        ActivateSpawnChunks();
        UpdateRenderDistance();
        ManageChunks();
    }

    void Update()
    {
        bool hasRenderDistanceChanged = UpdateRenderDistance();
        bool hasMovedConsiderably = Vector2.Distance(Player.transform.position, lastPlayerPos) >= generator.ChunkSize / 4;
        if (hasMovedConsiderably || hasRenderDistanceChanged)
        {
            lastPlayerPos = Player.transform.position;
            ManageChunks();
        }
    }

    private bool UpdateRenderDistance()
    {
        float prev = renderDistance;

        renderDistance = Mathf.CeilToInt(playerCamera.orthographicSize * 2 / generator.ChunkSize);

        return prev != renderDistance;
    }

    private void ManageChunks()
    {
        // Toogle chunks that are no longer in the render distance
        for (int i = activeChunks.Count - 1; i >= 0; i--)
        {
            ToggleChunk(activeChunks.ElementAt(i));
        }

        // Procedurally generate or activate chunks in the render distance
        (IEnumerable<int> xRange, IEnumerable<int> yRange) = GetChunkRanges(renderDistance);
        foreach (int x in xRange)
        {
            foreach (int y in yRange)
            {
                Vector2 chunkCoords = new(x, y);
                GameObject chunk = generator.Terrain.GetChunk(chunkCoords);

                if (chunk == null)
                {
                    GameObject newChunk = generator.GenerateChunk(chunkCoords);
                    ActivateChunk(newChunk);
                    continue;
                }

                ActivateChunk(chunk);
            }
        }
    }

    private void ActivateSpawnChunks()
    {
        foreach (Vector2 chunkCoords in spawnChunks)
        {
            GameObject chunk = generator.Terrain.GetChunk(chunkCoords);

            if (chunk == null)
            {
                chunk = generator.GenerateChunk(chunkCoords);
            }

            ActivateChunk(chunk);
        }
    }


    private void ToggleChunk(GameObject chunk)
    {
        if (spawnChunks.Contains(chunk.transform.position / generator.ChunkSize))
        {
            return;
        }

        float distanceToChunk = GetShortestDistanceToChunk(chunk);
        bool inRenderDistance = distanceToChunk <= generator.ChunkSize * renderDistance;

        if (inRenderDistance)
        {
            ActivateChunk(chunk);
        }
        else
        {
            DeactivateChunk(chunk);
        }
    }

    private void DeactivateChunk(GameObject chunk)
    {
        chunk.SetActive(false);
        activeChunks.Remove(chunk);
    }

    private void ActivateChunk(GameObject chunk)
    {
        chunk.SetActive(true);
        activeChunks.Add(chunk);
    }

    private float GetShortestDistanceToChunk(GameObject chunk)
    {
        Vector2 chunkTopLeft = chunk.transform.position;
        Vector2 chunkTopRight = chunkTopLeft + Vector2.right * generator.ChunkSize;
        Vector2 chunkBottomLeft = chunkTopLeft + Vector2.down * generator.ChunkSize;
        Vector2 chunkBottomRight = chunkTopLeft + Vector2.right * generator.ChunkSize + Vector2.down * generator.ChunkSize;

        return Mathf.Min(
            Vector2.Distance(lastPlayerPos, chunkTopLeft),
            Vector2.Distance(lastPlayerPos, chunkTopRight),
            Vector2.Distance(lastPlayerPos, chunkBottomLeft),
            Vector2.Distance(lastPlayerPos, chunkBottomRight)
        );
    }

    private (IEnumerable<int> xRange, IEnumerable<int> yRange) GetChunkRanges(int distance)
    {
        // Get the player's chunk position
        int playerX = Mathf.FloorToInt((lastPlayerPos.x - transform.position.x) / generator.ChunkSize);
        int playerY = Mathf.FloorToInt((lastPlayerPos.y - transform.position.y) / generator.ChunkSize);

        // Get the range of chunks to render
        int minX = Mathf.Clamp(playerX - distance, generator.LeftBedrockX, generator.RightBedrockX);
        int maxX = Mathf.Clamp(playerX + distance, generator.LeftBedrockX, generator.RightBedrockX);
        int minY = Mathf.Clamp(playerY - distance, generator.BottomBedrockY, generator.TopBedrockY);
        int maxY = Mathf.Clamp(playerY + distance, generator.BottomBedrockY, generator.TopBedrockY);

        IEnumerable<int> xRange = Enumerable.Range(minX, maxX - minX + 1);
        IEnumerable<int> yRange = Enumerable.Range(minY, maxY - minY + 1);

        return (xRange, yRange);
    }
}
