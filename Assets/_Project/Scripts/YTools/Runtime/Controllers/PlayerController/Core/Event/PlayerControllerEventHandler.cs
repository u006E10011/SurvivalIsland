using static YTools.InputPlayer;
using static YTools.PlayerControllerEvent;

namespace YTools
{
    public class PlayerControllerEventHandler
    {
       public PlayerControllerEventHandler(PlayerControllerPhysics physics, PlayerControllerData data)
        {
            _physics = physics;
            _data = data;
        }

        private readonly PlayerControllerData _data;
        private readonly PlayerControllerPhysics _physics;

        public void Update()
        {
            Move();
            IsJump();
            IsFly();
        }

        private void Move()
        {
            var isWalk = Instance.MoveDirection.x == 0 && Instance.MoveDirection.y == 0;

            if (!isWalk && _physics.IsGround)
                (_data.IsEnableAcceleration && Instance.IsAcceleration ? OnRunning : OnWalk)?.Invoke();
            else if (isWalk && _physics.IsGround)
                OnIdle?.Invoke();
        }

        private void IsJump()
        {
            if (_physics.IsGround && Instance.IsJump)
                OnJumpUp?.Invoke();
        }

        private void IsFly()
        {
            if (!_physics.IsGround)
                OnFly?.Invoke();
        }
    }
}