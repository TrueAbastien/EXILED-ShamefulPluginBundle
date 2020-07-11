using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace CharacterTraits.Traits
{
    public class PrecisionTrait : AbstractTrait
    {
        public PrecisionTrait(TraitType _type) : base(_type)
        {
            Events.PlayerHurtEvent += OnPlayerHurt;
        }

        ~PrecisionTrait()
        {
            Events.PlayerHurtEvent -= OnPlayerHurt;
        }

        private void OnPlayerHurt(ref PlayerHurtEvent ev)
        {
            if (ev.Attacker != null)
                if (affected.Contains(ev.Attacker))
                    if (ev.DamageType.isWeapon)
                        ev.Amount *= (type == TraitType.Positive) ? 1.25f : .8f;
        }

        protected override string GetNameNegative() => "Imprecise";
        protected override string GetNamePositive() => "SharpShooter";
    }
}
