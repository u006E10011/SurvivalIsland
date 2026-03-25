using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Ryadevn
{
    [CreateAssetMenu(fileName = nameof(ItemKeyData), menuName = "Data/" + nameof(ItemKeyData))]
    public class ItemKeyData : ScriptableObject
    {
        public SerializedDictionary<HarvestableObjectType, string> HarvestableKey = new();
        public SerializedDictionary<CraftedResourceType, string> CraftedKey = new();
    }
}
