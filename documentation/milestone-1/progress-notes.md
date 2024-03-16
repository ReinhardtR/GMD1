# Progress Notes

## Work Session 1

**_Initial setup of the project_**
I used the 2D URP template for this project, as it's a 2D Game. The URP was recommended by people online, so I went with that. Not quite sure what the difference is, but usually you want this one.

**_Player movement using the new Unity Input System_**
For now I'm just using WASD and Mouse Buttons. But it shouldn't be too difficult to map to the VIA Arcade Machine with this input system.

**_Simple terrain generator using a Rock prefab_**
Currently it simply generates a bunch of rocks in for loops. I want to have a noise map that puts ores in random position.

**_Collission between player and terrain_**
There was a weird issue where the player would be bobbing between each rock, like it was getting caught on the edge of the collision boxes, even though they were aligned perfectly.

The fix to this was to add a "composite collider" on the terrain object which parents all the rocks. This way, the player would collide with the terrain as a whole, rather than each individual rock.

**_Rotating drill on the player based on movement direction_**
There was an issue with the drill not being aligned correctly with the 8 directions the player can move, it would be slightly offset. I found out this is because of the rotation the physics engine puts on the player when colliding with the ground.

I fixed this by ticking a "Freeze Z rotation" option on the players rigidbody. Now the player can't be rotated by the physics engines collisions. This does mean that the players can't rotate off an edge, when standing near the edge, but I think that's a fair trade-off.

## Work Session 2

**_Health_**
To give the rocks a concept of health, I made a `Health` component. This component has a `maxHealth` and a `currentHealth` variable, and a `TakeDamage` method. This method takes an `int` as a parameter, and subtracts that from the `currentHealth`. If the `currentHealth` is less than or equal to 0, the rock is destroyed.

I wanted to keep this component simple, so that it can be used for other entities in the game, like the player or enemies.

**_Laser_**
I added a laser to the player, which can be fired with the left mouse button. To draw the laser I use a `LineRenderer`, which is a Unity component that draws a line between two points.

To detect collisions I use the `Physics2D.Raycast` method, which returns a `RaycastHit2D` object if it hits something. I then use the `RaycastHit2D` to draw the line.

I use tags to determine if the laser hit a rock, and if so, I called a method on the rock to deal damage. I wrap this in some logic to make the laser have a "fire rate", so the rocks don't get destroyed instantly.

I had some issues with the Raycast not colliding correctly, to debug this I found out you can use the `Debug.DrawLine` method to draw a line in the scene while in play mode. This helped me figure out that the ray was not being cast from the correct position.

A laser has the following properties.

- `damage` is the amount of damage the laser deals to a rock.
- `fireRate` is the amount of time in seconds between each damage trigger.
- `range` is the maximum distance the laser can travel.

## Work Session 3

**_Attempting to use Edge Collider_**
I was not very happy with my solution of using a Raycast, and then manually calling the methods on the rock to deal damage. So I attempted to put a `EdgeCollider2D` on the laser, and then use the `OnTriggerEnter2D` method on the rock to deal damage.

This caused multiple other issues, primarily with colliders hitting themselves, and the laser not actually entering the rock, because it was rendered on the same positions as the laser (which is cut off before the rock).

After some testing and reconsideration, I decided to stick with the Raycast solution, as it was the most reliable and simple solution.

I did improve it a little though, by using the `SendMessage` method, and sending an event to the object the Raycast hit. This way I don't have to manually call the method on the rock, and I can use the same method for other entities in the game.

**_Clean Up_**
I cleaned some of the naming conventions, and moved some logic into smaller component. E.g. the RockController no longer exists (for now), since the "Mineable" logic now has it's own component.

## Work Session 4

**_Hitting Multiple Rocks at Once_**
I'm trying to implement a wide laser, so that it can hit multiple rocks at once. To do this I can't use the `Physics2D.Raycast` method, since that has no width. Instead I'm trying to use the `Physics2D.BoxCastAll` method, which returns an array of `RaycastHit2D` objects.

But I'm having issues drawing the laser straight. This was easy with a Raycast, since the cast was simply straight from the lasers direction, but a BoxCast might hit somewhere else than straight. But I wan't to keep the laser straight. I also want to draw the laser to the furthest hit point, so it's not being cut off my the closes hit point.

I'm also having issues with the BoxCast hitting through layers of rocks. I'm gonna have to look into this in the next work sesion.

