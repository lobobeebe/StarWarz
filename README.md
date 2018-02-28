# StarWarz
School project.

## Completed
Player Body
* Shall be an Invisible game object representing the body
* Shall act as the hit box for the player's torso
* Shall maintain the same X,Z position and Y rotation as SteamVR headset
* Shall hold a Lightsaber "Holster" or clip on front of object

Player Hands
* Shall provide an anchor transform for a held Lightsaber Hilt
* Shall contain a Force Gesture Recognition Module. (See below)
* Shall maintain a Lightsaber Held state: Holding or Not Holding.
* Shall maintain a Force Selection state: None, Push, Grab, Lightning, Heal, or Future.
* When in the Not Holding state, the "Grip" button is pressed, and the hand is colliding with a Lightsaber Hilt, the Hand shall become the Lightsaber Hilt's anchor point.
* When in the Holding state, the "Grip" button is pressed, and the hand is colliding with a Holster, the Holster shall become the Lightsaber Hilt's anchor point.
* When in the Not Holding state, the "Grip" button is pressed, and the hand is not colliding with a Lightsaber Hilt, the Hand shall begin forwarding it's position to the Force Gesture Recognition Module every nth FixedUpdate, where n is a configurable value.
* The Hand shall cease forwarding it's position to the Force Gesture Recognition Module when the "Grip" button is released. The Force Selection state shall be updated to the Force Gesture Recognition Module's selection.

Force Gesture Recognition Module:
* The Force Gesture Recognition module will use a Neural Network trained using Gradient Descent.
* A Recurrent Neural Network was not used for this assignment, so a start and end point is required.
* The Force Gesture Recognition Module shall use the down-press of the "Grip" button to activate Gesture Recognition.
* The Force Gesture Recognition Module shall use the up-press of the "Grip" button to end Gesture Recognition and determine gesture.
* Details of the Force Gesture Recognition will not be discussed here. For more information, see Cody Beebe.

Holster
* Shall provide an anchor transform for an unheld Lightsaber Hilt

Lightsaber
* Press the menu on the right hand controller to activate.
* Can be used to deflect lasers
* Can strike enemies to make them explode
* Plays appropriate sound and gives off a glow

Training Droid
* Passive audio "buzz" emit from Droid
* Moves to a new random location every 4 +/- 2 seconds
* Fires a laser bolt every 4 +/- 2 seconds
** Laser bolt shall be directed toward player's chest with a rotational offset of +/- 10 degrees in all axes

Laser Bolt
* Moves in its transform's forward direction
* Creates an audio event at the location of the laser creation to indicate the laser was fired.
* After spawning, a 5 second timer is started. If the timer completes, the game object is destroyed.
* If the Laser Bolt touches the Lightsaber, the Laser Bolt's forward vector is set to the normalized vector difference between its location and the location of the droid that fired it.

## Future Work
Holster
* Holster shall be invisible until a hand is making contact with it - at which point it will be shown as an outline of a Lightsaber hilt.

## Contributor
### Cody Beebe
* Scripting of:
** Droids
** Lightsaber
** Force Gesture Recognition Module
** Hands
** Body
** Scenes 
### Kevin Kuretski
* Models
** Environment
** Lightsaber
** Droids
* Audio
** Droids
** Force abilities
** Lasers
** Lightsaber
* Overall direction of project.
