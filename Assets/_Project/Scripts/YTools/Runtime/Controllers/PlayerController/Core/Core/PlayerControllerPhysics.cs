using UnityEngine;

namespace YTools
{
    [System.Serializable]
    public class PlayerControllerPhysics
    {
        public PlayerControllerPhysics(PlayerControllerData config, Transform owner)
        {
            _data = config;
            _owner = owner;
        }

        public bool IsGround { get; private set; }
        public RaycastHit Hit => _hit;
        public Vector3 Velocity;
        
        private bool _isNotGround;
        private PlayerControllerData _data;
        private RaycastHit _hit;
        private Transform _owner;

        public void MathVelocity()
        {
            CheckGround();

            if (IsGround && Velocity.y < 0)
                Velocity.y = _data.Gravity;

            Velocity.y += Time.deltaTime * _data.Gravity;

            if (IsGround)
                Velocity.y = 0;
        }

        private void CheckGround()
        {
            _isNotGround = IsGround == false && Velocity.y < 0;
            IsGround = Physics.SphereCast(_owner.position, _data.Radius, Vector3.down, out _hit, _data.Distance, _data.Ground);

            if (_isNotGround && IsGround)
                PlayerControllerEvent.OnJumpDown?.Invoke();
        }

        public void SetParent()
        {
            if (IsGround)
            {
                if (((1 << _hit.collider.gameObject.layer) & _data.Attachable) != 0)
                    _owner.parent = _hit.collider.transform;
            }
            else
                _owner.parent = null;
        }
    }
}