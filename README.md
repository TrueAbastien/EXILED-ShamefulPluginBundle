# Shaky Growth - EXILED Plugin for SCP: Secret Laboratory

Every time a player from a specified **in-game role list** receive damage from a specified **damage type list**, the damaged player will have each of his **dimensional scale proprety randomly changed** to a value clamped to two specified values.

## Settings
Name | Type | Default | Constraints | Description
:---: | :---: | :---: | :---: | :------
sg_enabled | bool | true | None | Disable the current plugin if set to **false**.
sg_targetWL | StringList | Empty | Must be composed of RoleType name(s) | Whitelist of every possible target in-game role for an affected player.
sg_targetBL | StringList | Empty | Must be composed of RoleType name(s) | Blacklist of every possible target in-game role for an affected player.
sg_originWL | StringList | Empty | Must be composed of DamageType name(s) | Whitelist of every possible damage type affecting a targetted player.
sg_originBL | StringList | Empty | Must be composed of DamageType name(s) | Blacklist of every possible damage type affecting a targetted player.
sg_minScale | float | 0.25f | Must be lesser than *sg_maxScale* | Minimum scale a scale dimension can take as value.
sg_minScale | float | 2f | Must be greather than *sg_maxScale* | Maximum scale a scale dimension can take as value.

Whenever a whitelist is defined, it overrules the existance of a similar blacklist. However, if none are defined, all elements are considered whitelisted.

Here is a quick config to copy-paste.

``` yml
# Shaky Growth active state
sg_enabled: true
# Targetted RoleType list(s)
#sg_targetWL: ['ClassD', 'Scientist', 'Scp0492']
#sg_targetBL: ['ClassD', 'Scientist', 'Scp0492']
# Original DamageType list(s)
#sg_originWL: ['Usp', 'MicroHid', 'Logicer']
#sg_originBL: ['Usp', 'MicroHid', 'Logicer']
# Dimensional scale value limits
sg_minScale: 0.25
sg_maxScale: 2.0
```

---

I made this, but feel free to take any part of my code as your own.
