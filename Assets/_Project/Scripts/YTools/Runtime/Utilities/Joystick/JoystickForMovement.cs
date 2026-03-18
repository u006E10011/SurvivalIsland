using UnityEngine;

namespace YTools
{
    public class JoystickForMovement : JoystickHandler
    {
        public Vector2 Direcrion
        {
            get => new(Input.x, Input.y);
        }
    }
}