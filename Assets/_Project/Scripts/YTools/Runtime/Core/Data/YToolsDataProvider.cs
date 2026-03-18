using UnityEngine;

namespace YTools
{
    public class YToolsDataProvider
    {
        private const string PATH = "YTools/" + nameof(YToolsData);

        static YToolsDataProvider()
        {
            Data = Resources.Load<YToolsData>(PATH);

            if (Data == null)
            {
                Message.Send($"Data is null, create {nameof(YToolsData).Color(ColorType.Red)}".Color(ColorType.Cyan), DebugType.Warning);
                Data = (YToolsData)ScriptableObject.CreateInstance(nameof(YToolsData));
            }

            Application.targetFrameRate = Data.TargetFrame;
        }

        public static YToolsData Data { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init() { }
    }
}