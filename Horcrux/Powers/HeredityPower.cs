using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace Horcrux.Powers
{
    class HeredityPower : Power
    {
        public HeredityPower(Plugin _plugin) : base(_plugin, "Heredity", PowerType.Heredity) { }

        public override void subcribe()
        {
            Events.PlayerHurtEvent += HealOnDamage;
        }

        public override void unsubcribe()
        {
            Events.PlayerHurtEvent -= HealOnDamage;
        }

        private void HealOnDamage(ref PlayerHurtEvent ev)
        {
            if (ev.Attacker.GetRole() == plugin.slaveRole)
            {
                foreach (ReferenceHub player in Player.GetHubs().Where(x => x.GetRole() == plugin.masterRole))
                    player.AddHealth(ev.Amount * plugin.heredity_HealPercentage);
            }
        }

        protected override void doCommand(ReferenceHub player) { }

        protected override void undoCommand(ReferenceHub player) { }
    }
}
