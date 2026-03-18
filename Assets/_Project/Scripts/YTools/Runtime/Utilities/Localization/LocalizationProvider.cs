using System.Collections.Generic;
using UnityEngine;

namespace YTools
{
    public static class LocalizationProvider
    {
        static LocalizationProvider()
        {
            Data = Data == null ? Resources.Load<LocalizationData>($"{nameof(YTools)}/{nameof(LocalizationData)}") ?? ScriptableObject.CreateInstance<LocalizationData>() : Data;
            _localization = Data.GetTranslateAsDictionary();
        }

        public static LanguageType Type
        {
            get
            {
                if (Data.Type == LanguageType.Auto)
                    return LanguageDetector.Value == "ru" ? LanguageType.RU : LanguageType.EN;

                return Data.Type;
            }
        }

        public static LocalizationData Data { get; private set; }

        private static readonly Dictionary<string, LocalizationText> _localization = new();

        public static string Get(string key, string end = "")
        {
            LocalizationText data = _localization.ContainsKey(key.ToLower()) ? _localization[key.ToLower()] : Data.DefaultValue();
            return (Type == LanguageType.RU ? data.RU : data.EN) + end;
        }

        public static LocalizationText GetLocalizationText(string key)
        {
            return _localization.ContainsKey(key.ToLower()) ? _localization[key.ToLower()] : Data.DefaultValue();
        }

        public static string GetTextByLanguage(LocalizationText localization)
        {
            return Type == LanguageType.RU ? localization.RU : localization.EN;
        }
    }
}