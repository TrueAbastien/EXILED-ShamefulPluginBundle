using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace Horcrux.Powers
{
    class ShrinkPower : Power
    {
        public ShrinkPower(Plugin _plugin) : base(_plugin, "Agility", PowerType.Shrink) { }

        public override void subcribe() { }

        public override void unsubcribe() { }

        protected override void doCommand(ReferenceHub player)
        {
            player.SetScale(plugin.shrink_GlobalScale);
        }

        protected override void undoCommand(ReferenceHub player)
        {
            player.SetScale(1);
        }
    }
}
