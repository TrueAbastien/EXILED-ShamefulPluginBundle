using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace Tournament
{
    /// <summary>
    /// Main plugin for Tournament.
    /// </summary>
    public class Plugin : EXILED.Plugin
    {
        /// <summary>
        /// EventHandlers main instance.
        /// </summary>
        private EventHandlers eventHandlers;
        /// <summary>
        /// Commands main instance.
        /// </summary>
        private Commands commands;
        /// <summary>
        /// Methods main instance.
        /// </summary>
        public Methods methods { get; private set; }

        /// <summary>
        /// Settings: remove the Command Sender from the tournament.
        /// </summary>
        public bool excludeSender { get; private set; }
        /// <summary>
        /// Settings: list of all allowed plugin names.
        /// </summary>
        public string[] pluginNameTypo { get; private set; }
        /// <summary>
        /// Settings: name of the room in which to fight.
        /// </summary>
        public string fightRoom;
        /// <summary>
        /// Settings: seconds between teleportation and beginning of the hostilities.
        /// </summary>
        public float fightDelay { get; private set; }
        /// <summary>
        /// Settings: gun given to two contestant in the tournament.
        /// </summary>
        public ItemType tournamentGun;

        /// <summary>
        /// Every player participating in tournament.
        /// </summary>
        public List<ReferenceHub> playerPool = new List<ReferenceHub>();
        /// <summary>
        /// Player in the current fight pool.
        /// </summary>
        public List<ReferenceHub> currentPool = new List<ReferenceHub>();
        /// <summary>
        /// Define if the tournament is running or not.
        /// </summary>
        public bool isRunning = false;

        /// <summary>
        /// Called on Plugin dynamic start.
        /// </summary>
        public override void OnEnable()
        {
            // Reload .yml config
            ReloadConfig();

            // Instantiate main object instances
            eventHandlers = new EventHandlers(this);
            commands = new Commands(this);
            methods = new Methods(this);

            // Map every command to string
            commands.AddCommands("tournament.set", new Dictionary<string, Commands.CommandMethod>{
                { "start", methods.TournamentStart },
                { "stop", methods.TournamentStop },
                { "arm weapon", methods.TournamentArm },
                { "localize roomName", methods.TournamentLocalize }
            });
            commands.AddCommands("tournament.get", new Dictionary<string, Commands.CommandMethod>{
                { "info", methods.TournamentInfos },
                { "infos", methods.TournamentInfos }
            });

            // Subscribe to requiered events
            Events.RemoteAdminCommandEvent += commands.OnRACommand;
            Events.PlayerLeaveEvent += eventHandlers.OnPlayerLeave;
            Events.PlayerDeathEvent += eventHandlers.OnPlayerDeath;
        }

        /// <summary>
        /// Called on Plugin dynamic end.
        /// </summary>
        public override void OnDisable()
        {
            // Unsubscribe to requiered events
            Events.RemoteAdminCommandEvent -= commands.OnRACommand;
            Events.PlayerLeaveEvent -= eventHandlers.OnPlayerLeave;
            Events.PlayerDeathEvent -= eventHandlers.OnPlayerDeath;

            // Destroy main object instances
            eventHandlers = null;
            commands = null;
            methods = null;
        }

        /// <summary>
        /// Called on Plugin reload.
        /// </summary>
        public override void OnReload()
        {
            //
        }

        /// <summary>
        /// Called first to load the .yml settings data requiered in this Plugin.
        /// </summary>
        public void ReloadConfig()
        {
            // Load basic Settings
            excludeSender = Config.GetBool("tournament_excludeSender", true);
            pluginNameTypo = Config.GetStringList("tournament_pluginNames")?
                .Select(x => x.ToLower()).ToArray() ?? new string[] { "tournament" };
            fightRoom = Config.GetString("tournament_fightRoom", "HCZ_106");
            fightDelay = Config.GetFloat("tournament_fightDelay", 10f);

            // Load Tournament GunType
            try
            {
                tournamentGun = (ItemType)Config.GetInt("tournament_gunItemID", 30);
            }
            catch (Exception e)
            {
                Log.Error($"Failed to get ItemType: {e.Message}");
            }
            if (tournamentGun == 0)
            {
                tournamentGun = ItemType.GunUSP;
            }
            else
            {
                if (!tournamentGun.IsWeapon(true))
                    tournamentGun = ItemType.GunUSP;
            }
        }

        /// <summary>
        /// String access to Plugin Name.
        /// </summary>
        public override string getName { get; } = "Tournament";
    }
}
