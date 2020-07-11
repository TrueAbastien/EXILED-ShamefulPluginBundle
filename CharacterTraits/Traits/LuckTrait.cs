using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace CharacterTraits.Traits
{
    public class LuckTrait : AbstractTrait
    {
        public LuckTrait(TraitType _type) : base(_type)
        {
            Events.Scp914UpgradeEvent += OnScp914Upgrade;
        }

        ~LuckTrait()
        {
            Events.Scp914UpgradeEvent -= OnScp914Upgrade;
        }

        private void OnScp914Upgrade(ref SCP914UpgradeEvent ev)
        {
            foreach (Pickup item in ev.Items)
            {
                if (affected.Contains(item.info.ownerPlayer.GetPlayer()))
                {
                    switch (item.ItemId)
                    {
                        case ItemType.Coin:
                            if (type == TraitType.Positive)
                                item.SetIDFull(ItemType.KeycardJanitor);
                            break;
                        case ItemType.GunLogicer:
                            item.SetIDFull((type == TraitType.Positive) ?
                                ItemType.MicroHID :
                                ItemType.GunLogicer);
                            break;
                        case ItemType.KeycardZoneManager:
                            if (type == TraitType.Positive)
                                item.SetIDFull(ItemType.KeycardO5);
                            break;
                        case ItemType.KeycardFacilityManager:
                            if (type == TraitType.Negative)
                                item.SetIDFull(ItemType.KeycardFacilityManager);
                            break;
                        default: break;
                    }
                }
            }
        }

        protected override string GetNameNegative() => "Unlucky";
        protected override string GetNamePositive() => "Lucky";
    }
}
