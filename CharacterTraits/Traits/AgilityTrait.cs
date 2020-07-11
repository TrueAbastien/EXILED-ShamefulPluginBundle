using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace CharacterTraits.Traits
{
    public class AgilityTrait : AbstractTrait
    {
        public AgilityTrait(TraitType _type) : base(_type)
        {
            Events.PlayerHurtEvent += OnPlayerHurt;
        }

        ~AgilityTrait()
        {
            Events.PlayerHurtEvent -= OnPlayerHurt;
        }

        private void OnPlayerHurt(ref PlayerHurtEvent ev)
        {
            if (affected.Contains(ev.Player))
                if (ev.DamageType == DamageTypes.Falldown)
                    ev.Amount *= (type == TraitType.Positive) ? .25f : 2f;
        }

        protected override string GetNameNegative() => "Numb";
        protected override string GetNamePositive() => "Dexterous";
    }
}
