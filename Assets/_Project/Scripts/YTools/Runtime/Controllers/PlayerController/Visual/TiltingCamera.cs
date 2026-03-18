using UnityEngine;

namespace YTools
{
    public class TiltingCamera : MonoBehaviour
    {
        [SerializeField] private float _smoothing;
        [SerializeField] private Data.MinMax _minMax = new(-5, 5);

        private float value;
        private Vector3 _eulerAngles;

        private void LateUpdate()
        {
            value += Time.deltaTime;
            _eulerAngles = transform.localEulerAngles;

            if (InputPlayer.Instance.MoveDirection.x > 0)
                value -= _smoothing * Time.deltaTime;
            else if (InputPlayer.Instance.MoveDirection.x < 0)
                value += _smoothing * Time.deltaTime;
            else
            {
                if (value > 0)
                {
                    value -= _smoothing * Time.deltaTime;

                    if (value <= 0)
                        value = 0;
                }
                else
                {
                    value += _smoothing * Time.deltaTime;

                    if (value >= 0)
                        value = 0;
                }
            }

            value = Mathf.Clamp(value, _minMax.Min, _minMax.Max);

            _eulerAngles.z = value;
            transform.localEulerAngles = _eulerAngles;
        }
    }
}