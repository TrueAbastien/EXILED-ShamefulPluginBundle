using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace Horcrux
{
    public class Methods
    {
        private Plugin plugin;
        public Methods(Plugin _plugin) => plugin = _plugin;

        internal void pickPower()
        {
            if (plugin.horcruxOverload++ >= 0)
                return;

            Powers.Power power = plugin.powerPool[0];
            plugin.powerPool.RemoveAt(0);
            plugin.powerPool.ShuffleList();

            power.subcribe();
            plugin.currentPowers.Add(power);

            ReferenceHub[] players = Player.GetHubs().Where(x => x.GetRole() == plugin.masterRole).ToArray();
            foreach (var player in players)
                power.give(player);
        }

        internal void releasePower()
        {
            if (plugin.horcruxOverload-- >= 0)
                return;

            Powers.Power power = plugin.currentPowers[0];
            plugin.currentPowers.RemoveAt(0);

            power.unsubcribe();
            plugin.powerPool.Add(power);
            plugin.powerPool.ShuffleList();

            ReferenceHub[] players = Player.GetHubs().Where(x => x.GetRole() == plugin.masterRole).ToArray();
            foreach (var player in players)
                power.take(player);
        }

        internal void HorcruxInformations(ReferenceHub sender, Dictionary<string, string> args)
        {
            Log.Info(
                $" The { Player.GetHubs().Where(x => x.GetRole() == plugin.masterRole).ToList().Count } " +
                $"{ Enum.GetName(typeof(RoleType), plugin.masterRole) } have " +
                $"the { Player.GetHubs().Where(x => x.GetRole() == plugin.slaveRole).ToList().Count } " +
                $"{ Enum.GetName(typeof(RoleType), plugin.slaveRole) } as Horcruxes.");

            if (args["all"]?.ToLower().First() == 'a')
            {
                Log.Info(
                    $"   Current powers: { string.Join(", ", plugin.currentPowers.Select(x => x.data.name)) }.\n" +
                    $"   Master Names: { string.Join(", ", Player.GetHubs().Where(x => x.GetRole() == plugin.masterRole).Select(x => x.GetNickname())) }.\n" +
                    $"   Slave Names: { string.Join(", ", Player.GetHubs().Where(x => x.GetRole() == plugin.slaveRole).Select(x => x.GetNickname())) }.");
            }
        }
    }
}
