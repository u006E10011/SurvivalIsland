using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YTools
{
    public class JoystickHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Color32 _activeJoystickColor;
        [SerializeField] private Color32 _inActiveJoystickColor;

        [SerializeField, Space(10)] private Canvas _canvas;
        [SerializeField] private Image _joystickArea;
        [SerializeField] private Image _joystickBackground;
        [SerializeField] private Image _joystick;
        public Vector2 Input { get; protected set; }

        private Vector2 _joystickBackgroundStartPosition;
        private bool _joystickIsActive;

        private void Start()
        {
            ClickEffect();
            _joystickBackgroundStartPosition = _joystickBackground.rectTransform.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground.rectTransform, eventData.position, null, out var position))
            {
                Input = new()
                {
                    x = position.x * 2 / _joystickBackground.rectTransform.sizeDelta.x,
                    y = position.y * 2 / _joystickBackground.rectTransform.sizeDelta.y,
                };

                Input = Input.magnitude > 1f ? Input.normalized : Input;
                _joystick.rectTransform.anchoredPosition = new()
                {
                    x = Input.x * (_joystickBackground.rectTransform.sizeDelta.x / 2),
                    y = Input.y * (_joystickBackground.rectTransform.sizeDelta.y / 2)
                };
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ClickEffect();

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _joystickArea.rectTransform,
                eventData.position,
                _canvas.worldCamera,
                out Vector2 localPoint);

            _joystickBackground.rectTransform.anchoredPosition = new(
                Mathf.Clamp(localPoint.x, -_joystickArea.rectTransform.sizeDelta.x / 2, _joystickArea.rectTransform.sizeDelta.x / 2),
                Mathf.Clamp(localPoint.y, -_joystickArea.rectTransform.sizeDelta.y / 2, _joystickArea.rectTransform.sizeDelta.y / 2)
            );
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ClickEffect();

            _joystickBackground.rectTransform.anchoredPosition = _joystickBackgroundStartPosition;
            Input = Vector2.zero;
            _joystick.rectTransform.anchoredPosition = Vector2.zero;
        }

        private void ClickEffect()
        {
            if (!_joystickIsActive)
            {
                _joystick.color = _inActiveJoystickColor;
                _joystickIsActive = true;
            }
            else
            {
                _joystick.color = _activeJoystickColor;
                _joystickIsActive = false;
            }
        }
    }

}