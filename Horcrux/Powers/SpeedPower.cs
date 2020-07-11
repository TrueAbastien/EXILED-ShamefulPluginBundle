using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;
using CustomPlayerEffects;

namespace Horcrux.Powers
{
    class SpeedPower : Power
    {
        public SpeedPower(Plugin _plugin) : base(_plugin, "Speed", PowerType.Speed) { }

        protected override void doCommand(ReferenceHub player)
        {
            player.effectsController.GetEffect<Scp207>("SCP-207").ServerEnable();
        }

        public override void subcribe()
        {
            Events.PlayerHurtEvent += Negate207Damage;
        }

        private void Negate207Damage(ref PlayerHurtEvent ev)
        {
            if (ev.Player.GetRole() == plugin.masterRole && ev.DamageType == DamageTypes.Scp207)
                ev.Amount = 0;
        }

        protected override void undoCommand(ReferenceHub player)
        {
            player.effectsController.GetEffect<Scp207>("SCP-207").ServerDisable();
        }

        public override void unsubcribe()
        {
            Events.PlayerHurtEvent -= Negate207Damage;
        }
    }
}
