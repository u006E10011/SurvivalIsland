using DG.Tweening;
using Gaskellgames;
using System.Linq;
using UnityEngine;

using dic = AYellowpaper.SerializedCollections;

namespace Ryadevn
{
    [CreateAssetMenu(fileName = nameof(MapData), menuName = "Data/" + nameof(MapData))]
    public class MapData : ScriptableObject
    {
        public string ID;

        [Title("Animation")]
        public float Duration = 1;
        public Ease Ease = Ease.InBack;

        [Title("Condition")]
        public dic.SerializedDictionary<HarvestableObjectType, int> HarvestableCondition = new();
        public dic.SerializedDictionary<CraftedResourceType, int> CraftedCondition = new();

        public bool IsUnloced => YG.YG2.saves.Maps.Any(x => x.Equals(ID));
    }
}
