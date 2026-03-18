using UnityEngine;

namespace YTools
{
    public class PlayerControllerMove
    {
        public PlayerControllerMove(Transform owner, Transform camera, PlayerControllerData data)
        {
            MoveDirection = data.CameraType switch
            {
                CameraType.FPS => new DirectionOwnerForward(owner),
                CameraType.TPS => new DirectionCameraForward(camera),
                _ => new DirectionOwnerForward(owner)
            };
        }

        public IMoveDirection MoveDirection;
    }
}