namespace Ryadevn
{
    [System.Flags]
    public enum HarvestableObjectType : byte
    {
        Oak = 1 << 0,
        Birch = 1 << 1,
        Stone = 1 << 2,
        Sand = 1 << 3,
        grass = 1 << 4,
        SugarCane = 1 << 5
    }
}
