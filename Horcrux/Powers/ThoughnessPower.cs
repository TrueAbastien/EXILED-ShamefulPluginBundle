using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace Horcrux.Powers
{
    class ThoughnessPower : Power
    {
        public ThoughnessPower(Plugin _plugin) : base(_plugin, "Thougness", PowerType.Thoughness) { }

        public override void subcribe()
        {
            Events.PlayerHurtEvent += NegateProjectileDamage;
        }

        public override void unsubcribe()
        {
            Events.PlayerHurtEvent -= NegateProjectileDamage;
        }

        private void NegateProjectileDamage(ref PlayerHurtEvent ev)
        {
            if (ev.Player.GetRole() == plugin.masterRole && ev.DamageType == DamageTypes.Grenade)
                ev.Amount = 0;
        }

        protected override void doCommand(ReferenceHub player) { }

        protected override void undoCommand(ReferenceHub player) { }
    }
}
