using UnityEngine;

namespace YTools
{
    [CreateAssetMenu(fileName = nameof(PlayerControllerData), menuName = "YTools/PlayerController/" + nameof(PlayerControllerData))]
    public class PlayerControllerData : ScriptableObject
    {
        [Tooltip("FPS - Камера от первого лица\n\n"
               + "TPS - Камера от 3 лица\n")]
        public CameraType CameraType;
        public float SpeedRotationModel = 10;

        public float Speed = 15;
        public float MinSpeed = 5;
        public float Acceleration = 25;
        public float JumpHeight = 2;

        public bool IsEnableAcceleration = true;
        public bool PressingJump = true;

        public float Sensitivity = 120;
        public float SensivityRatio = 24;
        public Data.MinMax RotateMinMax = new(-90, 90);

        public LayerMask Ground;
        public LayerMask Attachable;
        public bool VisibleGizmos;
        public float Radius = 0.3f;
        public float Distance = 0.9f;
        public float Gravity = -40f;
    }
}