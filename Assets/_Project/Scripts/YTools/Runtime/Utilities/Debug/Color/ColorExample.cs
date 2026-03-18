using UnityEngine;

using static YTools.ColorType;

namespace YTools
{
    public static class ColorExample
    {
        private const string TEXT = "Color example";

        public static void Example()
        {
            Write(White);
            Write(Red);
            Write(Green);
            Write(Blue);
            Write(Yellow);
            Write(Black);
            Write(Cyan);
            Write(Gray);
            Write(Clear);
            Write(Magenta);
        }

        private static void Write(ColorType color) => Debug.Log($"{TEXT} {color}".Color(color));
    }
}