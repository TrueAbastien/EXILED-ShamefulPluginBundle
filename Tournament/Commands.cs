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
    /// Remote Admin Command System.
    /// </summary>
    public class Commands
    {
        /// <summary>
        /// Data for a specified command.
        /// </summary>
        [Serializable] public class CommandData
        {
            /// <summary>
            /// Command name.
            /// </summary>
            public string name { get; private set; }
            /// <summary>
            /// Method called as callback for the current command.
            /// </summary>
            public CommandMethod callback { get; private set; }
            /// <summary>
            /// List of every specified arguments.
            /// </summary>
            public List<string> arguments { get; private set; }
            /// <summary>
            /// Permission requiered to run the command.
            /// </summary>
            public string permission { get; private set; }

            /// <summary>
            /// Computed alias (for comparaison purpose).
            /// </summary>
            public string alias { get; private set; }

            /// <summary>
            /// Default constructor, pass every variable to retain as data.
            /// </summary>
            /// <param name="_name">First argument, function name</param>
            /// <param name="_callback">Method called on RA Command call</param>
            /// <param name="_args">Remains of the numerous arguments in RA Command</param>
            public CommandData(string _name, CommandMethod _callback, string _permission, IEnumerable<string> _args = null)
            {
                // Default Setter
                name = _name;
                callback = _callback;

                // Basic computation
                arguments = _args?.ToList() ?? new List<string>();
                alias = GenerateAlias(this);
            }

            /// <summary>
            /// Hidden constructor, retain a string array without redefining it to ease up the 2nd constructor process.
            /// </summary>
            /// <param name="_commandData">Every arguments sent</param>
            /// <param name="_callback"></param>
            private CommandData(string[] _commandData, CommandMethod _callback, string _permission)
                : this(_commandData?[0] ?? "__undefined__", _callback, _permission, _commandData?.Skip(1)) { }
            /// <summary>
            /// Second constructor, take the raw content of RA Command to parse it down.
            /// </summary>
            /// <param name="_command">RA Command content</param>
            /// <param name="_callback">Method callback (called on RA Command)</param>
            /// <param name="_permission">Permission requiered to run the command</param>
            public CommandData(string _command, CommandMethod _callback, string _permission)
                : this(_command.ToLower().Split(' '), _callback, _permission) { }

            /// <summary>
            /// Invoke stored callback.
            /// </summary>
            /// <param name="cs">Command sender (the one who called the command)</param>
            /// <param name="_args">Specified arguments</param>
            public void Invoke(CommandSender cs, string[] _args)
            {
                // Construct Dictionnary
                Dictionary<string, string> args = new Dictionary<string, string>();
                for (int ii = 0; ii < arguments.Count; ++ii)
                    args.Add(arguments[ii], _args?[ii] ?? "");

                // Find sending player
                ReferenceHub player = cs.SenderId == "SERVER CONSOLE" || cs.SenderId == "GAME CONSOLE" ?
                    PlayerManager.localPlayer.GetPlayer() : Player.GetPlayer(cs.SenderId);

                // Invoke command if possible
                if (player.CheckPermission(permission))
                    callback?.Invoke(player, args);
                else Log.Error("You don't have permission for this command...");
            }

            
            /// <summary>
            /// Static function, generate alias over a specified CommandData instance content.
            /// </summary>
            /// <param name="instance">Specific instance</param>
            /// <returns>Computed alias</returns>
            public static string GenerateAlias(CommandData instance)
            {
                return instance.name + instance.arguments.Count.ToString(); ;
            }
            /// <summary>
            /// Static function, generate alias over a string array (=raw RA Command).
            /// </summary>
            /// <param name="instance">Specific instance</param>
            /// <returns>Computed alias</returns>
            public static string GenerateAlias(string[] args)
            {
                return args?[0] + (args.Length - 1).ToString();
            }
        }


        /// <summary>
        /// Plugin reference.
        /// </summary>
        private Plugin plugin;

        /// <summary>
        /// Delegation template for every RA command method.
        /// </summary>
        /// <param name="sender">Command sender (the one who called the command)</param>
        /// <param name="args">Map of every arguments passed</param>
        public delegate void CommandMethod(ReferenceHub sender, Dictionary<string, string> args);
        /// <summary>
        /// Dictionnary of every commands possible.
        /// </summary>
        private Dictionary<string, CommandData> commands = new Dictionary<string, CommandData>();

        /// <summary>
        /// Default constructor, pass Plugin reference and allow to specify commands
        /// </summary>
        /// <param name="_plugin"></param>
        /// <param name="_commands"></param>
        /// <param name="_permission">Permission requiered to run the commands</param>
        public Commands(Plugin _plugin, Dictionary<string, CommandMethod> _commands = null, string _permission = "")
        {
            plugin = _plugin;
            AddCommands(_permission, _commands);
        }

        /// <summary>
        /// Add specifics command to the commands map.
        /// </summary>
        /// <param name="_commands">List of commands map</param>
        /// <param name="_permission">Permission requiered to run the commands</param>
        public void AddCommands(string _permission, Dictionary<string, CommandMethod> _commands)
        {
            if (_commands != null)
            {
                foreach (var ele in _commands)
                {
                    commands.Add(CommandData.GenerateAlias(ele.Key.ToLower().Split(' ').Skip(1).ToArray()),
                        new CommandData(ele.Key, ele.Value, _permission));
                }
            }
        }

        /// <summary>
        /// Linked to On Remote Admin Command event defined in EXILED.
        /// </summary>
        /// <param name="ev">Specific RACommand Event data</param>
        internal void OnRACommand(ref RACommandEvent ev)
        {
            // Forbid empty command lines
            if (string.IsNullOrEmpty(ev.Command))
                return;
            ev.Allow = false;

            // Filter out the commands not starting by a specific nametag
            string[] args = ev.Command.ToLower().Split(' ');
            if (plugin.pluginNameTypo.Contains(args[0]))
                return;

            // Invoke delegated event function (=callback)
            args = args.Skip(1).ToArray();
            commands[CommandData.GenerateAlias(args)]?.Invoke(ev.Sender, args);
        }
    }
}
