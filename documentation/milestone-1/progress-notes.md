
# Progress Notes

***Initial setup of the project***
I used the 2D URP template for this project, as it's a 2D Game. The URP was recommended by people online, so I went with that. Not quite sure what the difference is, but usually you want this one.

***Player movement using the new Unity Input System***
For now I'm just using WASD and Mouse Buttons. But it shouldn't be too difficult to map to the VIA Arcade Machine with this input system.

***Simple terrain generator using a Rock prefab***
Currently it simply generates a bunch of rocks in for loops. I want to have a noise map that puts ores in random position.

***Collission between player and terrain***
There was a weird issue where the player would be bobbing between each rock, like it was getting caught on the edge of the collision boxes, even though they were aligned perfectly.

The fix to this was to add a "composite collider" on the terrain object which parents all the rocks. This way, the player would collide with the terrain as a whole, rather than each individual rock.

***Rotating drill on the player based on movement direction***
There was an issue with the drill not being aligned correctly with the 8 directions the player can move, it would be slightly offset. I found out this is because of the rotation the physics engine puts on the player when colliding with the ground.

I fixed this by ticking a "Freeze Z rotation" option on the players rigidbody. Now the player can't be rotated by the physics engines collisions. This does mean that the players can't rotate off an edge, when standing near the edge, but I think that's a fair trade-off.

