using UnityEngine;

using static YTools.ColorType;

namespace YTools
{
    public static class ColorText
    {
        public static string Color(this object text, ColorType color) => color switch
        {
            White => $"<color=white>{text}</color>",
            Red => $"<color=red>{text}</color>",
            Green => $"<color=green>{text}</color>",
            Blue => $"<color=blue>{text}</color>",
            Yellow => $"<color=yellow>{text}</color>",
            Black => $"<color=black>{text}</color>",
            Cyan => $"<color=cyan>{text}</color>",
            Gray => $"<color=gray>{text}</color>",
            Clear => $"<color=clear>{text}</color>",
            Magenta => $"<color=magenta>{text}</color>",
            _ => $"<color=white>{text}</color>"
        };

        public static string Color(this string text, Color color)
        {
            return $"<color={ColorUtility.ToHtmlStringRGB(color)}>{text}</color>";
        }

        public static string Color(this string text, Color32 color)
        {
            return $"<color={ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>";
        }
    }
}