namespace YTools
{
    public static class LanguageDetector
    {
        public static string Value
        {
            get
            {
#if PLUGIN_YG_2
                return YG.YG2.envir.language;
#else
                return "en";
#endif
            }
        }


    }
}