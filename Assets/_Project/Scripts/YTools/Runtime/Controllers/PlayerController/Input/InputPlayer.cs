using UnityEngine;

namespace YTools
{
    public class InputPlayer : MonoBehaviour
    {
        #region Instance
        private static IInputPlayer _instance;
        public static IInputPlayer Instance
        {
            get => _instance;
            set => _instance = value;
        }
        #endregion

        public static bool IsLockInput { get; set; }

        [SerializeField] private PlayerControllerData _data;

        #region Mobile
        [SerializeField] private Canvas _inputMobile;
        [SerializeField] private JoystickHandler _joystick;
        [SerializeField] private RightInputHandler _rightInputHandler;

        [SerializeField, Space(10)] private SelectButton _jumpButton;
        #endregion

        private void Awake()
        {
            _instance = PlatformController.Instance.Type == PlatformType.PC
                ? new InputPC(_data)
                : new InputMobile(_joystick, _rightInputHandler, _jumpButton);

            if (_inputMobile)
                _inputMobile.gameObject.SetActive(_instance is InputMobile);
        }

        private void Update()
        {
            if (!IsLockInput)
                _instance.Update();
            else
                _instance.Reset();
        }
    }
}