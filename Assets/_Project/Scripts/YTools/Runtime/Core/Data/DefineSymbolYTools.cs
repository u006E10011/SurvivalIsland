#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace YTools
{
    [InitializeOnLoad]
    public class DefineSymbolYTools : AssetModificationProcessor
    {
        private static readonly string[] ExtraScriptingDefineSymbols = new string[] { "YTOOLS" };

        static DefineSymbolYTools()
        {
            AddExtraScriptingDefineSymbols(ExtraScriptingDefineSymbols);
        }

        public static void AddExtraScriptingDefineSymbols(string[] extraScriptingDefineSymbols)
        {
            List<string> scriptingDefineSymbols = GetScriptDefineSymbols();

            int oldCount = scriptingDefineSymbols.Count;
            scriptingDefineSymbols.AddRange(extraScriptingDefineSymbols.Except(scriptingDefineSymbols));

            if (oldCount != scriptingDefineSymbols.Count)
                SetScriptDefineSymbols(scriptingDefineSymbols);
        }

        public static void RemoveExtraScriptingDefineSymbols(string[] extraScriptingDefineSymbols)
        {
            List<string> scriptingDefineSymbols = GetScriptDefineSymbols();

            foreach (string defineSymbol in extraScriptingDefineSymbols)
                scriptingDefineSymbols.Remove(defineSymbol);

            if (scriptingDefineSymbols.Count != scriptingDefineSymbols.Count)
                SetScriptDefineSymbols(scriptingDefineSymbols);
        }

        private static List<string> GetScriptDefineSymbols()
        {
#if UNITY_6000_0_OR_NEWER
            UnityEditor.Build.NamedBuildTarget selectedBuildTarget = UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            string scriptingDefineSymbolsForGroup = PlayerSettings.GetScriptingDefineSymbols(selectedBuildTarget);
#else
            string scriptingDefineSymbolsForGroup = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
#endif
            return scriptingDefineSymbolsForGroup.Split(';').ToList();
        }

        private static void SetScriptDefineSymbols(List<string> scriptingDefineSymbols)
        {
            string newScriptingDefineSymbols = string.Join(";", scriptingDefineSymbols.ToArray());

#if UNITY_6000_0_OR_NEWER
            UnityEditor.Build.NamedBuildTarget selectedBuildTarget = UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            PlayerSettings.SetScriptingDefineSymbols(selectedBuildTarget, newScriptingDefineSymbols);
#else
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, newScriptingDefineSymbols);
#endif
        }

    }
}

#endif