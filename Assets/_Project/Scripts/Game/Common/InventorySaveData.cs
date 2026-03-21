namespace Ryadevn
{
    [System.Serializable]
    public class InventorySaveData
    {
        public int Amount;
        public HarvestableObjectType Type;

        public InventorySaveData(HarvestableObjectType type, int amount)
        {
            Type = type;
            Amount = amount;
        }
    }
}