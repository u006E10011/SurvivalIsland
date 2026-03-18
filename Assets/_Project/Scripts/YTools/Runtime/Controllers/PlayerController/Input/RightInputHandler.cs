using UnityEngine;
using UnityEngine.EventSystems;

namespace YTools
{
    public class RightInputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Vector2 MouseDirection { get; private set; }

        private bool _isPressed;
        private int _id;
        private Vector2 _oldPoint;

        private void Update()
        {
            if (_isPressed)
            {
                if (_id >= 0 && _id < Input.touches.Length)
                {
                    MouseDirection = Input.touches[_id].position - _oldPoint;
                    _oldPoint = Input.touches[_id].position;
                }
                else
                {
                    MouseDirection = (Vector2)Input.mousePosition - _oldPoint;
                    _oldPoint = Input.mousePosition;
                }
            }
            else
                MouseDirection = Vector2.zero;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
            _oldPoint = eventData.position;
            _id = eventData.pointerId;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
        }
    }
}