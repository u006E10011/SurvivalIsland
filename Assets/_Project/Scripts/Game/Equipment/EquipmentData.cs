using Gaskellgames;
using System.Collections.Generic;
using UnityEngine;

namespace Ryadevn
{
    [CreateAssetMenu(fileName = nameof(EquipmentData), menuName = "Data/" + nameof(EquipmentData))]
    public class EquipmentData : ScriptableObject
    {
        public List<CraftRecipe> Recipes = new();

        private void OnValidate()
        {
            foreach (var recipe in Recipes)
                recipe.Name = $"{recipe.HarvestableType} ({recipe.CraftPrice}) -> {recipe.CraftType} ({recipe.CraftedAmount})";
        }
    }

    [System.Serializable]
    public class CraftRecipe
    {
        [HideInInspector] public string Name;

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
