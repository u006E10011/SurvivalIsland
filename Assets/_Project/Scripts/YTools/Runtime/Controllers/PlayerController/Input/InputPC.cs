using UnityEngine;

using static UnityEngine.Input;

namespace YTools
{
    public class InputPC : IInputPlayer
    {
        public InputPC(PlayerControllerData data)
        {
            _data = data;
            CursorController.IsVisible(YToolsDataProvider.Data.VisibleCursor);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.lockState = CursorLockMode.None;
        }


        #region bool
        public bool IsJump { get; set; }
        public bool IsAcceleration { get; set; }
        #endregion

        #region Vector2
        public Vector2 MoveDirection { get; set; }
        public Vector2 MouseDirection { get; set; }
        #endregion

        private readonly PlayerControllerData _data;

        public void Update()
        {
            MoveDirection = new Vector2(GetAxis("Horizontal"), GetAxis("Vertical"));
            MouseDirection = new Vector2(GetAxis("Mouse X"), GetAxis("Mouse Y"));

            Keyboard();
        }

        private void Keyboard()
        {
            IsAcceleration = GetKey(KeyCode.LeftShift) && (MoveDirection.x != 0 || MoveDirection.y != 0);
            IsJump = _data.PressingJump ? GetKey(KeyCode.Space) : GetKeyDown(KeyCode.Space);

            if (GetKeyDown(KeyCode.Escape) || GetKeyDown(KeyCode.Tab))
                CursorController.Switch();
        }

        public void Reset()
        {
            MoveDirection = new();
            MouseDirection = new();

            IsJump = false;
            IsAcceleration = false;
        }
    }
}

