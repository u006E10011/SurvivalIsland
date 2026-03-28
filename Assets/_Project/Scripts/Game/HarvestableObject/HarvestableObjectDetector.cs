using System.Linq;
using UnityEngine;

namespace Ryadevn
{
    public class HarvestableObjectDetector : MonoBehaviour
    {
        [SerializeField] private float _radius = .5f;
        [SerializeField] private float _maxDistance = 1f;
        [SerializeField] private LayerMask _harvestableLayer;

        [SerializeField, Space(10)] private ToolBar _toolBar;
        [SerializeField] private Camera _camera;

        private Collider[] _hitColliders = new Collider[20];

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                _toolBar.CurrentTool.PlayHitAnimation(Detect);
        }

        private void Detect()
        {
            var ray = new Ray(_camera.transform.position, _camera.transform.forward);

            if (!Physics.Raycast(ray, out var hitInfo, _maxDistance, _harvestableLayer, QueryTriggerInteraction.Collide))
                return;

            var hitCount = Physics.OverlapSphereNonAlloc(hitInfo.point, _radius, _hitColliders, _harvestableLayer, QueryTriggerInteraction.Collide);

            if (hitCount == 0)
                return;

            var harvestableObject = _hitColliders
                .Take(hitCount)
                .Where(x => x != null)
                .Select(x => x.GetComponentInParent<HarvestableObject>())
                .Where(x => x != null && (x.Type & _toolBar.CurrentTool.TargetType) == x.Type)
                .OrderByDescending(x => Vector3.Distance(x.transform.position, hitInfo.point))
                .FirstOrDefault();

            harvestableObject?.TakeDamage();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (_camera != null)
            {
                var ray = new Ray(_camera.transform.position, _camera.transform.forward);

                if (Physics.Raycast(ray, out var hitInfo, _maxDistance, _harvestableLayer, QueryTriggerInteraction.Collide))
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireSphere(hitInfo.point, _radius);
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(ray.origin, hitInfo.point);
                }
                else
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(ray.origin + ray.direction * _maxDistance, _radius);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * _maxDistance);
                }

            }
        }
#endif
    }
}
