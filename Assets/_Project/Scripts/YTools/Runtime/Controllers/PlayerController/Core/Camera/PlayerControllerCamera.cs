using UnityEngine;

namespace YTools
{
    public class PlayerControllerCamera
    {
        public PlayerControllerCamera(Transform owner, Transform camera, Transform model, PlayerControllerData data)
        {
            _data = data;

            Camera = data.CameraType switch
            {
                CameraType.FPS => new FPSCamera(owner, camera, data),
                CameraType.TPS => new TPSCamera(owner, camera, model, data),
                _ => new FPSCamera(owner, camera, data)
            };

            Init();
        }

        public ICameraRotation Camera;

        private readonly PlayerControllerData _data;

        public void SetSensitivity(float value)
        {
            Camera.Sensitivity = PlatformController.Instance.Type switch
            {
                PlatformType.PC => value,
                PlatformType.Mobile => value / _data.SensivityRatio,
                _ => default
            };

            _data.Sensitivity = value;
        }

        private void Init()
        {
            if (_data.Sensitivity != 0)
            {
                Camera.Sensitivity = PlatformController.Instance.Type switch
                {
                    PlatformType.PC => _data.Sensitivity,
                    PlatformType.Mobile => _data.Sensitivity / _data.SensivityRatio,
                    _ => default
                };
            }
            else
                DefaultSensitivity();
        }

        private void DefaultSensitivity() => Camera.Sensitivity = PlatformController.Instance.Type switch
        {
            PlatformType.PC => _data.Sensitivity,
            PlatformType.Mobile => _data.Sensitivity / _data.SensivityRatio,
            _ => default
        };
    }
}