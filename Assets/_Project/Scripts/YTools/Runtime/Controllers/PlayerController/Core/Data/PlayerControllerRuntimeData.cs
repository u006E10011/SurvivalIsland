namespace YTools
{
    public class PlayerControllerRuntimeData : PlayerControllerData
    {
        public float BoostSpeed;

        public PlayerControllerRuntimeData(PlayerControllerData data)
        {
            CameraType = data.CameraType;
            SpeedRotationModel = data.SpeedRotationModel;
            Speed = data.Speed;
            MinSpeed = data.MinSpeed;
            Acceleration = data.Acceleration;
            JumpHeight = data.JumpHeight;
            IsEnableAcceleration = data.IsEnableAcceleration;
            PressingJump = data.PressingJump;
            Sensitivity = data.Sensitivity;
            SensivityRatio = data.SensivityRatio;
            RotateMinMax = new Data.MinMax(data.RotateMinMax.Min, data.RotateMinMax.Max);
            Ground = data.Ground;
            Attachable = data.Attachable;
            VisibleGizmos = data.VisibleGizmos;
            Radius = data.Radius;
            Distance = data.Distance;
            Gravity = data.Gravity;
        }
    }
}