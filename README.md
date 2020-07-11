# Character Traits - EXILED Plugin for SCP: Secret Laboratory

Any player will be given random traits on spawn granting buff, debuff or power during his playtime. Traits are either **Negative** or **Positive**, one kind granting debuff while the others grant buff.

## Traits

Here is a list of all possible traits.

### Positive Traits
Name | Effect
:---: | :-----
Small | Reduce global player scale by 20%.
SharpShooter | Increase weapon damage by 25%.
Tough | Reduce any damage taken by 15%.
Caring | Any healing item heals 25HP bonus.
Lucky | Coin, Logicer & ZM Keycard upgrade faster.
Dexterous | Fall damage is divided by 4.
Wary | First hit taken deals no damage & tesla gates only damage you for 25HP.
Healthy | Increase max health by 25%.
Stony | Reduce by half pocket dimension damage and remove Scp207 damage.
Crafty | Grenade deals twice the amount of damage.

### Negative Traits
Name | Effect
:---: | :-----
Tall | Increase global player scale by 25%.
Imprecise | Reduce weapon damage by 20%.
Soft | Increase any damage taken by 10%.
Rough | Any healing item damages the player for 10HP.
Unlucky | Logicer & FM Keycard never upgrade.
Numb | Fall damage is doubled.
Careless | SCPs hit you for 25% more damages.
Feeble | Reduce max health by 15%.
Unhinged | Multiply pocket dimension damage by 3.
Explosive | Grenades will explode on throw.

## Settings
Name | Type | Default | Constraints | Description
:---: | :---: | :---: | :---: | :------
ct_enabled | bool | true | None | Disable the current plugin if set to **false**.
ct_roleTypeWL | StringList | Empty | Must be composed of RoleType name(s) | Whitelist of every possible in-game role to receive traits.
ct_roleTypeBL | StringList | Empty | Must be composed of RoleType name(s) | Blacklist of every possible in-game role to receive traits.
ct_pTraitAmount | int | 2 | Once added to *ct_nTraitAmount*, must be lower than 10 | Amount of Positive traits given on spawn.
ct_nTraitAmount | int | 1 | Once added to *ct_pTraitAmount*, must be lower than 10 | Amount of Negative traits given on spawn.

Whenever the whitelist is defined, it overrules the existance of the blacklist. However, if none are defined, all elements are considered whitelisted. SCP roles cannot be added to any RoleType list.

Here is a quick config to copy-paste.

``` yml
# Character Traits active state
ct_enabled: true
# Targetted RoleType list(s)
#ct_roleTypeWL: ['ClassD', 'Scientist', 'NtfCadet']
#ct_roleTypeBL: ['ClassD', 'Scientist', 'NtfCadet']
# Amount of any traits
ct_pTraitAmount: 2
ct_nTraitAmount: 1
```

---

I made this, but feel free to take any part of my code as your own.
