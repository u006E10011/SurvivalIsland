using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ryadevn
{
    internal class InventoryIconProvider
    {
        private static List<Sprite> _icons;

        static InventoryIconProvider()
        {
            _icons = Resources.LoadAll<Sprite>("Icons").ToList();
        }

        public static Sprite Get(HarvestableObjectType type)
        {
            return _icons.Find(x => x.name.ToLower().Equals(type.ToString().ToLower()));
        }
    }
}
