using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace Horcrux
{
    public class EventHandlers
    {
        private Plugin plugin;
        public EventHandlers(Plugin _plugin) => plugin = _plugin;

        internal void OnPlayerSpawn(PlayerSpawnEvent ev)
        {
            if (ev.Role == plugin.slaveRole)
            {
                plugin.methods.pickPower();
            }

            else if (ev.Role == plugin.masterRole)
            {
                foreach (Powers.Power power in plugin.currentPowers)
                    power.give(ev.Player);
            }
        }

        internal void OnPlayerDeath(ref PlayerDeathEvent ev)
        {
            if (ev.Player.GetRole() == plugin.slaveRole)
            {
                plugin.methods.releasePower();
            }

            else if (ev.Player.GetRole() == plugin.masterRole)
            {
                foreach (Powers.Power power in plugin.currentPowers)
                    power.take(ev.Player);
            }
        }

        internal void OnRoundStart()
        {
            plugin.powerPool.AddRange(new Powers.Power[]
            {
                new Powers.SpeedPower(plugin),
                new Powers.StrengthPower(plugin),
                new Powers.ResistancePower(plugin),
                new Powers.ThoughnessPower(plugin),
                new Powers.ElasticityPower(plugin),
                new Powers.HeredityPower(plugin),
                new Powers.ShrinkPower(plugin)
            });

            plugin.powerPool.ShuffleList();
            plugin.horcruxOverload = -plugin.powerPool.Count;
        }

        internal void OnRoundEnd()
        {
            plugin.currentPowers.Clear();
            plugin.powerPool.Clear();
        }
    }
}
