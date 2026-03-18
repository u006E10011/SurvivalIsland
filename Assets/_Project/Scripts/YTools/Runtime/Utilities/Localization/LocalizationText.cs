namespace YTools
{
    [System.Serializable]
    public struct LocalizationText
    {
        public LocalizationText(string key, string en, string ru)
        {
            Key = key;
            EN = en;
            RU = ru;
        }

        public string Key;
        public string EN;
        public string RU;
    }
}