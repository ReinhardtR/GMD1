# Progress Notes

## Work Session 1

**_Cleaning up the Mining System_**

I was not too happy with my static classes and singleton-esque data objects.

So I've removed the `ItemSpawner` class and instead made a `ItemFactory` component. It simply stores the logic for creating items and has a public method to spawn them.

I've decided to make it a singleton, since the scale of this game isn't large enough to warrant a more complex system, for now.

I've made a `TerrainData` ScriptableObject, to hold the data for the terrain. This is a much better solution than the previous one, where I had a static class with a bunch of public fields. This allows for having a testing terrain, and a real terrain, and easily switching between them.
