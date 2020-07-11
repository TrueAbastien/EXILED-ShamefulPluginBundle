using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace CharacterTraits.Traits
{
    public class SizeTrait : AbstractTrait
    {
        public SizeTrait(TraitType _type) : base(_type) { }

        protected override string GetNameNegative() => "Tall";

        protected override string GetNamePositive() => "Small";

        protected override void OnDeathNegative(ReferenceHub player)
        {
            player.SetScale(1f);
        }

        protected override void OnDeathPositive(ReferenceHub player)
        {
            player.SetScale(1f);
        }

        protected override void OnSpawnNegative(ReferenceHub player)
        {
            player.SetScale(1.25f);
        }

        protected override void OnSpawnPositive(ReferenceHub player)
        {
            player.SetScale(.8f);
        }
    }
}
