using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using UnityEngine;

namespace Horcrux
{
    public class Plugin : EXILED.Plugin
    {
        private EventHandlers eventHandlers;
        private Commands commands;
        public Methods methods { get; private set; }

        public List<Powers.Power> currentPowers = new List<Powers.Power>();
        public List<Powers.Power> powerPool = new List<Powers.Power>();
        public int horcruxOverload;

        public bool enabled { get; private set; }
        public string[] pluginNameTypo { get; private set; }
        public RoleType masterRole { get; private set; }
        public RoleType slaveRole { get; private set; }

        public float heredity_HealPercentage { get; private set; }
        public float resistance_DmgPercentage { get; private set; }
        public float shrink_GlobalScale { get; private set; }

        public override void OnEnable()
        {
            ReloadConfig();
            if (!enabled) return;

            eventHandlers = new EventHandlers(this);
            commands = new Commands(this);
            methods = new Methods(this);

            commands.AddCommands("horcrux.get", new Dictionary<string, Commands.CommandMethod>
            {
                { "info", methods.HorcruxInformations },
                { "infos", methods.HorcruxInformations },
                { "info all", methods.HorcruxInformations },
                { "infos all", methods.HorcruxInformations }
            });

            Events.RoundStartEvent += eventHandlers.OnRoundStart;
            Events.RoundEndEvent += eventHandlers.OnRoundEnd;

            Events.PlayerSpawnEvent += eventHandlers.OnPlayerSpawn;
            Events.PlayerDeathEvent += eventHandlers.OnPlayerDeath;
        }

        public override void OnDisable()
        {
            Events.RoundStartEvent -= eventHandlers.OnRoundStart;
            Events.RoundEndEvent -= eventHandlers.OnRoundEnd;

            Events.PlayerSpawnEvent -= eventHandlers.OnPlayerSpawn;
            Events.PlayerDeathEvent -= eventHandlers.OnPlayerDeath;

            eventHandlers = null;
            commands = null;
            methods = null;
        }

        public override void OnReload() { }

        private void ReloadConfig()
        {
            enabled = Config.GetBool("horcrux_enabled", true);
            pluginNameTypo = Config.GetStringList("horcrux_pluginNames")?
                .Select(x => x.ToLower()).ToArray() ?? new string[] { "horcrux" };

            try { masterRole = (RoleType)Config.GetInt("horcrux_masterRole", 5); }
            catch(Exception) { masterRole = RoleType.Scp049; }

            try { slaveRole = (RoleType)Config.GetInt("horcrux_slaveRole", 10); }
            catch (Exception) { masterRole = RoleType.Scp0492; }

            heredity_HealPercentage = Mathf.Clamp(Config.GetFloat("horcrux_heredity_healPercentage", .1f), 1e-2f, 1);
            resistance_DmgPercentage = Mathf.Clamp(Config.GetFloat("horcrux_resistance_dmgPercentage", .5f), 1e-2f, 1);
            shrink_GlobalScale = Mathf.Clamp(Config.GetFloat("horcrux_shrink_globalScale", .5f), 5e-2f, 1);
        }

        public override string getName { get; } = "Horcrux";
    }
}
