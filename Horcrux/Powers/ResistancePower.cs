using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace Horcrux.Powers
{
    class ResistancePower : Power
    {
        public ResistancePower(Plugin _plugin) : base(_plugin, "Resistance", PowerType.Resistance) { }

        public override void subcribe()
        {
            Events.PlayerHurtEvent += ReduceDamage;
        }

        private void ReduceDamage(ref PlayerHurtEvent ev)
        {
            if (ev.Player.GetRole() == plugin.masterRole)
                ev.Amount *= plugin.resistance_DmgPercentage;
        }

        public override void unsubcribe()
        {
            Events.PlayerHurtEvent -= ReduceDamage;
        }

        protected override void doCommand(ReferenceHub player) { }

        protected override void undoCommand(ReferenceHub player) { }
    }
}
