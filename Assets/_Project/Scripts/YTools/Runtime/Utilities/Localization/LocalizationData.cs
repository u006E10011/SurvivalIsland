using Gaskellgames;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace YTools
{
    [CreateAssetMenu(fileName = nameof(LocalizationData), menuName = "YTools/" + nameof(LocalizationData))]
    public class LocalizationData : ScriptableObject
    {
        [Title("Main")]
        public LanguageType Type = LanguageType.Auto;
        public TMPro.TMP_FontAsset Font;

        [Title("Data")]
        public TextAsset JsonData;
        public List<LocalizationText> Data = new();

        [Title("Export")]
        public string JsonPath;
        public string JsonName;

        private void Reset()
        {
            JsonPath = Path.GetDirectoryName(UnityEditor.AssetDatabase.GetAssetPath(this));
        }

        public Dictionary<string, LocalizationText> GetTranslateAsDictionary()
        {
            var dictionary = new Dictionary<string, LocalizationText>();
            foreach (var item in Data)
                dictionary[item.Key.ToLower()] = item;

            if (JsonData != null)
            {
                //string wrappedJson = "{\"items\":" + JsonData.text + "}";
                var wrapper = JsonUtility.FromJson<SerializationWrapper>(JsonData.text);

                if (wrapper != null && wrapper.Data != null)
                {
                    foreach (var item in wrapper.Data)
                        dictionary[item.Key.ToLower()] = item;
                }
            }

            return dictionary;
        }

        public LocalizationText DefaultValue() => new()
        {
            Key = "default",
            EN = "<color=red>Data is null</color>",
            RU = "<color=red>Data is null</color>"
        };

#if UNITY_EDITOR
        [Button]
        public void ExportToJson()
        {
            string jsonFileName = $"{JsonName}.json";
            string jsonFilePath = Path.Combine(JsonPath, jsonFileName);

            string jsonContent = JsonUtility.ToJson(new SerializationWrapper { Data = this.Data }, true);

            File.WriteAllText(jsonFilePath, jsonContent);
            UnityEditor.AssetDatabase.Refresh();

            Debug.Log($"Data exported to: {jsonFilePath}");
        }

        [System.Serializable]
        private class SerializationWrapper
        {
            public List<LocalizationText> Data;
        }
#endif
    }
}