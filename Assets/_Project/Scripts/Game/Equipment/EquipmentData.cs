using Gaskellgames;
using System.Collections.Generic;
using UnityEngine;

namespace Ryadevn
{
    [CreateAssetMenu(fileName = nameof(EquipmentData), menuName = "Data/" + nameof(EquipmentData))]
    public class EquipmentData : ScriptableObject
    {
        public List<CraftRecipe> Recipes = new();
    }

    [System.Serializable]
    public class CraftRecipe
    {
        [Title("Crafted")]
        public CraftedResourceType CraftType;
        public int CraftedAmount = 1;
        public float CraftedRate = 1;

        [Title("Contidions")]
        public HarvestableObjectType HarvestableType;
        public int CraftPrice = 1;
    }

    [System.Serializable]
    public class EquipmentTask
    {
        public int Count;
        public CraftedResourceSaveData CraftedResource;
        public HarvestableSaveData HarvestableResource;
    }
}
