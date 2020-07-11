# Tournament - EXILED Plugin for SCP: Secret Laboratory

This plugin will allow any admin user to start a **Tournament** event where players will be teleported one after the other in duels. Once one is dead, the loser will exit the tournament pool while the winner will be placed on top.

The event ends whenever only one player remains in the player pool.

## Settings
Name | Type | Default | Constraints | Description
:---: | :---: | :---: | :---: | :------
tournament_excludeSender | bool | true | None | Automatically remove the Command Sender from the Player Pool.
tournament_pluginNames | StringList | tournament | Must not be empty | Names the plugin can take in Remote Admin command.
tournament_fightRoom | string | HCZ_106 | Must be a valid room name | Name of the room where the tournament will take place.
tournament_fightDelay | float | 10f | Must be above or equal to 0 | Seconds spent between the teleportation of new players and the time they will be given guns.
tournament_gunItemID | int | 30 (GunUSP) | Must be a valid weapon ID (if not, will go back to USP) | Gun given to both player to fight each other.

## Commands

Any command will have to be invoked with a valid name defined in the **Plugin Names list (tournament_pluginNames)** followed by the command name and its arguments.
```
tournament <command> <args>...
```

Name | Arguments | Description
:---: | :---: | :------
start | - | Will start the tournament if no other tournament is currenlty running.
info(s) | - | Will print out informations regarding the current tournament in Remote Admin console.
stop | - | Will stops the tournament if a tournament is currently running.
arm | weaponID | Will change the weapon for the rest of the tournament (reset on reload).
localize | roomName | Will change the duels will take place (reset on reload).

---

I made this, but feel free to take any part of my code as your own.
