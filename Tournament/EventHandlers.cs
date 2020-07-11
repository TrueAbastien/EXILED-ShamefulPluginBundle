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
    /// Define most Event Handlers attached in EXILED Plugin.
    /// </summary>
    public class EventHandlers
    {
        /// <summary>
        /// Plugin reference.
        /// </summary>
        private Plugin plugin;

        /// <summary>
        /// Default constructor, pass specified plugin as reference.
        /// </summary>
        /// <param name="_plugin">Current plugin</param>
        public EventHandlers(Plugin _plugin) => plugin = _plugin;

        /// <summary>
        /// Linked to On Player Leave event defined in EXILED.
        /// </summary>
        /// <param name="ev">Specified PlayerLeave Event data</param>
        internal void OnPlayerLeave(PlayerLeaveEvent ev)
        {
            plugin.methods.RemovePlayer(ev.Player);
        }

        /// <summary>
        /// Linked to On Player Death event defined in EXILED.
        /// </summary>
        /// <param name="ev">Specified PlayerDeath Event data</param>
        internal void OnPlayerDeath(ref PlayerDeathEvent ev)
        {
            // Canceled out if tournament is not running
            if (!plugin.isRunning)
                return;

            // Remove the current Player from the current pool (if possible)
            if (plugin.methods.RemovePlayer(ev.Player))
            {
                // Instantiate next fight (if possible)
                if (!plugin.methods.NextFight())
                {
                    if (plugin.playerPool.Count == 1)
                    {
                        Map.Broadcast($"{ plugin.playerPool[0].GetNickname() } just won the tournament, congrats !", 5, false);
                        plugin.methods.EndTournament();
                    }
                }
            }
        }
    }
}
