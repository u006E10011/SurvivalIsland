using UnityEngine;

namespace YTools
{
    public class DirectionCameraForward : IMoveDirection
    {
        public DirectionCameraForward(Transform camera)
        {
            _camera = camera;
        }

        private readonly Transform _camera;

        public Vector3 Value()
        {
            var vector = _camera.forward * InputPlayer.Instance.MoveDirection.y + _camera.right * InputPlayer.Instance.MoveDirection.x;
            return new(vector.x, 0, vector.z);
        }
    }
}