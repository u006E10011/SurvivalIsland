using UnityEngine;

using static YTools.InputPlayer;

namespace YTools
{
    public class TPSCamera : ICameraRotation
    {
        public TPSCamera(Transform owner, Transform camera, Transform model, PlayerControllerData data)
        {
            _camera = camera;
            _owner = owner;
            _model = model;
            _data = data;
        }

        public readonly System.Action OnRotation;

        private float _rotationX;
        private float _rotationY;

        private readonly PlayerControllerData _data;
        private readonly Transform _camera;
        private readonly Transform _owner;
        private readonly Transform _model;

        public float Sensitivity { get; set; }


        public void Rotation()
        {
            if (Instance.MoveDirection != Vector2.zero)
                RotateOwner();

            TPS();
        }

        private void TPS()
        {
            _rotationX -= Instance.MouseDirection.y * Sensitivity * Time.deltaTime;
            _rotationY += Instance.MouseDirection.x * Sensitivity * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, _data.RotateMinMax.Min, _data.RotateMinMax.Max);

            _camera.localRotation = Quaternion.Euler(_rotationX, _rotationY, _camera.localRotation.z);
        }

        private void RotateOwner()
        {
            if (Instance.MoveDirection != Vector2.zero)
            {
                _model.rotation = Quaternion.Slerp(_model.rotation, _camera.rotation, _data.SpeedRotationModel * Time.deltaTime);
                _model.rotation = Quaternion.Euler(0, _model.eulerAngles.y, 0);
                _owner.Rotate((Sensitivity * Instance.MouseDirection.x * Vector3.up) * Time.deltaTime);
            }
        }
    }
}