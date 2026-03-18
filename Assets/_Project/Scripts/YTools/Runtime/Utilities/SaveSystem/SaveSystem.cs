using UnityEngine;
using System.IO;

namespace YTools
{
    public class SaveSystem
    {
        private static string savePath
        {
            get =>
            //#if !UNITY_EDITOR && !UNITY_WEBGL
            //Path.Combine(Application.persistentDataPath, "save.json");
            Path.Combine(Application.dataPath, "/Assets/Project/Saves/save.json");
//#endif
        }

        private static SaveData _data;

        public static SaveData Data
        {
            get
            {
                if (_data == null)
                    Load();

                return _data;
            }
            private set => _data = value;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            Load();
        }

        public static void Save()
        {
            try
            {
                var json = JsonUtility.ToJson(Data, true);
                File.WriteAllText(savePath, json);
                Message.Send($"Game saved successfully to: {savePath}");
            }
            catch (System.Exception e)
            {
                Message.Send($"Save failed: {e.Message}", DebugType.Error);
            }
        }

        public static void Load()
        {
            try
            {
                if (File.Exists(savePath))
                {
                    var json = File.ReadAllText(savePath);
                    _data = JsonUtility.FromJson<SaveData>(json);
                    Message.Send("Game loaded successfully".Color(ColorType.Green));
                }
                else
                {
                    _data = new SaveData();
                    Save();
                    Message.Send("No save file found. Creating new data");
                }
            }
            catch (System.Exception e)
            {
                Message.Send($"Load failed: {e.Message}", DebugType.Error);
                _data = new SaveData();
            }
        }

        public static void DeleteSave()
        {
            try
            {
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                    _data = new SaveData();
                    Message.Send("Save file deleted successfully".Color(ColorType.Green));
                }
            }
            catch (System.Exception e)
            {
                Message.Send($"Delete save failed: {e.Message}", DebugType.Error);
            }
        }

        public static bool SaveExists()
        {
            return File.Exists(savePath);
        }
    }
}