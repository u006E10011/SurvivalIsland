using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ryadevn
{
    public class DisplayEquipment : MonoBehaviour
    {
        [SerializeField] private Image _harvestableResourceIcon;
        [SerializeField] private Image _craftedResourcesIcon;
        [SerializeField] private Image _progressBar;
        [SerializeField] private TMP_Text _progressText;

        private EquipmentTask _task;
        private CraftRecipe _recipe;

        public void Init(EquipmentTask task, CraftRecipe recipe)
        {
            _task = task;
            _recipe = recipe;
            _craftedResourcesIcon.sprite = InventoryIconProvider.Get(task.CraftedResource);
            _harvestableResourceIcon.sprite = InventoryIconProvider.Get(task.HarvestableResource);
        }

        public void Report(float progress)
        {
            _progressText.text = $"{_task.Count * _recipe.CraftPrice} -> {_task.Count * _recipe.CraftedAmount}";
            _progressBar.fillAmount = 1 - (progress / _recipe.CraftedRate);
        }

        public void Complete()
        {
            _progressText.text = "-  -  -";
            _progressBar.fillAmount = 0;
        }
    }
}
