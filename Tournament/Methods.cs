using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;
using MEC;
using UnityEngine;
using Mirror;

namespace Tournament
{
    /// <summary>
    /// Contains every reusable methods which don't belong in the Plugin class.
    /// </summary>
    public class Methods
    {
        /// <summary>
        /// Plugin reference.
        /// </summary>
        private Plugin plugin;

        /// <summary>
        /// Default constructor, pass specified plugin as reference.
        /// </summary>
        /// <param name="_plugin">Current plugin</param>
        public Methods(Plugin _plugin) => plugin = _plugin;

        /// <summary>
        /// Starts the tournament (RA command).
        /// </summary>
        /// <param name="sender">Command sender (the one who called the command)</param>
        /// <param name="args">Map of every arguments passed</param>
        public void TournamentStart(ReferenceHub sender, Dictionary<string, string> args)
        {
            // Forbid to start a new tournament while one is active
            if (plugin.isRunning)
            {
                Log.Warn("A tournament is already active !");
                return;
            }
            plugin.isRunning = true;

            // Construct player pool
            plugin.playerPool = Player.GetHubs().ToList();
            if (plugin.excludeSender)
                plugin.playerPool.Remove(sender);

            // Verify the current amount of player in player pool
            if (plugin.playerPool.Count < 2)
            {
                Log.Error("Not enough player to start a tournament...");
                EndTournament();

                return;
            }

            // Pass every Player as invisible
            foreach (var player in plugin.playerPool)
                player.SetRole(RoleType.Spectator);

            // Shuffle list and pick the last two player for the next fight
            plugin.playerPool.ShuffleList();
            NextFight();
        }

        /// <summary>
        /// Print out informations regarding the tournament state (RA command).
        /// </summary>
        /// <param name="sender">Command sender (the one who called the command)</param>
        /// <param name="args">Map of every arguments passed</param>
        public void TournamentInfos(ReferenceHub sender, Dictionary<string, string> args)
        {
            if (plugin.isRunning)
            {
                if (plugin.currentPool.Count >= 2)
                    Log.Info($"Current fight between: { string.Join(" & ", plugin.currentPool.Select(x => x.GetNickname())) }.");
                else Log.Info("No fight is currently ongoing...");

                Log.Info(
                    $"Fight delay: { plugin.fightDelay }s\n" +
                    $"Fight room: { plugin.fightRoom }\n" +
                    $"Gun item: { Enum.GetName(typeof(ItemType), plugin.tournamentGun) } ({ (int)plugin.tournamentGun })\n" +
                    $"Player pool: { string.Join(", ", plugin.playerPool.Select(x => x.GetNickname())) }");
            }
            else Log.Warn("Tournament is not running !");
        }

        /// <summary>
        /// Change the current localization of the fight room (RA Command).
        /// </summary>
        /// <param name="sender">Command sender (the one who called the command)</param>
        /// <param name="args">Map of every arguments passed</param>
        public void TournamentLocalize(ReferenceHub sender, Dictionary<string, string> args)
        {
            string roomName = args["roomName"];
            if (Map.Rooms.Where(x => x.Name == roomName).ToArray().Length > 0)
                plugin.fightRoom = roomName;
        }

        /// <summary>
        /// Change the weapon given to any fighting player (RA Command).
        /// </summary>
        /// <param name="sender">Command sender (the one who called the command)</param>
        /// <param name="args">Map of every arguments passed</param>
        public void TournamentArm(ReferenceHub sender, Dictionary<string, string> args)
        {
            // Compute current Item ID
            ItemType weapon;
            try { weapon = (ItemType)int.Parse(args["weapon"]); }
            catch(Exception) { return; }

            // Verify if is a Weapon
            if (weapon.IsWeapon(true))
                plugin.tournamentGun = weapon;
        }

        /// <summary>
        /// Stops the tournament (RA command).
        /// </summary>
        /// <param name="sender">Command sender (the one who called the command)</param>
        /// <param name="args">Map of every arguments passed</param>
        public void TournamentStop(ReferenceHub sender, Dictionary<string, string> args)
        {
            if (EndTournament())
            {
                Log.Info($"{ sender.GetNickname() } has just ended the event.");
                Map.Broadcast("The tournament had to come to an end !", 5, false);
            }
            else Log.Error($"{ sender.GetNickname() } tried to end the event but failed !");
        }

        /// <summary>
        /// Start the next fight between the two players on bottom Player Pool list.
        /// </summary>
        /// <returns>Verify if next fight started</returns>
        public bool NextFight()
        {
            // Can't be processed: player pool is too small
            if (plugin.playerPool.Count < 2)
                return false;
            
            // Kill previous Fighters
            foreach (var player in plugin.currentPool)
                player.SetRole(RoleType.Spectator);
            plugin.currentPool.Clear();

            // Cleanup Every Pickup
            foreach (var item in Pickup.Instances)
            {
                item.Delete();
            }

            // Add the two bottom Player to the current pool
            for (int ii = 0; ii < 2; ++ii)
            {
                plugin.currentPool.Add(plugin.playerPool.Last());
                plugin.playerPool.RemoveAt(plugin.playerPool.Count - 1);
            }
            Map.Broadcast($"Next match between { string.Join(" & ", plugin.currentPool.Select(x => x.GetNickname())) }.", 5, false);

            // Bring new Players
            Vector3 roomPosition = Map.Rooms.Where(x => x.Name.Equals(plugin.fightRoom)).ToArray()[0].Position;
            foreach (var player in plugin.currentPool)
            {
                player.SetRole(RoleType.ClassD);
                player.SetPosition(roomPosition);
            }

            // Give new Players guns
            Timing.CallDelayed(plugin.fightDelay, () =>
            {
                foreach (var player in plugin.currentPool)
                    player.AddItem(plugin.tournamentGun);
                Map.Broadcast("Fight !", 3, false);
            });

            return true;
        }

        /// <summary>
        /// End the tournament either forcefully or automatically.
        /// </summary>
        /// <returns>Verify if tournament did end</returns>
        public bool EndTournament()
        {
            // Only trigger if a tournament is running
            if (!plugin.isRunning)
            {
                Log.Warn("No tournament has been started...");
                return false;
            }
            plugin.isRunning = false;
            
            // Kill Fighters
            foreach (var player in plugin.currentPool)
                player.SetRole(RoleType.Spectator);

            // Empty Pools
            plugin.playerPool.Clear();
            plugin.currentPool.Clear();

            return true;
        }

        /// <summary>
        /// Remove player from the current pool or from Player Pool.
        /// </summary>
        /// <param name="player">Specific dead player</param>
        /// <returns>Sends out the result of any removal</returns>
        public bool RemovePlayer(ReferenceHub player)
        {
            if (plugin.currentPool.Remove(player))
            {
                PlayerVictory(plugin.currentPool?[0]);
                return true;
            }
            return plugin.playerPool.Remove(player);
        }

        /// <summary>
        /// Clear current pool and add the winner on top of the Player Pool.
        /// </summary>
        /// <param name="player">Specific player/winner</param>
        public void PlayerVictory(ReferenceHub player)
        {
            plugin.playerPool.Insert(0, player);
            plugin.currentPool.Clear();
        }
    }
}
