using UnityEngine;

namespace YTools
{
    [CreateAssetMenu(fileName = nameof(YToolsData), menuName = "YTools/" + nameof(YToolsData))]
    public class YToolsData : ScriptableObject
    {
        public int TargetFrame = 60;
        public DebugType DebugType = DebugType.EditorOnly;
        public bool VisibleCursor;
        public bool SaveSystem = false;

        [Header("Saves")]
        public string SavePath = "Assets/Project/Saves/SaveData.json";
    }
}