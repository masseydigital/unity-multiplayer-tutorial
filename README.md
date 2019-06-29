# unity-multiplayer-tutorial
Learning multiplayer with Unity. The first part of this repo is going through _quill18creates_ youtube tutorial series for creating multiplayer games using Unity's Multiplayer HLAPI. *The Unity Multiplayer HLAPI is deprecated and will be replaced soon with their new networking system*. The second part of this repo is going to be a short demo of a test game I built with the HLAPI. 

## Quick Start

1) Create a Unity 2019.1.

2) Install the Multiplayer HLAPI package from the package manager.

3) Create an empty gameobject and name it "Network Manager"

4) Add a Network Manager Component onto the Network Manager gameobject.

5) Add a Net Manager HUD component to the Network Manager gameobject.  This is going to add default UI to your scene when the scene starts.

6) Create a Player prefab with a Network Identity Component.

7) Assign your player prefab to the _Spawn Info_ variable.

8) Hit play and observe

## Concepts
A Unity application by default can act as both a server and a client.  

Each player in your networked game should have it's own _Player_ object.  It is recommended that the Player object is not a physical object in the game - this is due to how Unity interacts with objects in the scene.  Player objects must contain a Network Identity.

Everything that is going to be networked needs to have a network id.

To network objects together, you derive from NetworkBehaviour instead of Monobehaviour.

There are several different checks to network object control to include _isPlayer_, _hasAuthority_, _isLocalPlayer_, etc.

NetworkServer.Spawn() is the only way to make a network object appear on all of the clients views.  Only the *Server* can run this method.

_Update_ runs on everyones computer whether or not they own the particular object.

_Commands_ are special functions that *only* get executed on the server.  Command methods in Unity must have the Command attribute, and the method must be prefixed with *Cmd.  The code is called by the client, but then is executed on the server.

Any networked object that is going to be spawned in your game must be registerd in the _Registered Spawnable Prefabs_ list on the Network Manager.

When actions happen on behalf of the server, they must be propagated back to all the other clients.

The _Network Transform_ component comes with the Networking Package and communicates transform changes to the client.

_Remote Procedure Calls (RPCS)_ are similar to commands, but they only get ran by clients.  All remote procedure calls must begin with te _RPC_ attribute.

_Rubberbanding_ occurs when the client player makes an illegal action that is rejected by the server causing the client to _rubberband_ back to their previous location.

_SyncVars_ are variables where if their value changes on the *_server_*, then all clients are automatically informed of the new value.  They help alleviate the need for explicitly calling RPC's when making networked variables.  To define a _SyncVar_ variable, you can add the *[SyncVar]* attribute to the variable that you would like to sync.  You can also add a hook to your attribute, i.e. [SyncVar("MethodToRun")] to call specific functions when a SyncVar is updated.  If you use a hook on a SyncVar, your local value does *_not_* get automatically updated.

Mantra suggested by Quill: 
1) Don't update what hasn't changed
2) Use prediction when possible
3) Figure out what is deterministic
4) Minimize network updates

Generally, we do not want to directly update transform.position through the network because the players will teleport/blink to the location as the udpates come in. Instead, we want to predict where the player is supposed to be, and then update on the client side to get them to that location.