using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using YTools;

namespace Ryadevn
{
    public class ToolBar : MonoBehaviour
    {
        [SerializeField] private float _sensivity = 30f;

        [SerializeField] private AudioClip _selectSound;
        [SerializeField] private ToolBarItem _toolBarItem;
        [SerializeField] private HorizontalLayoutGroup _container;
        [SerializeField] private List<ToolBarIData> _tools = new();

        public Tool CurrentTool { get; private set; }

        private void Start()
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

            Select(_tools[0].Tool);
        }

        private void Update() => InputPC();

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


        private float _scrollAccumulator = 0f;

        private void InputPC()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                Select(_tools[0].Tool);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                Select(_tools[1].Tool);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                Select(_tools[2].Tool);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                Select(_tools[3].Tool);

            float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

            if (Mathf.Abs(scrollDelta) > 0.01f)
            {
                _scrollAccumulator += scrollDelta;

                if (Mathf.Abs(_scrollAccumulator) >= 1f / _sensivity)
                {
                    int currentIndex = -1;

                    for (int i = 0; i < _tools.Count; i++)
                    {
                        if (_tools[i].Tool == CurrentTool)
                        {
                            currentIndex = i;
                            break;
                        }
                    }

                    if (currentIndex != -1)
                    {
                        int steps = Mathf.FloorToInt(Mathf.Abs(_scrollAccumulator) * _sensivity);
                        int direction = _scrollAccumulator > 0 ? 1 : -1;

                        int newIndex = currentIndex;

                        for (int i = 0; i < steps; i++)
                            newIndex = (newIndex + direction + _tools.Count) % _tools.Count;

                        Select(_tools[newIndex].Tool);
                        _scrollAccumulator -= direction * steps / _sensivity;
                    }
                }
            }
            else
            {
                _scrollAccumulator *= 0.95f;
                if (Mathf.Abs(_scrollAccumulator) < 0.01f)
                    _scrollAccumulator = 0f;
            }
        }
    }
}
