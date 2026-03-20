using DG.Tweening;
using UnityEngine;

namespace Ryadevn
{
    public class HarvestableObjectSegment : MonoBehaviour
    {
        private Vector3 _scale;
        private HarvestableObjectData _data;

        private void Awake()
        {
            _scale = transform.localScale;
            _data = Resources.Load<HarvestableObjectData>("Data/" + nameof(HarvestableObjectData));
        }

        public void Growth()
        {
            transform.DOScale(_scale, _data.DurationAnimation).SetEase(_data.EaseGrowth).SetLink(gameObject);
        }

        public void Destruction()
        {
            transform.DOScale(0, _data.DurationAnimation).SetEase(_data.EaseDestruction).SetLink(gameObject);
        }
    }
}
