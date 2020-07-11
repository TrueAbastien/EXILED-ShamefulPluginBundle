using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace Horcrux.Powers
{
    class ElasticityPower : Power
    {
        public ElasticityPower(Plugin _plugin) : base(_plugin, "Elasticity", PowerType.Elasticity) { }

        public override void subcribe()
        {
            Events.PlayerHurtEvent += NegateElectricalDamage;
        }

        private void NegateElectricalDamage(ref PlayerHurtEvent ev)
        {
            if (ev.Player.GetRole() == plugin.masterRole)
                if (ev.DamageType == DamageTypes.Tesla || ev.DamageType == DamageTypes.MicroHid)
                    ev.Amount = 0;
        }

        public override void unsubcribe()
        {
            Events.PlayerHurtEvent -= NegateElectricalDamage;
        }

        protected override void doCommand(ReferenceHub player) { }

        protected override void undoCommand(ReferenceHub player) { }
    }
}
