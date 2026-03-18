using UnityEngine;

namespace YTools
{
    public class PlatformController : PersistentSingleton<PlatformController>
    {
        [SerializeField] private PlatformType _type = PlatformType.PC;

        public PlatformType Type
        {
            get
            {
#if !UNITY_EDITOR
                _type = Playgama.Bridge.device.type == Playgama.Modules.Device.DeviceType.Mobile? PlatformType.Mobile : PlatformType.PC;
#endif

                return _type;
            }
        }

        public void Set(PlatformType type) => _type = type;
    }
}