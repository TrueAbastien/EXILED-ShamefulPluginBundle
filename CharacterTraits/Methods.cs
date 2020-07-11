using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;
using Trait = CharacterTraits.Traits.Trait;

namespace CharacterTraits
{
    public class Methods
    {
        private Plugin plugin;
        public Methods(Plugin _plugin) => plugin = _plugin;

        public List<string> GetTraitFrom(ReferenceHub player)
        {
            List<string> result = new List<string>();
            foreach (var trait in Trait.all)
                if (trait.Contains(player))
                    result.Add(trait.GetName());
            return result;
        }

        //
    }
}
