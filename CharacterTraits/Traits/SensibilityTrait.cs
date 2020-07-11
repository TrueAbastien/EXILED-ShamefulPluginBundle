using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace CharacterTraits.Traits
{
    public class SensibilityTrait : AbstractTrait
    {
        public SensibilityTrait(TraitType _type) : base(_type)
        {
            Events.PlayerHurtEvent += OnPlayerHurt;
        }

        ~SensibilityTrait()
        {
            Events.PlayerHurtEvent -= OnPlayerHurt;
        }

        private void OnPlayerHurt(ref PlayerHurtEvent ev)
        {
            if (affected.Contains(ev.Player))
            {
                if (ev.DamageType == DamageTypes.Scp207)
                {
                    if (type == TraitType.Positive)
                        ev.Amount = 0;
                }

                else if (ev.DamageType == DamageTypes.Pocket)
                {
                    ev.Amount *= (type == TraitType.Positive) ? .5f : 3f;
                }
            }
        }

        protected override string GetNameNegative() => "Unhinged";
        protected override string GetNamePositive() => "Stony";
    }
}
