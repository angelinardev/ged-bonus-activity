# ged bonus activity
 Angelina Ratchkov
Student number: 100740576

EVEN TASKS: Ice Climber
video: https://www.youtube.com/watch?v=awYlHBXHits

Controls:
WASD to move forward, back, left and right
Space to jump (can be spammed)
Z to break a block (might need to hold the key, and only some blocks can be removed, mainly the top
upper layer)
X to undo it

The player movement code comes from my GDW game, which I implemented.

Singleton for health manager:
There is a single instance of a health manager attached to an empty Game Manager object.
In the script, I make sure there is an instance with:

if (!instance)
        {
            instance = this;
        }


Since this script is only attached to one object, there should be only one instance.
I call this instance in the PlayerController when a player collides with an enemy to remove health:

if (other.collider.tag == "Enemy") //player hits the enemy
        {
            HealthManager.instance.ChangeHealth(-1);
            //push player back
            // Calculate Angle Between the collision point and the player
            Vector3 dir = other.contacts[0].point - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            other.gameObject.GetComponent<Rigidbody>().AddForce(dir *force);
	}

There is a single enemy at the top, as a cube (doesn't move or do anything, but will damage the player if
the player touches it. The player is also slightly pushed upwards).

It was implemented in this way because it makes it very easy to add or remove health to the player,
since there is only one instance/class that needs to be worried about. It should also allow
the game to run faster when there is only one instance.

Undo destroying a block command pattern:
I used the command pattern code that was given during the labs (either lab session 2 or 3) and modified
it to fit this purpose.
When the player destroys a block, a new command is created and added to a command invoker instance.
I save the position and transform data of the block that was destroyed, and add it to a list that
will store all the data. At the start of the game, I make sure to save a list of all already
created blocks, so that I can access that list later to remove them.

Then, I check for the undo button (which is X). If it is pressed, then I instantiate
a new platform gameobject based on the data stored in the command instance,
and then run the command instance undo command.

I created a class, BlockPlacer, that has custom code that allows me to do what I want specifically for
this task. In this case, Execute will remove an item from the list and destroy the gameobject,
and Undo will add an item to the list and instantiate it. This is also done through a helper
class, ItemPlacer.

Doing it this way allows me to create various command patterns if I want, with different
Undo and Execute methods. This gives the code flexibility. So I could reuse a lot of this code
for different purposes. All that is needed is to create a command object wherever we want the command to
done (so in this case, when the user destroys a block so that we can add it to the command list),
and then to call the Undo command when the undo key is pressed.
There is also only one instance of the CommandInvoker class needed, which saves on memory too.

So, when breaking a block:

//create new command, using data from collided object
 command = new BlockPlacer(other.transform.position, other.transform);
 //adds to list
 commandinstance.AddCommand(command);

When doing undo:

commandinstance.UndoCommand(lastRemoved);


And then the commandinstance will run the Execute/Undo commands based on a commandHistory list.

I had to reverse the code that was given during the lab (so in this case, executing
the command removes the item from the list, and undoing adds the item to the list).

Observer pattern was not implemented and there is no flowchart for it.