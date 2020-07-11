using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace Horcrux.Powers
{
    class StrengthPower : Power
    {
        public StrengthPower(Plugin _plugin) : base(_plugin, "Strength", PowerType.Strength) { }

        protected override void doCommand(ReferenceHub player) { }

        public override void subcribe()
        {
            Events.DoorInteractEvent += OpenDoor;
        }

        private void OpenDoor(ref DoorInteractionEvent ev)
        {
            if (ev.Player.GetRole() == plugin.masterRole && !ev.Door.locked)
                ev.Door.isOpen = true;
        }

        protected override void undoCommand(ReferenceHub player) { }

        public override void unsubcribe()
        {
            Events.DoorInteractEvent += OpenDoor;
        }
    }
}
