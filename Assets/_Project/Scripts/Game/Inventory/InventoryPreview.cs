using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ryadevn
{
    public class InventoryPreview : IDisposable
    {
        public static Action<List<InventorySaveDataBase>> OnRegister;
        public static Action<List<InventorySaveDataBase>> OnUnregister;

        private readonly List<InventorySaveDataBase> _requiredResources = new();
        private readonly Dictionary<(Type enumType, int value), InventoryItem> _items = new();
        private readonly Button _filterButton;

        private bool _toggleFilter;
        private Image _toggleIcon;

        public InventoryPreview(Dictionary<(Type enumType, int value), InventoryItem> items, Button button)
        {
            _items = items;
            _filterButton = button;

            _toggleIcon = button.transform.GetChild(0).GetComponent<Image>();

            OnRegister += Register;
            OnUnregister += Unregister;

            _filterButton.onClick.AddListener(ToggleFilter);
            _toggleIcon.enabled = _toggleFilter;
        }

        public void UpdatePreview()
        {
            foreach (var (key, value) in _items)
                value.gameObject.SetActive(!_toggleFilter || _requiredResources.Exists(x => x.IsSameResource(value.Data)));
        }

        private void ToggleFilter()
        {
            _toggleFilter = !_toggleFilter;
            _toggleIcon.enabled = _toggleFilter;
            UpdatePreview();
        }

        private void Register(List<InventorySaveDataBase> list)
        {
            foreach (var item in list)
            {
                var data = _requiredResources.Find(x => x.IsSameResource(item));

                if (data != null)
                    data.Amount += item.Amount;
                else
                    _requiredResources.Add(item);
            }
        }

        private void Unregister(List<InventorySaveDataBase> list)
        {
            foreach (var item in list)
            {
                var data = _requiredResources.Find(x => x.IsSameResource(item));

                if (data != null)
                {
                    data.Amount -= item.Amount;

                    if (data.Amount <= 0)
                        _requiredResources.Remove(data);
                }
            }
        }

        public void Dispose()
        {
            OnRegister -= Register;
            OnUnregister -= Unregister;
        }
    }
}
