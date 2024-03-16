using System;
using System.Collections.Generic;
using UnityEngine;

public class Terrain
{
    public static event Action<Vector2> OnRockChangedEvent;

    private readonly static Dictionary<Vector2, GameObject> chunks = new();
    private readonly static HashSet<Vector2> rocks = new();

    public static void AddChunk(Vector2 chunkCoords, GameObject chunk)
    {
        chunks.Add(chunkCoords, chunk);
    }

    public static GameObject GetChunk(Vector2 chunkCoords)
    {
        return chunks.GetValueOrDefault(chunkCoords);
    }

    public static bool HasChunkAt(Vector2 chunkCoords)
    {
        return chunks.ContainsKey(chunkCoords);
    }

    public static bool TryGetChunk(Vector2 chunkCoords, out GameObject chunk)
    {
        return chunks.TryGetValue(chunkCoords, out chunk);
    }

    public static void AddRock(Vector2 position)
    {
        rocks.Add(position);
        OnRockChangedEvent?.Invoke(position);
    }

    public static bool HasRockAt(Vector2 position)
    {
        return rocks.Contains(position);
    }

    public static void RemoveRock(Vector2 position)
    {
        rocks.Remove(position);
        OnRockChangedEvent?.Invoke(position);
    }
}
