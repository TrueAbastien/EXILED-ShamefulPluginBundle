using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterTraits.Traits
{
    public enum TraitType
    {
        None = -1,
        Positive = 0,
        Negative = 1
    }

    public abstract class AbstractTrait
    {
        protected List<ReferenceHub> affected = new List<ReferenceHub>();
        
        public TraitType type { get; protected set; }
        public AbstractTrait(TraitType _type)
        {
            type = _type;
            Trait.all.Add(this);
        }

        public void OnSpawn(ReferenceHub player)
        {
            if (affected.Contains(player))
                return;
            affected.Add(player);

            switch (type)
            {
                case TraitType.Positive:
                    OnSpawnPositive(player);
                    break;
                case TraitType.Negative:
                    OnSpawnNegative(player);
                    break;
                default: break;
            }
        }
        protected virtual void OnSpawnPositive(ReferenceHub player) { }
        protected virtual void OnSpawnNegative(ReferenceHub player) { }

        public void OnDeath(ReferenceHub player)
        {
            if (!affected.Remove(player))
                return;

            switch (type)
            {
                case TraitType.Positive:
                    OnDeathPositive(player);
                    break;
                case TraitType.Negative:
                    OnDeathNegative(player);
                    break;
                default: break;
            }
        }
        protected virtual void OnDeathPositive(ReferenceHub player) { }
        protected virtual void OnDeathNegative(ReferenceHub player) { }

        public string GetName(TraitType _type = TraitType.None)
        {
            switch (_type == TraitType.None ? type : _type)
            {
                case TraitType.Positive:
                    return GetNamePositive();
                case TraitType.Negative:
                    return GetNameNegative();
                default: return "__undefined__";
            }
        }
        protected abstract string GetNamePositive();
        protected abstract string GetNameNegative();

        public void Add(ReferenceHub player)
        {
            if (!affected.Contains(player))
            {
                affected.Add(player);
                OnSpawn(player);
            }
        }
        public void Remove(ReferenceHub player)
        {
            if (affected.Remove(player))
            {
                OnDeath(player);
            }
        }
        public bool Contains(ReferenceHub player)
        {
            return affected?.Contains(player) ?? false;
        }
    }
}
