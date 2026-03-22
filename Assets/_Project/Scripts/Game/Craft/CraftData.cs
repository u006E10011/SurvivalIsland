using Gaskellgames;
using System.Collections.Generic;
using UnityEngine;

namespace Ryadevn
{
    public class CraftData : ScriptableObject
    {
        public List<CraftRecipe> Recipes = new();
    }

    [System.Serializable]
    public class CraftRecipe
    {
        [Title("Crafted")]
        public CraftedResourceType ResourcesType;
        public int Amount;

        [Title("Contidions")]
        public AYellowpaper.SerializedCollections.SerializedDictionary<HarvestableObjectType, int> HarvestablObjectCondition = new();
        public AYellowpaper.SerializedCollections.SerializedDictionary<CraftedResourceType, int> CraftedResourceCondition = new();
    }
}
