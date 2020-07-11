using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;
using EXILED.ApiObjects;

namespace CharacterTraits.Traits
{
    public class MedicTrait : AbstractTrait
    {
        public MedicTrait(TraitType _type) : base(_type)
        {
            Events.UseMedicalItemEvent += OnUsedMedicalItem;
        }

        ~MedicTrait()
        {
            Events.UseMedicalItemEvent -= OnUsedMedicalItem;
        }

        private void OnUsedMedicalItem(MedicalItemEvent ev)
        {
            if (affected.Contains(ev.Player))
            {
                if (type == TraitType.Positive)
                    ev.Player.AddHealth(25);
                else ev.Player.AddHealth(-10);
            }
                
        }

        protected override string GetNameNegative() => "Rough";
        protected override string GetNamePositive() => "Caring";
    }
}
