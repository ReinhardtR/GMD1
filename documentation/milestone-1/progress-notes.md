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

To detect collisions I use the `Physics2D`.Raycast method, which returns a `RaycastHit2D` object if it hits something. I then use the `RaycastHit2D` to draw the line.

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
