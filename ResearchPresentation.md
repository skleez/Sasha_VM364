# Getting Started with Unet and Unity Developer Services

###In this tutorial I hope to get you started with a simple but working Unity multiplayer project using Unet.

First things first, lets make a new Unity project:

Next, add a 3D plane for a floor, and Save-As the scene twice. Once as "Main_Scene", and again as "Lobby_Scene".

Next, in your Lobby_Scene, create an empty gameobject, and add the NetworkManager Component, and the NetworkManagerHUD Component to it:

This "Network Manager" object will contain most of the important information for your multiplayer game.

Next, we will want to make a player prefab for our game. For this, I created a "Prefab" folder in my project, created a new prefab called "Player", and I dragged the "FPSController" prefab from the Unity Standard Assets onto it.
Once your prefab is set up, you should select it, and in the inspector, add the NetworkIdentity Component to it. Make sure to check the box that says "Local Player Authority" (This means that each player will only be able to control their player).

Next, open up your project's Build Settings, and add the "Lobby_Scene" as Scene 0, and the "Main_Scene" as Scene 1.

Once all of that is done, go back to the "Network Manager" object. Add the "Player" Prefab to the Player Prefab slot in the Network Manager Component, drag the "Lobby_Scene" to the slot for Offline Scene, and drag the "Main_Scene" to the slot for Online Scene. 
(Note: Only prefabs with the "Network Identity" component can added to the Player Prefab slot, and only scenes specified in your build settings can be added to the "Offline" and "Online" scene slots.)

Next, open up the Main_Scene, and create an empty gameobject. Add a "Network Start Position" component to the object, and place it where you would like the player to spawn. If you have multiple of these, Unity will. by default, choose one at random, and spawn the player there.

Thats all you need to do in the Unity scenes! Try exporting a build at this stage, open it up. If you click "LAN Host" it should start your "Main_Scene", and if simultaneously test your game in Unity and select "LAN Client", you should join the same game. 

However, if you try to start a game with "Enable Matchmaker", and click "Create Internet Match", you should get an error like this:

This is because your game is not connected to Unity's multiplayer service. It is not the most robust service, but starts completely free, and should allow you to create and test your smaller projects across computers pretty easily.

Start by opening a browser, go to https://developer.cloud.unity3d.com/landing/  and sign in with your Unity account.
It should take you to a page that looks like this one, and your next step is to click "Create New Project".
Name your project, and on the next screen, click "Enable Multiplayer". 
Input roughly how many players you expect to play your game at one time (You can change this later if you need to).
This will give your project a unique UPID, which Unity will use to connect your project to the Multiplayer Service.
Go back to the Unity Application, and search for "Services." You don't want the standard Mac services (located in every single application under the Apple menu), you want Unity's unique Services page, which should open up a new tab where your inspector window is.

Click on the new Services tab, sign in with your Unity credentials, and connect your Unity project with the project you made earlier on the Unity Developer site.

Thats all there is to it! Try exporting another build, and testing the "Create Internet Match" button. Your second player should be able to find the game using "Find Internet Match", from any other computer.

For more documentation check out Unity's Unet Manual and documentation here: http://docs.unity3d.com/Manual/UNetConcepts.html
When troubleshooting your own Unity Networking problems, make sure to try searching "Unet" instead of just "Unity Multiplayer," because it is relitively new, and programers are refering to it in multiple ways (Unet is the CORRECT name).
Make sure to be careful when reading about Unity's MasterServer (Discontinued after Unity 3.x, DON'T USE IT), as well as information on Untiy 4.x's Networking system (I don't know much about it, but it is not applicable in Unity 5).
