# Farm Tower TD Defense 

## Team Members 

Teddy Starynski – https://github.com/ReddishBlue 

Brady Bock - https://github.com/bbock27 

Lucy Malmud – https://github.com/lmalmud 

## Game Summary 

You are a farmer on a frontier world settling the land. However, there are hungry monsters lurking in the shadows waiting to eat your crops and livestock. But the land is really good for growing crops. Get money by farming during the daytime and buying towers to defend your land from the monsters at night. You’ll have to manage your time wisely, fight bravely, and act strategically to ensure the survival of yourself and your crops. 

## Genres 

This will be a tower defense game that is played from the first-person perspective. It will include both strategy (in the tower defense component) and simulation (in the farming aspect) 
- Tower defense 
- First person
- Farming sim 
- Strategy 

## Inspiration 

### Rogue Tower 

Rogue Tower is a roguelike tower defense game, where the map is expanded procedurally every wave. Specifically, the inspiration we took from this game comes from the game mode “Touch Grass (April Fools)”, where the game is played in first person. While this game mode was created as a joke in Rogue Tower, it can be pretty fun and is an interesting way to play a tower defense game. 

![screenshot of rogue tower first person mode](GameDesignDocScreenshots/rogueTowerScreenshot.png) 

### Love Death and Robots: Suits 

Suits is an episode from the Netflix show Love Death and Robots, where a group of farmers have mech suits that they pilot to defend their land from alien invaders. The subject matter of our game was inspired by this episode of the show! 

![Love, Death, and Robots Image](https://images.squarespace-cdn.com/content/v1/5b3cee29697a98bbfb322033/1602565009284-23POBIW9CNGPXZTBS53B/2020-10-10_11h39_37.png?format=1500w) 

### Thronefall 

We really liked the art style of this game, and how bold the colors are. It’s a tower defense game, so some inspiration can also be drawn from the level design and mechanics. The player is a character that roams around in this game, instead of just an omniscient mouse that can place stuff down, so this is a good game to look at for making a tower defense game where the player character is an actual character in the world. 

![thronefall image1](GameDesignDocScreenshots/thronefall1.png) 

![thronefall image2](GameDesignDocScreenshots/thronefall2.png) 


### Bad North 

Another game that we like the art style of. Both Bad North and Thronefall have low-poly art styles and nice, soft lighting. The designs are very simplistic, but effective. 

![bad north image1](GameDesignDocScreenshots/badNorth1.png) 

![bad north image2](GameDesignDocScreenshots/badNorth2.png) 
 

### Minecraft 

One of our favorite parts of Minecraft was the farming- providing a peaceful, routine activity that integrated into the rest of the gameplay, yet has a different feel to some of the other activities players partake in. We are inspired by these mechanics, as well as some of the textures, designs, and properties of livestock and farming. 

![Minecraft Farming Image](https://images.surferseo.art/4d7c9bab-c8e4-427a-8b2f-b9d3147e24aa.png) 


### Plants vs. Zombies 

We’re taking inspiration from the sun and sunflowers mechanic. We plan to have plants that generate income (your farm), and the monsters will first destroy those plans, causing you to lose income. However, you can lose those plants and it won’t be game over yet (much like how you can lose your sunflowers in PvZ without a game over). However, if you let your plants get eaten, you lose out on income.  


## Gameplay 

A paragraph or bulleted list describing how the player will interact with the game, and the key gameplay mechanics that you plan to have implemented in your finalized game. Also use this section to broadly describe the expected user interface and game-controls. 

**Core mechanics required:** 
- Day-night cycle 
    - During the daytime, the player is free from attacking enemies. They can plant crops and build/upgrade towers. 
    - During the nighttime, the enemies come along a set path. Your defenses must hold in order to protect your crops and the farmhouse where your family is sleeping (you can lose your planters but each enemy that gets to the farmhouse removes one of your lives) 
- At least one map 
    - The path the enemies take is static according to pre-made maps.  
    - There will be fixed locations where you can place towers.  
- A variety of towers
    - hitscan attacks
    - Medieval fantasy themes such as archer tower, cannon, wizard tower, etc. 
    - Each tower will have different damage capabilities and costs, requiring strategic thinking on the part of the player as to how best allocate their resources. 
    - The player will have the ability to place towers in different locations and of different types. 
- Farming mechanic 
    - There are set planters and you decide what crop goes in them  
    - crops give money after $$x$$ cycles 
    - Longer = more money but more of an investment 
    - Plants automatically replant themselves, the player just decides what goes in them
- Enemies
    - Enemies follow a pre-set pathway through the map clearly marked as a road for the player
    - A set of enemy types that vary in health, speed, etc.
      - slow but tanky
      - fast but weak


**Nice to have goals:** 
- Environment based effects on the towers, meaning placement matters.
    - Ex. Higher = more range 
    - The enemies target the planters 
    - Not buying enough defense directly impacts your eco. 
- Variety in maps 
    - Either you get a random map or the user selects one.  
- The player has an attack 
    - Allows for strategies where you invest more in eco at the beginning and use your skill attacking the enemies to avoid buying towers until necessary 

## Development Plan 

### Project Checkpoint 1-2: Basic Mechanics and Scripting (Ch 5-9) 

We first intend to implement the tower defense aspect of our game, layering on the farming components as time progresses. Before we have implemented the farming simulation aspect of the game (which is the mechanism that will enable players to get more and improved resources), we will include automatically allocated resources. This will allow for us to develop our game as though it were a regular tower defense, and later layer in the simulation aspect. 

Our goals for the first checkpoint will include: 

Implement basic map functionality such as 
- A pre-generated map that will serve as the basis for the physical structure of gameplay using primitives 
    - Get an “enemy” to follow a preset path 
- A player that can move around the world 
- The ability to place primitive towers (not necessarily functional). Ex able to place a cube in the world.
