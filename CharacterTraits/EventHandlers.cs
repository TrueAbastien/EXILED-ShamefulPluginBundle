using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;
using Trait = CharacterTraits.Traits.Trait;

namespace CharacterTraits
{
    public class EventHandlers
    {
        private List<int> traitIndexes = new List<int>();

        private Plugin plugin;
        public EventHandlers(Plugin _plugin)
        {
            plugin = _plugin;
            for (int ii = 0; ii < Trait.count; ++ii)
                traitIndexes.Add(ii * 2);
        }

        internal void OnPlayerSpawn(PlayerSpawnEvent ev)
        {
            if (plugin.roleTypes.Contains(ev.Player.GetRole()) != plugin.roleTypeWhitelist)
            {
                return;
            }
            if (plugin.positiveTraitAmount + plugin.negativeTraitAmount > Trait.count)
            {
                return;
            }
            if (ev.Player.GetRole().IsAnyScp())
            {
                return;
            }

            traitIndexes.ShuffleList();
            int index = 0;
            for (; index < plugin.positiveTraitAmount; ++index)
            {
                Trait.all[traitIndexes[index]].Add(ev.Player);
            }
            for (; index < plugin.negativeTraitAmount; ++index)
            {
                Trait.all[traitIndexes[index] + 1].Add(ev.Player);
            }

            ev.Player.Broadcast(5, "You are " + string.Join(", ", plugin.methods.GetTraitFrom(ev.Player)) + ".", false);
        }

        internal void OnPlayerDeath(ref PlayerDeathEvent ev)
        {
            foreach (var trait in Trait.all)
            {
                trait.Remove(ev.Player);
            }
        }
    }
}
