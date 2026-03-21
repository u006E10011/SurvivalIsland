using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Ryadevn
{
    public class Inventory : MonoBehaviour
    {
        public static System.Action<HarvestableObjectType, int> OnAdd;
        public static System.Action<HarvestableObjectType, int> OnRemove;

        [SerializeField] private InventoryItem _item;
        [SerializeField] private Transform _container;

        private readonly Dictionary<HarvestableObjectType, InventoryItem> _items = new();

        private void Awake()
        {
            Init();
        }

        private void OnEnable()
        {
            OnAdd += Add;
            OnRemove += Remove;
        }

        private void OnDisable()
        {
            OnAdd -= Add;
            OnRemove -= Remove;
        }

        private void Init()
        {
            foreach (var data in YG2.saves.InventorySaveData)
                CreateItem(data);
        }

        private void Add(HarvestableObjectType type, int amount)
        {
            var index = YG2.saves.InventorySaveData.FindIndex(x => x.Type == type);

            if (index == -1)
            {
                YG2.saves.InventorySaveData.Add(new(type, amount));
                UpdateInventory(YG2.saves.InventorySaveData[^1]);
            }
            else
            {
                YG2.saves.InventorySaveData[index].Amount += amount;
                UpdateInventory(YG2.saves.InventorySaveData[index]);
            }

            YG2.SaveProgress();
        }

        private void Remove(HarvestableObjectType type, int amount)
        {
            var index = YG2.saves.InventorySaveData.FindIndex(x => x.Type == type);

            if (index == -1)
                return;

            YG2.saves.InventorySaveData[index].Amount -= amount;
            YG2.SaveProgress();
            UpdateInventory(YG2.saves.InventorySaveData[index]);
        }

        private void UpdateInventory(InventorySaveData data)
        {
            if (_items.TryGetValue(data.Type, out InventoryItem item))
            {
                if (data.Amount > 0)
                    item.UpdateAmount(data);
                else
                    Destroy(_items[data.Type].gameObject);

                return;
            }

            if (data.Amount <= 0)
                return;

            CreateItem(data);
        }

        private void CreateItem(InventorySaveData data)
        {
            var newItem = Instantiate(_item, _container);
            newItem.Init(data);
            _items.Add(data.Type, newItem);
        }
    }
}
