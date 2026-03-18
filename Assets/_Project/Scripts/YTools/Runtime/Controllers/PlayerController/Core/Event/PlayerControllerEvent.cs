using System;

namespace YTools
{
    public class PlayerControllerEvent
    {
        public static Action OnIdle;
        public static Action OnWalk;
        public static Action OnRunning;
        public static Action OnJumpUp;
        public static Action OnJumpDown;
        public static Action OnFly;
    }
}