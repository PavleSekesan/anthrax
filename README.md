# Anthrax
  
Anthrax is a real-time ant colony strategy game developed in Unity.
  
## Game mechanics
  
### Food

![food](https://i.imgur.com/9RwcnpS.png)

Food acts as a basic resource in the game. It is used to construct nests and build worker and warrior ants.
Food is spawned randomly around the screen and can be collected by worker ants.

### The nest

![nest](https://i.imgur.com/1KMsHNl.png)

The colony consists of a system of nests. Each nest defines its territory boundary that is a circle.   
Ants of two different types can be spawned from the selected nest.
At the start of the game, each player owns one nest, and more can be built for 10 food by worker ants

### The worker ant

![worker](https://i.imgur.com/6Vwb5OC.gif)

The worker ant is the basic unit of the game that can collect food ant and build new nests.
It costs 3 food to build a worker from the selected nest.
At the start of the game 5 worker ants are automatically spawned.
Worker ants move randomly around the screen searching for food.
When the food gets into its sensor range, the worker collects it and returns it back to the nest.

When nest construction is ordered by the player by moving the cursor to the desired location,
the closest ant to that construction location will move towards it, stop at the location and
build the nest in a couple of seconds.

Worker ants' movement is restricted to their allied territory, and they bounce when they collide
with the territory boundaries.

### The warrior ant

![warrior](https://i.imgur.com/Q04D63g.gif)

The warrior ant is the fighting unit of the game that can attack enemy worker and warrior ants.
It costs 5 food to build a warrior ant from the selected nest.
Warrior ants move randomly around the screen (slightly faster than worker ants) searching for enemies.
When an enemy gets into its sensor range, the warrior ant charges towards it in an attempt to hit it.
Afterwards, if it gets into attack range, the warrior ant deals random damage that cannot exceed a 
predetermined maximal value.
After attacking, the warrior must stand still for a couple of moments to recover.

Warrior ants' movement is restricted only to territory owned by any player, and they bounce when they collide
with the territory boundaries.

## Controls

### Player 1

**Move cursor** - W, S, A, D

**Cycle selected nest** - Tab

**Spawn worker from selected nest** - Z

**Spawn warrior from selected nest** - X

**Build new nest at cursor location** - C

### Player 2

**Move cursor** - Up, Down, Left, Right

**Cycle selected nest** - Right Shift

**Spawn worker from selected nest** - I

**Spawn warrior from selected nest** - O

**Build new nest at cursor location** - P
