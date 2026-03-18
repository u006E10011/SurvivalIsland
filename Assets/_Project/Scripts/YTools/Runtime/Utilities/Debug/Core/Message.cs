using UnityEngine;

namespace YTools
{
    public static class Message
    {
        public static string Send(string message, DebugType type = DebugType.Default, Object context = null)
        {
            if (!Application.isEditor && YToolsDataProvider.Data.DebugType == DebugType.EditorOnly)
                return "DebugType.EditorOnly";

            return MessageType(message, context, type);
        }

        private static string MessageType(string message, Object context, DebugType type)
        {
            if (type.HasFlag(DebugType.EditorOnly) && !Application.isEditor)
                return "DebugType.EditorOnly";

            if (type.HasFlag(DebugType.Default))
                Debug.Log($"<color=white>{message}</color>", context);
            if (type.HasFlag(DebugType.Warning))
                Debug.LogWarning($"<color=yellow>{message}</color>", context);
            else if (type.HasFlag(DebugType.Error))
                Debug.LogError($"<color=red>{message}</color>", context);

            return message;
        }
    }
}