using UnityEngine;

using static YTools.InputPlayer;

namespace YTools
{
    public class FPSCamera : ICameraRotation
    {
        public FPSCamera(Transform owner, Transform camera, PlayerControllerData data)
        {
            _camera = camera;
            _owner = owner;
            _data = data;
        }

        public float Sensitivity { get; set; }

        private float _rotationX;

        private readonly PlayerControllerData _data;
        private readonly Transform _camera;
        private readonly Transform _owner;

        public void Rotation()
        {
            _rotationX -= Instance.MouseDirection.y * Sensitivity * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, _data.RotateMinMax.Min, _data.RotateMinMax.Max);

            _camera.localRotation = Quaternion.Euler(_rotationX, _camera.localRotation.y, _camera.localRotation.z);
            _owner.Rotate((Sensitivity * Instance.MouseDirection.x * Vector3.up) * Time.deltaTime);
        }
    }
}