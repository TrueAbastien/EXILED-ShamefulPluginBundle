# Horcrux - EXILED Plugin for SCP: Secret Laboratory

This plugin will grant each player of a specific in-game role **super powers**, ranging from *speed (the ability to move faster)* to *strength (the ability to open any door)*, whenever another player, of a specific role, spawns. The death of a **giving (slave)** player will induce the removal of the oldest super power for every **receiving (master)** players.

## Powers

Here is a quick list of every existing *super power* which can be applied to master role players.

ID | Name | Effect
:---: | :---: | :------
1 | Speed | Boost in movement speed
2 | Strength | Can open any door/gate (if unlocked)
3 | Resistance | Reduce damage taken
4 | Shrink | Reduce global player size
5 | Elasticity | Grant invulnerability to Micro HID and Tesla gates
6 | Thoughness | Grant invulnerability to any explosion
7 | Heredity | Heal based on slave role damage dealt

## Settings
Name | Type | Default | Constraints | Description
:---: | :---: | :---: | :---: | :------
horcrux_enabled | bool | true | None | Disable the plugin on dynamic start if set to **false**.
horcrux_pluginNames | StringList | horcrux | Must not be empty | Names the plugin can take in Remote Admin command.
horcrux_masterRole | int | 5 (Scp049) | Must be a RoleType ID (if not, will go back to default) | Master role ID, receiving any super power.
horcrux_slaveRole | int | 10 (Scp0492) | Must be a RoleType ID (if not, will go back to default) | Slave role ID, giving a super power to master role by staying alive.
horcrux_heredity_healPercentage | float | 0.1f | Must be between **0.01f** and **1f** | Heal percentage, based on damage dealt, given on a slave role player damage for Heredity power.
horcrux_resistance_dmgPercentage | float | 0.5f | Must be between **0.01f** and **1f** | Damage percentage, reducing a master role player damage taken, for Resistance power.
horcrux_shrink_globalScale | float | 0.5f | Must be between **0.05f** and **1f** | Global scale, resizing 3D size vector of a master role player, for Shrink power.

## Commands

Any command will have to be invoked with a valid name defined in the **Plugin Names list (horcrux_pluginNames)** followed by the command name and its arguments.
```
horcrux <command> <args>...
```

Name | Arguments | Permission | Description
:---: | :---: | :---: | :------
info(s) | - | horcrux.get | Will print out **basic** information regarding the current state induced by the plugin activation.
info(s) | a(ll) | horcrux.get | Will print out **advanced** information regarding the current state induced by the plugin activation.

---

I made this, but feel free to take any part of my code as your own.
