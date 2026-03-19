using UnityEngine;
using UnityEngine.UI;

namespace Ryadevn
{
    public class ToolBarItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _outline;
        [SerializeField] private Button _button;

        public void Init(ToolBarIData data, ToolBar toolBar)
        {
            _icon.sprite = data.Icon;
            _button.onClick.AddListener(() => toolBar.Select(data.Tool));
        }

        public void Outline(bool isSelect)
        {
            _outline.gameObject.SetActive(isSelect);    
        }
    }
}