## Work Session 5

**_Going back to Raycasts_**
I decided to go back to using Raycasts for the laser, since I couldn't get the BoxCast to work properly. The issue is that the BoxCast doesn't get cut off after colliding, the BoxCat always includes everything in the path.

My solution is to fire multiple raycasts, to be able to hit multiple rocks at once. The amount of raycasts being fired, is calculated based on the width of the laser.

I cast the raycasts in a loop, and calculate which collision is the furthest, and render the position of the laser based on that. To make sure I don't trigger a damage event on the same rock multiple times, I keep a list of rocks that have been hit, and check if the rock has already been hit before triggering the damage event.

**_Rethinking the Movement Input_**
Until this point, the player would simply move in the direction of the Joystick. This meant that when the player wanted to mine upwards, they would have to also move upwards.

This felt a little clunky, and I wanted to find an alternative, and I think I found one.

I've now removed the `Jump` button, and replaced it with a `Boost` button. When the player presses the `Boost` button, the player will move in the current direction. This means that the player can change direction without moving, and it feels a lot better.

To make this work the player no longer has gravity, and will always be floating. To make this make sense, the story is that the planet has zero gravity (for some mysterious reason), and the player has a jetpack. This is why the button is called `Boost`.

**_Migrating away from Unity Input System_**
I've decided to migrate away from the new Unity Input System, and instead use the Input Manager. The new input system is great, but it's a little overkill for this project. The Input Manager is a lot simpler, and seems generaly easier to work with, and there is more documentation on it.

## Work Session 6

**_Unity Event Functions_**

