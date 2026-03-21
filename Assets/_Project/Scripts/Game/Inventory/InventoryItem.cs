using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ryadevn
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _amount;

        public void Init(InventorySaveData data)
        {
            _icon.sprite = InventoryIconProvider.Get(data.Type);
            _amount.text = data.Amount.ToString();
        }

        public void UpdateAmount(InventorySaveData data)
        {
            _amount.text = data.Amount.ToString();
        }
    }
}
