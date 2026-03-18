using UnityEngine;

using static YTools.InputPlayer;

namespace YTools
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, IService
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private Transform _model;
        [SerializeField] private PlayerControllerData _dataInstance;

        private PlayerControllerRuntimeData _data;

        public PlayerControllerPhysics Physics { get; private set; }
        public CameraType CameraType => _dataInstance.CameraType;

        private CharacterController _controller;
        private PlayerControllerEventHandler _eventHandler;
        private PlayerControllerCamera _rotationCamera;
        private PlayerControllerMove _direction;

        private void Awake()
        {
            _data = new(_dataInstance);
            Physics = new(_data, transform);
            _eventHandler = new(Physics, _data);
            _rotationCamera = new(transform, _camera, _model, _data);
            _direction = new(transform, _camera, _data);
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Physics.MathVelocity();
            Physics.SetParent();
            _eventHandler.Update();

            Move();
            Jump();

            if (!CursorController.Visible)
                _rotationCamera.Camera.Rotation();
        }

        public void SetSensitivity(float value)
        {
            _rotationCamera.SetSensitivity(value);
        }

        public void SetBoostSpeed(float value)
        {
            _data.BoostSpeed = value;
        }

        private void Move()
        {
            var speed = _data.IsEnableAcceleration && Instance.IsAcceleration ? _data.Acceleration : _data.Speed;
            _controller.Move((speed + _data.BoostSpeed) * Time.deltaTime * _direction.MoveDirection.Value());
        }

        private void Jump()
        {
            if (Physics.IsGround && Instance.IsJump)
                Physics.Velocity.y = Mathf.Sqrt(_data.JumpHeight * -2f * _data.Gravity);

            _controller.Move(Physics.Velocity * Time.deltaTime);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying || !_data.VisibleGizmos)
                return;

            Gizmos.DrawRay(transform.position, Vector3.down * _data.JumpHeight);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, Vector3.down * _data.Distance);
            Gizmos.DrawSphere(transform.position + Vector3.down * _data.Distance, _data.Radius);
        }
#endif
    }
}