I found plenty of small bugs and issues caused by using the wrong Unity event functions. Specifically the `Awake`, `Start` and `OnEnable` functions. I was unaware of when to use which, and when they were executed. [This article](https://gamedevbeginner.com/start-vs-awake-in-unity/) helped me understand the differences. I also found out that the `OnEnable` function is called every time the object is enabled, which is what I needed when spawning ores with my `TerrainGenerator` script.

**_Spawning Ores_**
I made a `Enumeration` class, that I can use to make enum classes. I made a `RockType` enum, which stores all relevant information for each type of rock.

- `name` is the name of the rock.
- `StartColor` is the color of the rock when it's on full health.
- `EndColor` is the color of the rock when it's on zero health.
- `MaxHealth` is the maximum health of the rock.
- `SpawnChance` is the chance of the rock spawning in the terrain.
- `DropAmount` is the amount of ores the rock drops when destroyed.

This way all the information of the enum is stored in the enum class, and not scattered around the components. I really liked this approach, and I think I will use it for items in the game.

I made the `TerrainGenerator` spawn ores based on the `SpawnChance` of the `RockType` enum. I also made the `Mineable` component have a `RockType` variable. So the health and color of the rock can be set based on the `RockType` enum.

I still need to implement the dropping of ores, and the player picking them up. But I think I will do that in the next work session.

## Work Session 7

**_Dropping Ores_**
I made an `ItemType` enum class, that contains all the items that can be on the ground, including the ores from mining rocks.

I've made an Item prefab, that has a `SpriteRenderer`, a `BoxCollider2D` and a `Rigidbody2D`. The `Rigidbody2D` is set to kinematic, so the item doesn't fall to the ground.

I've made a `ItemSpawner` helper class to spawn items, which can be used by other components. Since the `ItemSpawner` is not in the scene, and can't be passed the prefab through the inspector, I use `Resources.Load` to get a reference of the prefab. This requires the `Item` prefab to be in a folder called `Resources`.

I've made a `Pickupable` component, which on collision triggers destroys the game object.

**_Picking Up Ores_**
I've made a `PlayerInventory` component to store the items in the players inventory. The `PlayerInventory` has a `Dictionary` of `ItemType` and `int`, which stores the amount of each item in the inventory.

I've added a weight value to each item in the `ItemType` enum class, which will be used to calculate the players inventory weight.

I've made the `Pickupable` component, add the items to the players inventory when the player collides with the item. I identify that the player is the collider by using tags.

## Work Session 8

**_Implementing the Weight Mechanic_**
I've added a max weight to the player inventory, this is current just a private `SerializeField` variable. But I will probably want to be able to upgrade this in the shop. This might require it to be pubic. I've not decided on a strategy to create upgrades yet.

I've added a penalty to the player speed based on the total weight of the items in the player inventory. This is to make the player think about what they pick up, and not just pick up everything.

**_Removing the Weight Mechanic_**
I decided to remove the weight of the inventory, since it was a an annoying micro management feature, and didn't add much to the game. It was enough for the player to think about the time when mining, and the time it takes to mine the ores. So they player still have to think about what they take the time to mine and pickup, but players who have very efective mining strategies won't be limited by their weight.

**_Placeholder UI_**
Made some placeholder UI text to tell the player what is in their inventory. And also a text element to tell the user their speed. Which can be changed in the future by upgrades.

## Work Session 9

**_Chunking Terrain_**
I've decided to make a chunking system, since the plan is to procedurally generate the terrain. For this to be performant, I need to chunk the terrain, so that I don't have to generate the entire terrain at once.

To do this I had to add a bunch of logic into the `TerrainGenerator`. I made a bunch of iterations on this, but now have a working solution.

I store the chunks in a HashMap for easy look up based on chunk position. This makes it easy to look up, when doing chunk loading.

I've made a `ChunkManager` component, that handles the loading and unloading of chunks. It takes the players position, and calculates the currenct chunk the player is in, and then enables surrounding chunks based on render distance (defined in chunks).

To optimize this I only check when the player has moved half a chunk size.

**_Ore Veins and Perlin Noise_**
To make more predictable ore spawning, I wanted to make ore veins. To decide where to place these veins I generate a perlin noise map for each chunk. I then use this map to decide where to place the ore veins.

I then randomly choose an ore type to place in the vein, and then place the ore in the terrain.

To make sure I don't place rocks on the same position twice, which can happen since veins are randomly sized, I keep a list of positions that have been used. If the position is already in the list, I skip it.

The random vein positions are clamped by the chunk size, so veins are cut off if they hit a chunk border. Minecraft does this as well, and I think it looks good, and it's a relatively simple solution.

## Work Session 10 + 11

**_Migrating to ScriptableObjects and other Refactors_**
After watching talks and videoes abotu ScriptableObjects, I have decieded to stop using enums for the RockTypes and ItemTypes, and instead use ScriptableObjects.

It felt more in the spirit of Unity, more flexible and performant. And honestly just wanted to try it out. And I think it's a good idea to learn how to use them, since they are so powerful.

I've made a `Inventory` ScriptableObject, which contains a `Dictionary` of `Item` and `int`. This is the same as the `PlayerInventory` component, but now it's a ScriptableObject.

I've made the `Pickupable` component into a `Collectible` component, which takes a `Item` ScriptableObject as a parameter, and has a `Collect` method, which destroys the object and calls an event.

In addition to this I made a `Collector` component, which looks for collisions with the `Collectible` component, and adds the item to an `Inventory`, if an `Inventory` is given in the inspector.

I like this separation of concerns, and I think it makes the code more readable and maintainable.

**_Making it pretty_**

I have found some assets for free on the internet that I can use for my rocks and ores.

I want the rocks that have no adjacent rocks to use a different sprite. This means that the rocks have to know if there are rocks adjacent, so they can update their sprite accordingly.

Since the rocks use world position, and have a size of 1, I can simply use a `HasRockAt` method on the `Terrain`, which takes a position, and returns a boolean if there is a rock at that position.

```cs
bool isRockAbove = Terrain.HasRockAt(transform.position + Vector3.up);
bool isRockBelow = Terrain.HasRockAt(transform.position + Vector3.down);
bool isRockLeft = Terrain.HasRockAt(transform.position + Vector3.left);
bool isRockRight = Terrain.HasRockAt(transform.position + Vector3.right);
```

To know when to update this sprite, I have a `OnRockRemoved` event on the `Terrain` class, which is called when a rock is removed. This event is called in the `RemoveRock` method, which is called when a rock is destroyed.

Since there is a lot of rocks enabled at the same time I wanted to optimize this, so I only update rocks adjacent to the removed rock.

Since I know the position of the removed rock, I can calculate the distance from the rock being updated to the removed rock. If this is over 1, it's not adjacent, and I can skip updating the sprite.

```cs
float distance = Vector2.Distance(removedRockPosition, transform.position);
if (distance > 1f)
{
    return;
}
```

## Work Session 12

I have made a Shader and Material that adds a randomly generated cracking texture on top of the rock textures, based on the health of the rock. This gives the player a visual indication of the health of the rock.
