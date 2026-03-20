using DG.Tweening;
using Gaskellgames;
using UnityEngine;

namespace Ryadevn
{
    [CreateAssetMenu(fileName = nameof(HarvestableObjectData), menuName = "Data/" + nameof(HarvestableObjectData))]
    public class HarvestableObjectData : ScriptableObject
    {
        [Title("Settigs")]
        public float GrowthTime = 3f;
        public float DelayToGrowth = 5f;

        [Title("Animation")]
        public float DurationAnimation = .25f;
        public Ease EaseGrowth = Ease.InBack;
        public Ease EaseDestruction = Ease.OutBack;
    }
}
