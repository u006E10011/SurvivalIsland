using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ryadevn
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _amount;

        public InventorySaveDataBase Data { get; private set; }

        public void Init(InventorySaveDataBase data)
        {
            Data = data;

            _icon.sprite = InventoryIconProvider.Get(data);
            _amount.text = data.Amount.ToString();
        }

        public void UpdateAmount(InventorySaveDataBase data)
        {
            _amount.text = data.Amount.ToString();
        }
    }
}
