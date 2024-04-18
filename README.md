<b>INTRODUCTION</b>

This project is designed to show my train of thought and some basic understanding of Unity.
I believe that there is usually more than one right approach to any task, so I wanted to
demonstrate my prototyping capabilities and performant but readable code, rather than make
the most efficient and architecturally clean systems. Some things could definitely be done
better, but that would require spending more time on things that don't really need optimization
at the moment. Hope this alligns with what you are looking for.

-------------------------

<b>INPUT INSTRUCTIONS</b>

The game has 3 control elements:

Left Stick - moves the character
Right Stick - used for aiming AND shooting if dragged all the way out
Slide button - touching the screen anywhere besides the sticks results in sliding (dodging)

-------------------------

<b>ARCHITECTURAL REASONING</b>

I chose the approach with systems made as Scriptable Objects, although not all the functional
code resides in such systems (due to prototyping compromises). Combined with Dependency Injection
it gives me a very versitile framework, where I can switch parts of the game on the run in
the inspector, but also don't always have to think about manually dragging the references.
Such approach is performant and agile, and would come in handy with more features.

I usually also use UniRx, but in this case I wouldn't really benefit from it, and since it is
not exactly good for performance I thought adding it would only show poor taste.

-------------------------

<b>TIME REPORT</b>

Monday 15.04 (~4h total) - Found assets for a character, level and enemies, set up the project, created the level,
created the Animator Controller for the player.

Tuesday 16.04 (~3h total) - Added a virtual camera with following logic, created a script that handled both
movement and shooting, tweaked the settings to make it feel close to the source material.

Wednesday 17.04 (~6h total) - Refactored the project, split logic into multiple systems and controllers,
added camera logic for aiming, added touch controls and changed overall input logic, fixed some bugs.

TOTAL: ~13h

CONCLUSION: I had a couple of weird issues that required either restarting Unity, or moving logic to
MonoBehaviours to solve, but otherwise I think I was quite efficient. Without refactoring it could have
taken almost half as much time.

-------------------------

<b>AREAS FOR IMPROVEMENT</b>

The visual aspect could benefit greatly from more in-depth work. Gun visualization, muzzle flash, bullets
hitting the walls - all those things could be improved.
I didn't have time to do enemies, which is unfortunate. I would probably need another 2-4 hours to do
simple AI with proper animation and hit effect.
Another thing that I wanted to do is pulling the camera follow target towards the room centre, because
I feel like it sometimes happens in Enter the Gungeon during the fight.
And I guess I would improve current architecture a little, I would really want to move all the logic to
Scriptable Objects, leaving only basic functions to MonoBehaviours.

-------------------------

Used third-party packages:

MINIFANTASY - Dungeon (https://assetstore.unity.com/packages/2d/environments/minifantasy-dungeon-206693)
2D Character - Astronaut (https://assetstore.unity.com/packages/2d/characters/2d-character-astronaut-182650)
2D Skeleton - Isometric PixelArt Character (https://assetstore.unity.com/packages/2d/characters/2d-skeleton-isometric-pixelart-character-254596)
Zenject (https://assetstore.unity.com/packages/tools/utilities/extenject-dependency-injection-ioc-157735)

Used Unity packages:

URP
Cinemachine
Input System
Other default packages