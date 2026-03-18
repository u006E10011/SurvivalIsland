namespace YTools
{
    [System.Flags]
    public enum DebugType : byte
    {
        Default = 1 << 1,
        Warning = 1 << 2,
        Error = 1 << 3,
        EditorOnly = 1 << 4,
        All = (Default | Warning | Error | EditorOnly) ^ EditorOnly
    }
}