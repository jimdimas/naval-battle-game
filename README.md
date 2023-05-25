# naval-battle-game

This is a simple Naval Battle game i coded using C#/Windows Forms.It focuses more on the functionality and the behavior of the game rather than the graphics and apppearance.

The custom_components folder contains the BattlefieldPoint component , which extends the default Button class of Windows Forms.The BattlefieldPoint component represents a single point in the battlefield , having
different functionality if it's occupied by a ship or not or if it's on our battlefield or on the enemy's one.The CustomLabel component is used for appearance purposes in the battlefield.

The classes folder contains all the custom C# objects that are needed for the game and are not extensions or default Windows Forms items/components such as Forms,Button etc. .
The Battlefield class represents the abstraction of a battlefield which contains a 2d array of BattlefieldPoint objects and the overall functionality of adding Warships in the Battlefield ,with respect to different limitations (ship size,occupy free positions etc.).
The Warship class holds a list of Battlefield points that represent the warship in the battlefield and contains the functionality of creating a Warship and removing points when a point of it is hit.

The forms folder contains all the needed forms , the StartForm which is a prompt when the game is started to either login or register,the Login/Register Forms , an intermediate form called UserForm which is used to 
see user statistics of game results and the GameForm which is the main form that supports the game.

The GameForm creates the 2 battlefields , sets a number of Warships in each (4 warships of sizes 2,3,4,5 on each) and keeps count of the player tries and the time.When the game ends (all of the user's or computer's ships are sunk) , the GameEnd method is called which is a method of the form.
