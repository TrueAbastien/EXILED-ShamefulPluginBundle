using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace ShakyGrowth
{
    public class EventHandlers
    {
        private Plugin plugin;
        public EventHandlers(Plugin _plugin) => plugin = _plugin;

        internal void OnPlayerHurt(ref PlayerHurtEvent ev)
        {
            if (plugin.targetWhiteList != plugin.targetList.Contains(ev.Player.GetRole()))
            {
                return;
            }

            if (plugin.originWhiteList != plugin.originList.Contains(ev.DamageType.name))
            {
                return;
            }

            ev.Player.SetScale(
                NextFloatBetween(plugin.minScale, plugin.maxScale),
                NextFloatBetween(plugin.minScale, plugin.maxScale),
                NextFloatBetween(plugin.minScale, plugin.maxScale));
        }

        private float NextFloatBetween(float min, float max)
        {
            return (float)plugin.gen.NextDouble() * (max - min) + min;
        }
    }
}
