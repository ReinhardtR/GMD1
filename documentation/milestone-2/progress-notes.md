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
