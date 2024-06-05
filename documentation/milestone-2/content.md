# Introduction

The second milestone of my Game is the Base and Enemies System. This includes the following:

- Base Health
- Base Weapon
- Base Weapon Controlling
- Base Weapon Shooting
- Enemy Spawning
- Enemy Movement
- Enemy Health
- Wave System

Since my milestones are too large for a single blog post, I will only be covering the parts I find most interesting/unique about my game development process.

# The Base

The actual base (a.k.a: the tower or the dome) is in it's current implementation simply a circle that the player can move around in, and the enemies try to attack from the outside.

To make sure that the player could move into the sphere from the bottom, but that no one could move through the sphere at the top. I used and Edge Collider 2D, that allowed me to draw a line around the top of the sphere, blocking anyone from moving through the top, while leaving the bottom open.

For the weapon of the base, I have created a rectangle that sits on top of the base, and that can be rotated by the player, with the joystick, when the player is controlling the base. The weapon rotates around the center of the sphere, so it looks like it's attatched to the base. To ensure that the player can't rotate the weapon too far, I have limited the rotation to 90 degrees in each direction.

When the player is controlling the base, the camera gets zoomed out so the player has a better overview of the enemies moving towards the base. This was also to allow for flying enemies to be in view, but I don't plan to implement these in this MVP.

Currently the logic for taking control of the tower is handled in my Player Controller. Ideally this would be separated in a different script, since the Player Controller is messy with a lot of if statements at the moment. I initially did this to ensure that the player wouldn't move around when the tower was being controlled.

Although, the actual logic for the tower logic is in a Tower Controller.

The Tower Controller has a reference to it's own Health component. It uses an event in the Health component, to be notified of when the tower is dead. When this happens, the Tower Controller will notify the GameManager that the game is over. You could reverse this and make the GameManager listen to the towers health, I'm not sure which is the best practice.

When the game is over it simply goes to the Main Menu scene. This could of course be improved for a better UX.

When enemies move up right next to the base, the projectile shooting out of the weapon won't be able to hit the enemies. So instead the player has to use melee. Melee is simply a collider on the weapon, that when moved up and down will trigger damage on the enemies.

The Tower Weapon Controller checks for all collisions, with the "Enemy"-tag, in the box collider of the weapon and it deals damage to all enemies in the box. But it also has a "fire rate".

The projectile is a generic component that check for collisions with the enemey tag, and deals damage to the enemy health component, which means it can be reused.

The projectile gets destroyed when it moves off screen for more than 1 second, to avoid lagging the game with too many projectiles. I keep it for 1 second so that It can possibly hit enemies that gets out of view, when the player exits the tower.

# The Enemies

For the enemies I created a Enemy Controller that admittedly handles too many things, and should be split out. My intention was to simply get it working, and then split it out when I needed to create more enemy types, but I never got to that point in the time frame.

The most interesting parts of the implementation is the approaching of the base and the animations.

The Enemy Controller finds a gameobject with tag "Tower", which they will try to approach and attack.

To check if the enemey should walk towards the target, I calculate the distance between the enemy and the target. If the distance is greater than the range of the enemy, and the enemy is grounded (meaning it's not falling or jumping), the enemy will move towards the target.

```csharp
ColliderDistance2D targetDistance = Physics2D.Distance(targetColl, coll);

bool shouldWalk = targetDistance.distance >= range && isGrounded;

rb.velocity = shouldWalk
    ? new Vector2(targetDistance.normal.x, 0) * speed
    : new Vector2(0, rb.velocity.y);

animator.SetBool("IsWalking", shouldWalk);
spriteRenderer.flipX = targetDistance.normal.x <= 0;
```

The script sets the velocity of the enemy, to either move towards the target, with a given speed. Or it will just persist the vertical velocity, to continue falling or jumping.

It then set's a boolean in the animator, to play the walking animation, and flips the sprite so it looks like the enemy is walking in the right direction. This is a simple trick to make the enemy look like it's walking towards the target, without having to create multiple sprites for each direction.

A separete method checks if it's in range to attack, and it then sets plays the attack animation with the `SetTrigger` method, since it should only play once.

To indicate to the player when the enemy takes damage, I have made a coroutine that flashes the sprite red for 100ms, which gets called when the health damage event is triggered.

I do this by simple changing the color of the sprite, since that will tint the sprites:

```csharp
IEnumerator FlashRed()
{
    spriteRenderer.color = Color.red;
    yield return new WaitForSeconds(0.1f);
    spriteRenderer.color = Color.white;
}
```

I like this implementation since it works with any sprite, and doesn't require any additional sprites to be created.
