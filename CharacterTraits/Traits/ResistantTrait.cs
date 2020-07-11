using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace CharacterTraits.Traits
{
    public class ResistantTrait : AbstractTrait
    {
        public ResistantTrait(TraitType _type) : base(_type)
        {
            Events.PlayerHurtEvent += OnPlayerHurt;
        }

        ~ResistantTrait()
        {
            Events.PlayerHurtEvent -= OnPlayerHurt;
        }

        private void OnPlayerHurt(ref PlayerHurtEvent ev)
        {
            if (affected.Contains(ev.Player))
                ev.Amount *= (type == TraitType.Positive) ? .85f : 1.1f;
        }

        protected override string GetNameNegative() => "Soft";
        protected override string GetNamePositive() => "Tough";
    }
}
