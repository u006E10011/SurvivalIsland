using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ryadevn
{
    public class MapUnlocker : MonoBehaviour
    {
        [SerializeField] private MapUnlockConditionDisplay _display;

        private Map _map;
        private MapData _data;
        private List<InventorySaveDataBase> _conditions;

        public void Init(Map map, MapData data)
        {
            _map = map;
            _data = data;

            if (data != null && _data.IsUnloced)
                Destroy(gameObject);

            ViewCondition();
            _display.Init(_conditions);
        }

        private void ViewCondition()
        {
            _conditions = new List<InventorySaveDataBase>();

            foreach (var item in _data.HarvestableCondition)
                _conditions.Add(new HarvestableSaveData(item.Key, item.Value));

            foreach (var item in _data.CraftedCondition)
                _conditions.Add(new CraftedResourceSaveData(item.Key, item.Value));

            InventoryPreview.OnRegister?.Invoke(_conditions);
        }

        private bool TryUnlock()
        {
            var harvestable = _data.HarvestableCondition.Any(x => x.Value > Inventory.GetResourceAmount(x.Key));
            var crafted = _data.CraftedCondition.Any(x => x.Value > Inventory.GetResourceAmount(x.Key));

            if (harvestable || crafted)
                return false;

            YG.YG2.saves.Maps.Add(_data.ID);
            YTools.AudioController.Get().Play("unlock_map");
            InventoryPreview.OnUnregister?.Invoke(_conditions);
            _conditions.ForEach(item => Inventory.OnRemove?.Invoke(item));

            return true;
        }

        private void Animation()
        {
            _map.gameObject.SetActive(true);
            _map.transform.DOScale(1, _data.Duration).SetEase(_data.Ease).SetLink(_map.gameObject)
                .OnComplete(() => Destroy(gameObject));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (TryUnlock())
                Animation();
        }
    }
}
