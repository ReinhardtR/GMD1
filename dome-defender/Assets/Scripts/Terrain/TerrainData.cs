using System;
using System.Collections.Generic;
using UnityEngine;

public class TerrainData : ScriptableObject
{
    public event Action<Vector2> OnRockChangedEvent;

    private readonly Dictionary<Vector2, GameObject> chunks = new();
    private readonly HashSet<Vector2> rocks = new();

    public void AddChunk(Vector2 chunkCoords, GameObject chunk)
    {
        chunks.Add(chunkCoords, chunk);
    }

    public GameObject GetChunk(Vector2 chunkCoords)
    {
        return chunks.GetValueOrDefault(chunkCoords);
    }

    public bool HasChunkAt(Vector2 chunkCoords)
    {
        return chunks.ContainsKey(chunkCoords);
    }

    public bool TryGetChunk(Vector2 chunkCoords, out GameObject chunk)
    {
        return chunks.TryGetValue(chunkCoords, out chunk);
    }

    public void AddRock(Vector2 position)
    {
        rocks.Add(position);
        OnRockChangedEvent?.Invoke(position);
    }

    public bool HasRockAt(Vector2 position)
    {
        return rocks.Contains(position);
    }

    public void RemoveRock(Vector2 position)
    {
        rocks.Remove(position);
        OnRockChangedEvent?.Invoke(position);
    }
}
