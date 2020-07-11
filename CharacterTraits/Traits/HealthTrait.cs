using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace CharacterTraits.Traits
{
    public class HealthTrait : AbstractTrait
    {
        public HealthTrait(TraitType _type) : base(_type) { }

        protected virtual void OnSpawnPositive(ReferenceHub player)
        {
            player.SetMaxHealth(player.GetMaxHealth() * 1.25f);
        }
        protected virtual void OnSpawnNegative(ReferenceHub player)
        {
            player.SetMaxHealth(player.GetMaxHealth() * .85f);
        }

        protected override string GetNameNegative() => "Feeble";
        protected override string GetNamePositive() => "Healthy";
    }
}
