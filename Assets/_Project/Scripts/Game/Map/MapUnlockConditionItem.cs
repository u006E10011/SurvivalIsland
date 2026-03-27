using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ryadevn
{
    internal class MapUnlockConditionItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _conditionText;
        [SerializeField] private Image _icon;

        private InventorySaveDataBase _condition;

        private void OnEnable()
        {
            Inventory.OnUpdateInventory += UpdateColor;
        }

        private void OnDisable()
        {
            Inventory.OnUpdateInventory -= UpdateColor;
        }

        public void Init(InventorySaveDataBase data)
        {
            _condition = data;
            _icon.sprite = InventoryIconProvider.Get(data);
            _conditionText.text = data.Amount.ToString();

            UpdateColor(data);
        }

        private void UpdateColor(InventorySaveDataBase data)
        {
            if (data is HarvestableSaveData harvestable)
                _conditionText.color = Inventory.GetResourceAmount(harvestable.ResourceType) >= _condition.Amount ? Color.green : Color.white;
            else if (data is CraftedResourceSaveData crafted)
                _conditionText.color = Inventory.GetResourceAmount(crafted.ResourceType) >= _condition.Amount ? Color.green : Color.white;
        }
    }
}
