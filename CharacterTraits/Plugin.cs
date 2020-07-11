using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;

namespace CharacterTraits
{
    public class Plugin : EXILED.Plugin
    {
        private EventHandlers eventHandlers;
        public Methods methods { get; private set; }

        public bool enabled { get; private set; }

        public List<RoleType> roleTypes { get; private set; }
        public bool roleTypeWhitelist { get; private set; }

        public int positiveTraitAmount { get; private set; }
        public int negativeTraitAmount { get; private set; }


        public override void OnEnable()
        {
            ReloadConfig();
            if (!enabled) return;

            eventHandlers = new EventHandlers(this);
            methods = new Methods(this);

            Events.PlayerSpawnEvent += eventHandlers.OnPlayerSpawn;
            Events.PlayerDeathEvent += eventHandlers.OnPlayerDeath;
        }

        public override void OnDisable()
        {
            Events.PlayerSpawnEvent -= eventHandlers.OnPlayerSpawn;
            Events.PlayerDeathEvent -= eventHandlers.OnPlayerDeath;

            eventHandlers = null;
            methods = null;
        }

        public override void OnReload() { }
        public override string getName { get; } = "CharacterTraits";

        private void ReloadConfig()
        {
            enabled = Config.GetBool("ct_enabled", true);

            roleTypes = new List<RoleType>();
            RoleType output;
            List<string> roleTypeNames = Config.GetStringList("ct_roleTypeWL") ?? Config.GetStringList("ct_roleTypeBL") ?? new List<string>();
            foreach (string role in roleTypeNames)
                if (Enum.TryParse(role, out output))
                    roleTypes.Add(output);
            roleTypeWhitelist = Config.GetStringList("ct_roleTypeWL") != null;

            positiveTraitAmount = Config.GetInt("ct_pTraitAmount", 2);
            negativeTraitAmount = Config.GetInt("ct_nTraitAmount", 1);
        }
    }
}
