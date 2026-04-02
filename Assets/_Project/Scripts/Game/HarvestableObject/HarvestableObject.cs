using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using YTools;
using Gaskellgames;
using DG.Tweening;

namespace Ryadevn
{
    [SelectionBase]
    public class HarvestableObject : MonoBehaviour
    {
        [SerializeField] private HarvestableObjectType _type;
        [SerializeField] private List<HarvestableObjectSegment> _segments = new();

        public HarvestableObjectType Type => _type;
        public bool IsDestroyed => _currentLevel < 0;

        private int _currentLevel = 0;
        private WaitForSeconds _growthTime;
        private WaitForSeconds _delayToGrowth;
        private Coroutine _growth;
        private HarvestableObjectData _data;
        private AnimationGetDamage _animation;

        private void Awake()
        {
            _data = Resources.Load<HarvestableObjectData>("Data/" + nameof(HarvestableObjectData));
            _growthTime = new(_data.GrowthTime);
            _delayToGrowth = new(_data.DelayToGrowth);
            _currentLevel = _segments.Count - 1;
            _animation = new(_data, transform);
        }

        [Button]
        public void PlayHitAnimation() => _animation.PlayHitAnimation();

        [Button]
        public void TakeDamage()
        {
            if (_currentLevel < 0)
                return;

            _segments[_currentLevel].Destruction();
            _currentLevel--;
            _animation.PlayHitAnimation();

            if (_growth != null)
                StopCoroutine(_growth);

            _growth = StartCoroutine(Regeniration());

            AudioController.Get().Play(_type.ToString());
            Inventory.OnAdd?.Invoke(new HarvestableSaveData(_type, _data.HarvestDrop[_type]));
        }

        private IEnumerator Regeniration()
        {
            yield return _delayToGrowth;

            while (_currentLevel != _segments.Count - 1)
            {
                _currentLevel++;
                _segments[_currentLevel].Growth();

                yield return _growthTime;
            }

            _growth = null;
        }
    }
}
