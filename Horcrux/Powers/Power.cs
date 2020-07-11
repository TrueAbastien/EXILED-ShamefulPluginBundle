using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace Horcrux.Powers
{
    public abstract class Power
    {
        public enum PowerType
        {
            None = 0,
            Speed = 1,      // Boost in Movement Speed
            Strength = 2,   // Can open every (unlocked) doors
            Resistance = 3, // Take less damage
            Shrink = 4,     // Reduce player size
            Elasticity = 5, // HID & Tesla invulnerability
            Thoughness = 6, // Projectile resistance
            Heredity = 7    // Heal you on slave role damage
        }
        [Serializable]
        public class PowerData
        {
            public string name { get; private set; }
            public PowerType type { get; private set; }

            public PowerData(string _name, PowerType _type)
            {
                name = _name;
                type = _type;
            }
        }

        protected bool enabled;
        protected Plugin plugin;

        public PowerData data { get; protected set; }
        public Power(Plugin _plugin, string _name, PowerType _type)
        {
            plugin = _plugin;
            data = new PowerData(_name, _type);
        }

        public abstract void subcribe();
        public abstract void unsubcribe();

        protected abstract void doCommand(ReferenceHub player);
        protected abstract void undoCommand(ReferenceHub player);

        public void give(ReferenceHub player)
        {
            doCommand(player);
            player.Broadcast(3, $"You just gained { data.name } power !", false);
        }
        public void take(ReferenceHub player)
        {
            undoCommand(player);
            player.Broadcast(3, $"You just lost { data.name } power...", false);
        }
    }
}
