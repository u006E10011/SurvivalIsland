using UnityEngine;
using UnityEngine.UI;

namespace Ryadevn
{
    public interface IInteractable
    {
        void Interact();
    }

    public class Interactable : MonoBehaviour
    {
        [SerializeField] private float _distance = 5f;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private Button _button;
        [SerializeField] private Camera _camera;

        private IInteractable _interactable;

        private void OnValidate()
        {
            _camera ??= Camera.main;
        }

        private void OnEnable() => _button.onClick.AddListener(Interact);
        private void OnDisable() => _button.onClick.RemoveListener(Interact);

        private void Update() => DrawRaycast();

        private void DrawRaycast()
        {
            var ray = new Ray(_camera.transform.position, _camera.transform.forward);

            if (Physics.Raycast(ray, out var hitInfo, _distance, _layer))
            {
                if (hitInfo.collider.TryGetComponent<IInteractable>(out _interactable))
                {
                    _button.gameObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                        _interactable.Interact();
                }
            }
            else
            {
                _button.gameObject.SetActive(false);
                _interactable = null;
            }
        }

        private void Interact() => _interactable?.Interact();
    }
}
