# Progress Notes

## Tutorials used

Wave System: https://www.youtube.com/watch?v=Vrld13ypX_I
Audio Manager: https://www.youtube.com/watch?v=6OT43pvUyfY

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

## Work Session 3

**_Enemy_**

I've made a an enemy controller, that handles the movement logic of enemies.

It executes the movement of the enemy and handles animator state.

I have created an Animation Controller with transitioning states based on movement.

Currently has a working idle and walk animation.

Gonna add a hit, death and hurt animation.

# Work Session 4

**_Attack_**

I've now added an attack animation and logic to the enemy.

I've added transitions between the animation states in the animator, with any state -> attack, to ensure that the attack is played no matter what the enemy is doing.

This then transitions back to the idle state.

I've added some gizmos to debug the attack range of the enemy.

I trigger walking animations by doing animator.setBool("isWalking", true) and animator.setBool("isWalking", false) to stop the walking animation.

I trigger attack animation by uing a trigger parameter, animator.SetTrigger("Attack"). This will play the attack animation and then transition back to idle.

I decided not to use the Hurt animation, and instead flash the sprite with a red color, so it seems like the enemy is hurt.

This makes the enemy feel more responsive and less clunky. As it can flash red and perform any animations at the same time.

The EnemyController is doing too much, it would be optimal to split the behaviour out if wanting to expand the game. But currently it doesn't cause any problems, but I'm aware of it.

# Work Session 5

**_Wave System_**

I could expand on the wave system allowing multiple enemy types to spawn in a wave. But I currently only have one enemy type, so I will leave it as is.

**_Spawning Enemies_**

I had a problem where if the player was not in the base chunks when enemies were spawning, the enemies would fall through since there is no chunk to stand on.

Then I thought I could constrain the enemy to the ground level, but I don't like that solution since that is very coupled to what ground level is. And it also wouldn't solve the problem of my "is grounded" check via. raycasts also not working when no chunks is under the enemy. And this check is important so that enemies that walk know when they can walk.

Another solution I thought of is to always have a set of chunks around the base always loaded. I thought of this solution because I remember Minecraft doing something similar, where the spawn chunks are always loaded. It's not very resource intensive and it solves all my problems.

I implement this in my chunk manager by giving a list of "spawn chunks" that will always be loaded. All spawn chunks are activated initially and the method that toggles chunks now checks if it's a spawn chunk, and skips the toggling if it is.

**_Projectiles_**

To optimize projectiles I could have used object pooling, but I don't think it's necessary since there isn't that many projectiles on the screen at once.

**_SFX_**

I added a AudioManager singleton component on my GameManager, that is used to play sounds that can't be directly connected to a specific game objects life cycle. E.g. theme music, UI sounds, etc.

I tried adding a sound effect to my projectile by adding a Audio source component, since that is only a sound effect played once on awake. But when the projectile got destroyed before the sound was done playing, it sounded like the sound was cut off.

So I'm instead using the AudioManager to play the sound.
