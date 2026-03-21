namespace Ryadevn
{
    [System.Flags]
    public enum HarvestableObjectType : byte
    {
        Tree = 1 << 0,
        Stone = 1 << 1,
        Sand = 1 << 2,
        grass = 1 << 3,
        SugarCane = 1 << 4
    }
}
