using System;
using UnityEngine;

namespace Ryadevn
{
    [Serializable]
    public abstract class InventorySaveDataBase
    {
        public abstract Enum Type { get; }
        [SerializeField] private int _amount;

        public int Amount
        {
            get => _amount;
            set => _amount = value;
        }

        protected InventorySaveDataBase(int amount)
        {
            _amount = amount;
        }

        protected InventorySaveDataBase() { }

        public override bool Equals(object obj)
        {
            if (obj is not InventorySaveDataBase other)
                return false;

            return Type.GetType() == other.Type.GetType() &&
                   Convert.ToInt32(Type) == Convert.ToInt32(other.Type);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type.GetType(), Convert.ToInt32(Type));
        }

        public bool IsSameResource(InventorySaveDataBase other)
        {
            if (other == null)
                return false;

            return Type.GetType() == other.Type.GetType() &&
                   Convert.ToInt32(Type) == Convert.ToInt32(other.Type);
        }
    }

    [Serializable]
    public class InventorySaveData<T> : InventorySaveDataBase where T : System.Enum
    {
        public T ResourceType;
        public override Enum Type => ResourceType;

        public InventorySaveData(T type, int amount) : base(amount)
        {
            ResourceType = type;
        }

        public InventorySaveData() { }
    }

    [Serializable]
    public class HarvestableSaveData : InventorySaveData<HarvestableObjectType>
    {
        public HarvestableSaveData(HarvestableObjectType type, int amount) : base(type, amount)
        {
        }

        public HarvestableSaveData() { }
    }

    [Serializable]
    public class CraftedResourceSaveData : InventorySaveData<CraftedResourceType>
    {
        public CraftedResourceSaveData(CraftedResourceType type, int amount) : base(type, amount)
        {
        }

        public CraftedResourceSaveData() { }
    }
}