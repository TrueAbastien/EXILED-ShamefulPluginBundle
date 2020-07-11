using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterTraits.Traits
{
    public static class Trait
    {
        public static List<AbstractTrait> all = new List<AbstractTrait>();
        public static int count => all?.Count / 2 ?? 0;

        public static SizeTrait
            SmallTrait = new SizeTrait(TraitType.Positive),
            TallTrait = new SizeTrait(TraitType.Negative);
        public static PrecisionTrait
            SharpShooterTrait = new PrecisionTrait(TraitType.Positive),
            ImpreciseTrait = new PrecisionTrait(TraitType.Negative);
        public static ResistantTrait
            ToughTrait = new ResistantTrait(TraitType.Positive),
            SoftTrait = new ResistantTrait(TraitType.Negative);
        public static MedicTrait
            CaringTrait = new MedicTrait(TraitType.Positive),
            RoughTrait = new MedicTrait(TraitType.Negative);
        public static LuckTrait
            LuckyTrait = new LuckTrait(TraitType.Positive),
            UnluckyTrait = new LuckTrait(TraitType.Negative);
        public static AgilityTrait
            DexterousTrait = new AgilityTrait(TraitType.Positive),
            NumbTrait = new AgilityTrait(TraitType.Negative);
        public static AttentionTrait
            WaryTrait = new AttentionTrait(TraitType.Positive),
            CarelessTrait = new AttentionTrait(TraitType.Negative);
        public static HealthTrait
            HealthyTrait = new HealthTrait(TraitType.Positive),
            FeebleTrait = new HealthTrait(TraitType.Negative);
        public static SensibilityTrait
            StonyTrait = new SensibilityTrait(TraitType.Positive),
            UnhingedTrait = new SensibilityTrait(TraitType.Negative);
        public static ExplosionTrait
            CraftyTrait = new ExplosionTrait(TraitType.Positive),
            ExplosiveTrait = new ExplosionTrait(TraitType.Negative);
    }
}
