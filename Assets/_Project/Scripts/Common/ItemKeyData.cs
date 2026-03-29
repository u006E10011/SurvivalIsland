using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Ryadevn
{
    [CreateAssetMenu(fileName = nameof(ItemKeyData), menuName = "Data/" + nameof(ItemKeyData))]
    public class ItemKeyData : ScriptableObject
    {
        public SerializedDictionary<HarvestableObjectType, string> HarvestableKey = new();
        public SerializedDictionary<CraftedResourceType, string> CraftedKey = new();

        public string Get(InventorySaveDataBase data)
        {
            if (data is HarvestableSaveData harvestableData)
                return HarvestableKey[harvestableData.ResourceType];
            else if (data is CraftedResourceSaveData craftedData)
                return CraftedKey[craftedData.ResourceType];

            return string.Empty;
        }
    }
}
