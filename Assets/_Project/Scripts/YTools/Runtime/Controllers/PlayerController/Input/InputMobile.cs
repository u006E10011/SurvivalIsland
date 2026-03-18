using UnityEngine;

namespace YTools
{
    public class InputMobile : IInputPlayer
    {
        public InputMobile(JoystickHandler joystick, RightInputHandler rightInputHandler, SelectButton jump)
        {
            _joystick = joystick;
            _jump = jump;
            _rightInputHandler = rightInputHandler;
        }

        #region Propertis
        public bool IsJump { get; set; }
        public bool IsAcceleration { get; set; }

        public Vector2 MoveDirection { get; set; }
        public Vector2 MouseDirection { get; set; }
        #endregion

        #region Private
        private readonly JoystickHandler _joystick;
        private readonly RightInputHandler _rightInputHandler;
        private readonly SelectButton _jump;
        #endregion

        public void Update()
        {
            MoveDirection = _joystick.Input;
            MouseDirection = _rightInputHandler.MouseDirection;
            IsJump = _jump.IsPressed;
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