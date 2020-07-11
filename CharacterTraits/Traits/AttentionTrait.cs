using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace CharacterTraits.Traits
{
    public class AttentionTrait : AbstractTrait
    {
        private List<ReferenceHub> covered = new List<ReferenceHub>();

        public AttentionTrait(TraitType _type) : base(_type)
        {
            Events.PlayerHurtEvent += OnPlayerHurt;
        }

        ~AttentionTrait()
        {
            Events.PlayerHurtEvent -= OnPlayerHurt;
        }

        private void OnPlayerHurt(ref PlayerHurtEvent ev)
        {
            if (affected.Contains(ev.Player))
            {
                switch (type)
                {
                    case TraitType.Positive:
                        {
                            if (covered.Remove(ev.Player))
                                ev.Amount = 0;
                            else if (ev.DamageType == DamageTypes.Tesla)
                                ev.Amount = 25;
                        }
                        break;
                    case TraitType.Negative:
                        if (ev.Attacker?.GetTeam() == Team.SCP)
                        {
                            ev.Amount *= 1.25f;
                        }
                        break;
                    default: break;
                }
            }
                
        }

        protected override string GetNameNegative() => "Careless";
        protected override string GetNamePositive() => "Wary";

        protected override void OnSpawnPositive(ReferenceHub player)
        {
            if (!covered.Contains(player))
                covered.Add(player);
        }
        protected override void OnDeathPositive(ReferenceHub player)
        {
            covered.Remove(player);
        }
    }
}
