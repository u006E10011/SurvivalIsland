using UnityEngine;

namespace YTools
{
    public class DirectionOwnerForward : IMoveDirection
    {
        public DirectionOwnerForward(Transform owner)
        {
            _owner = owner;
        }

        private readonly Transform _owner;

        public Vector3 Value()
        {
            return _owner.forward * InputPlayer.Instance.MoveDirection.y + _owner.right * InputPlayer.Instance.MoveDirection.x;
        }
    }
}