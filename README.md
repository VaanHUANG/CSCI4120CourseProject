# CSCI4120CourseProject

This project serves as the course final project of CSCI4120:Principles of Computer Game Software.

This project is low-poly FPS game featuring the intermasking reaction of three elements: water, fire and earth. In this game, 
on each level, slimes of different elements are generated on the map randomly by the GameManager, and then motivated by the AI
deisgned by one of my groupmate Barry Choi, to attack the player by melée crush. Players have a gun in hand (de facto the famous 
flare gun), and can switch between different elements to counter different slimes. If players, unfortunately, shoot a bullet of 
the same element as the hit slime, the slime will grow stronger; If the bullet element masks that of the slime, the slime will
shrink dramactically; If it's the other element, the slime will shrink a little bit. For more info on the project, please refer to the Phase1 Report and Final Report under the root directory.

My role in this project is mostly physics programmer (well at the beginning I collect all the assets for building a low-poly
world). I mostly deal with collision detection to realize the explosion effect, the shrink and grow of slimes and the shooting
action. Tags are used to distinguish different type of objects.
