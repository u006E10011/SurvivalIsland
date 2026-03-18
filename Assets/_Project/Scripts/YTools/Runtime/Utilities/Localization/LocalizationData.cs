using System.Collections.Generic;
using UnityEngine;

namespace YTools
{
    [CreateAssetMenu(fileName = nameof(LocalizationData), menuName = "YTools/" + nameof(LocalizationData))]
    public class LocalizationData : ScriptableObject
    {
        public LanguageType Type = LanguageType.Auto;
        public TMPro.TMP_FontAsset Font;

        public TextAsset JsonData;
        public List<LocalizationText> Data = new();

        public Dictionary<string, LocalizationText> GetTranslateAsDictionary()
        {
            var dictionary = new Dictionary<string, LocalizationText>();

            foreach (var item in Data)
                dictionary[item.Key.ToLower()] = item;

            return dictionary;
        }

        public LocalizationText DefaultValue() => new()
        {
            Key = "default",
            EN = "<color=red>Data is null</color>",
            RU = "<color=red>Data is null</color>"
        };
    }
}