# Progress Notes

## Work Session 1

**_Cleaning up the Mining System_**

I was not too happy with my static classes and singleton-esque data objects.

So I've removed the `ItemSpawner` class and instead made a `ItemFactory` component. It simply stores the logic for creating items and has a public method to spawn them.

I've decided to make it a singleton, since the scale of this game isn't large enough to warrant a more complex system, for now.

I've made a `TerrainData` ScriptableObject, to hold the data for the terrain. This is a much better solution than the previous one, where I had a static class with a bunch of public fields. This allows for having a testing terrain, and a real terrain, and easily switching between them.

## Work Session 2

**_Tower_**

I made a game object for the tower. It's simply a cricle with a collider.

I made a control center (also just a circle), that the player can walk into and press the _Interact_ button to lock into the control center.

This will disable normal player movement, and instead control the tower.

I made the camera lock to the tower and zoom out, to give the player a better overview of the enemies when defending.

**_Thoughts_**

I currently have tower and player input interactions in the PlayerController. I would like a better solution for this where the input interactions are split out in its own classes. But for now I will stick to this since there is more important features to implement.

I also think my player, camera and tower is coupled too much. I would like to decouple them with ScriptableObject variables, that the components can listen to changes on.

**_Bedrock_**

I made a bedrock layer around the edges of the terrain to stop the player from moving out of the terrain boundaries.

I remove prevent the generator from placing bedrock right under the base, so the player has an "entrance" to the ground.
