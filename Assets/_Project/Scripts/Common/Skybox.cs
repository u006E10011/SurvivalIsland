using System.Collections;
using UnityEngine;

namespace Ryadevn
{
    public class Skybox : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 10f;

        private void Start()
        {
            StartCoroutine(Animation());
        }

        private IEnumerator Animation()
        {
            while (true)
            {
                RenderSettings.skybox.SetFloat("_Rotation", Time.time * _rotationSpeed);
                yield return null;
            }
        }
    }
}