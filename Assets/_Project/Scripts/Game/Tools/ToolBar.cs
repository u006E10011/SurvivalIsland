using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using YTools;

namespace Ryadevn
{
    public class ToolBar : MonoBehaviour
    {
        [SerializeField] private AudioClip _selectSound;
        [SerializeField] private ToolBarItem _toolBarItem;
        [SerializeField] private HorizontalLayoutGroup _container;
        [SerializeField] private List<ToolBarIData> _tools = new();

        public Tool CurrentTool { get; private set; }

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            foreach (var item in _tools)
            {
                item.Item = Instantiate(_toolBarItem, _container.transform);
                item.Item.Init(item, this);
            }
        }

        public void Select(Tool tool)
        {
            _tools.ForEach(x =>
            {
                var isSelected = x.Tool == tool;
                x.Tool.gameObject.SetActive(isSelected);
                x.Item.Outline(isSelected);

                if(isSelected)
                    CurrentTool = tool;
            });

            AudioController.Get().Play(_selectSound);
        }
    }
}
