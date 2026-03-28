using DG.Tweening;
using UnityEngine;

namespace Ryadevn
{
    [System.Serializable]
    internal class AnimationGetDamage
    {
        private HarvestableObjectData _data;
        private Tween _hitTween;
        private Transform _target;
        private Vector3 _scale;

        public AnimationGetDamage(HarvestableObjectData data, Transform target)
        {
            _data = data;   
            _target = target;
            _scale = target.localScale;
        }

        public void PlayHitAnimation()
        {
            _hitTween?.Kill();

            Sequence sequence = DOTween.Sequence();
            sequence.Append(_target.DOScale(_scale * _data.ScaleAmount, _data.ScaleDuration)
                .SetEase(_data.ScaleEaseStart));

            sequence.Append(_target.DOScale(_scale, _data.ScaleDuration)
                .SetEase(_data.ScaleEaseEnd));

            var vector = new Vector3
            {
                x = Random.Range(-_data.TargetRotation.x, _data.TargetRotation.x),
                y = Random.Range(-_data.TargetRotation.y, _data.TargetRotation.y),
                z = Random.Range(-_data.TargetRotation.z, _data.TargetRotation.z)
            };

            sequence.Join(_target.DORotate(vector, _data.ScaleDuration * 0.5f)
                .SetEase(_data.RotationEase)
                .OnComplete(() => ResetRotation(_target)));

            _hitTween = sequence;
            _hitTween.Play();
        }

        private void ResetRotation(Transform target)
        {
            target.DORotate(Vector3.zero, 0.2f)
                .SetEase(Ease.OutBack);
        }

        public void CancelAnimation()
        {
            _hitTween?.Kill();
        }
    }

}