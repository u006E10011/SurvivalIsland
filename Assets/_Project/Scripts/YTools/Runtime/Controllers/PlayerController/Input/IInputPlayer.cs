using UnityEngine;

namespace YTools
{
    public interface IInputPlayer
    {
        public bool IsJump { get; set; }
        public bool IsAcceleration {  get; set; }

        public Vector2 MoveDirection { get; set; }
        public Vector2 MouseDirection { get; set; }

        public void Update();
        public void Reset();
    }
}