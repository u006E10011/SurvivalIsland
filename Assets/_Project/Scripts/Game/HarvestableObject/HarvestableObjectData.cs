using AYellowpaper.SerializedCollections;
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

        [Title("Harvest Animation")]
        [Indent(1)] public float DurationAnimation = .25f;
        [Indent(1)] public Ease EaseGrowth = Ease.InBack;
        [Indent(1)] public Ease EaseDestruction = Ease.OutBack;

        [Title("Hit Animation")]
        [Indent(1)] public float ScaleDuration = 0.2f;
        [Indent(1)] public float ScaleAmount = 0.9f;
        [Indent(1)] public Ease ScaleEaseStart = Ease.OutBack;
        [Indent(1)] public Ease ScaleEaseEnd = Ease.OutQuad;

        [Space(10)]
        [Indent(1)] public Vector3 TargetRotation = new(3, 0, 3);
        [Indent(1)] public Ease RotationEase = Ease.OutQuad;

        [Space(10)]
        [SerializedDictionary("Type", "Amount")]
        public AYellowpaper.SerializedCollections.SerializedDictionary<HarvestableObjectType, int> HarvestDrop = new();
    }
}
