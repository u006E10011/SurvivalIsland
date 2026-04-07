using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Ryadevn
{
    public class Inventory : MonoBehaviour
    {
        public static Action<InventorySaveDataBase> OnAdd;
        public static Action<InventorySaveDataBase> OnRemove;
        public static Action<InventorySaveDataBase> OnUpdateInventory;

        [SerializeField] private InventoryItem _item;
        [SerializeField] private Button _filterButton;
        [SerializeField] private Transform _container;

        private readonly Dictionary<(Type enumType, int value), InventoryItem> _items = new();
        public InventoryPreview Preview { get; private set; }

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

        public void Init()
        {
            foreach (var data in YG2.saves.InventorySaveData.GetAllItems())
            {
                if (data.Amount > 0)
                    CreateItem(data);
            }

            (Preview = new(_items, _filterButton)).UpdatePreview();
        }

        private void Add(InventorySaveDataBase data)
        {
            YG2.saves.InventorySaveData.AddResource(data);
            YG2.SaveProgress();
            InventoryAddedInfo.OnShow(data as HarvestableSaveData);
            UpdateInventory(data);
            Preview.UpdatePreview();
        }

        private void Remove(InventorySaveDataBase data)
        {
            YG2.saves.InventorySaveData.RemoveResource(data);
            YG2.SaveProgress();
            UpdateInventory(data);
            Preview.UpdatePreview();
        }

        private void UpdateInventory(InventorySaveDataBase data)
        {
            OnUpdateInventory?.Invoke(data);

            var key = (data.Type.GetType(), Convert.ToInt32(data.Type));

            if (_items.TryGetValue(key, out InventoryItem item))
            {
                var actualData = FindActualData(data);

                if (actualData == null || actualData.Amount <= 0)
                {
                    Destroy(item.gameObject);
                    _items.Remove(key);
                }
                else
                    item.UpdateAmount(actualData);

                return;
            }

            var newData = FindActualData(data);

            if (newData != null && newData.Amount > 0)
                CreateItem(newData);
        }

        private InventorySaveDataBase FindActualData(InventorySaveDataBase data)
        {
            var allItems = YG2.saves.InventorySaveData.GetAllItems();
            return allItems.Find(x => x.IsSameResource(data));
        }

        private void CreateItem(InventorySaveDataBase data)
        {
            var newItem = Instantiate(_item, _container);
            newItem.Init(data);
            var key = (data.Type.GetType(), Convert.ToInt32(data.Type));
            _items.Add(key, newItem);
        }

        public static int GetResourceAmount<T>(T type) where T : Enum
        {
            var allItems = YG2.saves.InventorySaveData.GetAllItems();
            var resource = allItems.Find(x => x.Type.GetType() == typeof(T) &&
                                             Convert.ToInt32(x.Type) == Convert.ToInt32(type));
            return resource?.Amount ?? 0;
        }

        public static bool HasResource<T>(T type, int amount) where T : Enum
        {
            return GetResourceAmount(type) >= amount;
        }

        private void OnDestroy()
        {
            Preview.Dispose();
        }
    }
}