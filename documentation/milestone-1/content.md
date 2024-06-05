# Introduction

The first milestone of my Game is the Movement and Mining System. This includes the following:

- Terrain Generation
- Player Movement
- Laser Controlling
- Laser Mining
- Chunk Loading
- Resource Spawning
- Item Collection
- Inventory System

Since my milestones are too large for a single blog post, I will only be covering the parts I find most interesting/unique about my game development process.

# Terrain Generation

I started with a simple implementation of the Terrain Generation. Simply creating a 2D array of blocks, in a nested loop.

The blocks were a prefab I called _Rock_. The _Rock_ prefab was a simple square, with collision and texture.

This worked fine until I wanted to create a bigger terrain, then the game got laggy. I realized that I was creating too many blocks, and that I needed to create a system that only created the blocks that were visible to the player.

## Chunks

I needed to implement a system that was able to procedurally create the blocks that were visible to the player, and hide the blocks that were not visible.

So being inspired by Minecraft, I decided to create a Chunk Manager. The terrain gets split into 8x8 chunks. The Chunk Manager tracks the position of the player, the current chunk the player is in, and then enables/disables chunks based on the player's position. If the player moves beyond exising chunks it calls the Terrain Generator to create new chunks.

This is the method that manages the chunks:

```csharp
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
```

The method does a lot of calculations so to optimize, the method is only called if the player has moved a certain distance, or if the render distance has changed.

```csharp
bool hasRenderDistanceChanged = UpdateRenderDistance();
bool hasMovedConsiderably = Vector2.Distance(Player.transform.position, lastPlayerPos) >= generator.ChunkSize / 4;
if (hasMovedConsiderably || hasRenderDistanceChanged)
{
    lastPlayerPos = Player.transform.position;
    ManageChunks();
}
```

The render distance might change, by a future setting, or when controlling the tower.

This greatly improved the performance of the game in larger terrains.

## Spawn Chunks

Another thing I had to consider is that when the player moved far away from the tower, the chunks around the tower aren't loaded. This means that when enemies spawn they will fall through the terrain. To fix this I created a field that stores the location of the "spawn chunks", inspired by Minecraft, which will always be loaded.

## Ores

The terrain of course needed ores. Inspired by Minecraft, again, I wanted to have a vein like generation of ores, with a random distribution that felt natural.

To do this I generated a Perlin Noise map for each chunk, and based on the values I checked it against a threshold to determine if a vein should spawn there. If it did, I would pick a random ore (based on the spawn chances of the ores). Then I would create a vein-like shape by placing a random number of ores adjacent to the original position. The vein would be clamped to chunk, so that it wouldn't go outside the chunk, similar to in Minecraft.

Here is a code snippet of the method:

```csharp
private void PlaceOresInVein(Transform chunk, Rock rock, Vector2 position)
{
    // Determine number of ores in the vein
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
```

# Laser Mining

I spent a lot of time adjusting and fine-tuning the laser mining system, but I don't have a lot of characters left to talk about it 😅, so I will summarize a bit.

The laser detects collisions using multiple raycasts, spread evenly across the width of the laser. This allows the laser to mine multiple rocks at once.

The laser gets rendered by a line renderer, which I do a lot of math to determine when the line should end, since the laser has a set range, but should stop when hitting a rock.

I also implement "fire rate" functionality here, to only deal damage to the rocks every x seconds. Since I wanna be able to hit multiple rocks at once, I store the instance ID of the rocks the lasers hit, so I can prevent the same rock from taking damage multiple times in the same iteration.

It was a challenge to do all the vector math to get the laser feeling right, mostly because I wanted to be able to hit multiple rocks at once, but I'm happy with the result.
