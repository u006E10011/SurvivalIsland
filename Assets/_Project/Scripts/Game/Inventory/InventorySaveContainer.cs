using System;
using System.Collections.Generic;

namespace Ryadevn
{
    [Serializable]
    public class InventorySaveContainer
    {
        public List<HarvestableSaveData> HarvestableResources = new();
        public List<CraftedResourceSaveData> CraftedResources = new();

        public List<InventorySaveDataBase> GetAllItems()
        {
            var result = new List<InventorySaveDataBase>();
            result.AddRange(HarvestableResources);
            result.AddRange(CraftedResources);
            return result;
        }

        public void AddResource(InventorySaveDataBase resource)
        {
            switch (resource)
            {
                case HarvestableSaveData h:
                    AddOrUpdate(HarvestableResources, h);
                    break;
                case CraftedResourceSaveData c:
                    AddOrUpdate(CraftedResources, c);
                    break;
            }
        }

        private void AddOrUpdate<T>(List<T> list, T newResource) where T : InventorySaveDataBase
        {
            var existing = list.Find(x => x.IsSameResource(newResource));

            if (existing != null)
                existing.Amount += newResource.Amount;
            else
                list.Add(newResource);
        }

        public void RemoveResource(InventorySaveDataBase resource)
        {
            switch (resource)
            {
                case HarvestableSaveData h:
                    RemoveFromList(HarvestableResources, h);
                    break;
                case CraftedResourceSaveData c:
                    RemoveFromList(CraftedResources, c);
                    break;
            }
        }

        private void RemoveFromList<T>(List<T> list, T resource) where T : InventorySaveDataBase
        {
            var existing = list.Find(x => x.IsSameResource(resource));

            if (existing != null)
            {
                existing.Amount -= resource.Amount;

                if (existing.Amount <= 0)
                    list.Remove(existing);
            }
        }
    }
}