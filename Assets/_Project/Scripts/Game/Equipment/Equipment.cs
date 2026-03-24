using Gaskellgames;
using System.Collections;
using UnityEngine;
using YG;
using YTools;

namespace Ryadevn
{
    public class Equipment : MonoBehaviour, IInteractable
    {
        [SerializeField] private CraftedResourceType _resourceType;
        [SerializeField] private DisplayEquipment _display;

        private EquipmentTask _task;
        private CraftRecipe _recipe;
        private Coroutine _coroutine;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _recipe = Resources.Load<EquipmentData>("Data/" + nameof(EquipmentData)).Recipes.Find(x => x.CraftType == _resourceType);

            if (_recipe == null)
            {
                Message.Send($"Recipe is null: {_resourceType.Color(ColorType.Cyan)}".Color(ColorType.White));
                return;
            }

            _task = YG2.saves.EquipmentTask.Find(x => x.CraftedResource.ResourceType == _resourceType) ?? new()
            {
                CraftedResource = new(_resourceType, 0),
                HarvestableResource = new(_recipe.HarvestableType, 0)
            };

            _display.Init(_task, _recipe);
            StartCoroutine(UpdateProgressTask());
        }

        [Button]
        public void Interact()
        {
            var currentHarvestableAmount = new HarvestableSaveData(_recipe.HarvestableType, Inventory.GetResourceAmount(_recipe.HarvestableType));
            var craftedCount = 0;

            if (_recipe.CraftPrice > currentHarvestableAmount.Amount)
                return;

            if (currentHarvestableAmount.Amount == _recipe.CraftPrice)
                craftedCount = 1;
            else
                craftedCount = (int)Mathf.Round((float)currentHarvestableAmount.Amount / 2) / _recipe.CraftPrice;


            currentHarvestableAmount.Amount = craftedCount * _recipe.CraftPrice;
            Inventory.OnRemove?.Invoke(currentHarvestableAmount);

            _task.Count += craftedCount;

            if (_coroutine == null)
                StartCoroutine(UpdateProgressTask());

            Save();
        }

        private IEnumerator UpdateProgressTask()
        {
            var craftedAmount = new CraftedResourceSaveData(_resourceType, _recipe.CraftedAmount);
            var time = _recipe.CraftedRate;

            while (_task.Count > 0)
            {
                time -= Time.deltaTime;
                _display.Report(time);

                if (time <= 0)
                {
                    time = _recipe.CraftedRate;
                    _task.Count--;

                    Inventory.OnAdd?.Invoke(craftedAmount);
                }

                yield return null;
            }

            Complete();
        }

        private void Complete()
        {
            _coroutine = null;
            _display.Complete();

            var saveData = YG2.saves.EquipmentTask.Find(x => x.CraftedResource.ResourceType == _resourceType);

            if (saveData != null)
                YG2.saves.EquipmentTask.Remove(saveData);

            YG2.SaveProgress();
        }

        private void Save()
        {
            var saveData = YG2.saves.EquipmentTask.Find(x => x.CraftedResource.ResourceType == _resourceType);

            if (saveData != null)
                saveData.Count = _task.Count;
            else
                YG2.saves.EquipmentTask.Add(_task);

            YG2.SaveProgress();
        }

    }
}
