using UnityEngine;

namespace Ryadevn
{
    [System.Serializable]
    public class ToolBarIData
    {
        public Sprite Icon;
        public Tool Tool;
        public ToolBarItem Item { get; set; }
    }
}