using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace CharacterTraits.Traits
{
    public class ExplosionTrait : AbstractTrait
    {
        public ExplosionTrait(TraitType _type) : base(_type)
        {
            Events.GrenadeThrownEvent += OnGrenadeThrown;
            Events.PlayerHurtEvent += OnPlayerHurt;
        }

        ~ExplosionTrait()
        {
            Events.GrenadeThrownEvent -= OnGrenadeThrown;
            Events.PlayerHurtEvent -= OnPlayerHurt;
        }

        private void OnGrenadeThrown(ref GrenadeThrownEvent ev)
        {
            if (affected.Contains(ev.Player))
                if (type == TraitType.Negative)
                    ev.Fuse = .1f;
        }
        private void OnPlayerHurt(ref PlayerHurtEvent ev)
        {
            if (affected.Contains(ev.Attacker))
                if (type == TraitType.Positive)
                    if (ev.DamageType == DamageTypes.Grenade)
                        ev.Amount *= 2;
        }

        protected override void OnSpawnPositive(ReferenceHub player)
        {
            player.AddItem(ItemType.GrenadeFrag);
        }

        protected override string GetNameNegative() => "Explosive";
        protected override string GetNamePositive() => "Crafty";
    }
}
