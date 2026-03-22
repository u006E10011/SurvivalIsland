using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ryadevn
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _amount;

        public void Init(InventorySaveDataBase data)
        {
            _icon.sprite = InventoryIconProvider.Get(data);
            _amount.text = data.Amount.ToString();
        }

        public void UpdateAmount(InventorySaveDataBase data)
        {
            _amount.text = data.Amount.ToString();
        }
    }
}
