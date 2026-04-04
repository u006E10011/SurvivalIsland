using System.Collections.Generic;
using UnityEngine;

namespace Ryadevn
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private MapData _data;
        [SerializeField] private MapUnlocker _mapUnlocker;

        [field: SerializeField] public GameObject MapGround { get; private set; }

        private void OnValidate()
        {
            MapUtils.Register(_mapUnlocker, this);
        }

        private void Awake()
        {
            _mapUnlocker.Init(this, _data);

            if (_data != null)
            {
                var isUnlock = _data.IsUnloced;
                transform.localScale = isUnlock ? Vector3.one : Vector3.zero;
                gameObject.SetActive(isUnlock);
            }
        }

        private void OnDrawGizmosSelected()
        {
            MapUtils.DrawGizmos(_mapUnlocker);
        }
    }

    public static class MapUtils
    {
        private readonly static Dictionary<MapUnlocker, Map> _maps = new();

        public static void Register(MapUnlocker mapUnlocker, Map map)
        {
            if (mapUnlocker && map && _maps.ContainsKey(mapUnlocker))
                return;

            _maps[mapUnlocker] = map;
        }

        public static void Unregister(MapUnlocker mapUnlocker)
        {
            if(_maps.ContainsKey(mapUnlocker))
                _maps.Remove(mapUnlocker);
        }

        public static void DrawGizmos(MapUnlocker unlocker)
        {
            if(_maps.TryGetValue(unlocker, out var map))
            {
                var collider = map.MapGround.GetComponent<BoxCollider>();
                Gizmos.DrawWireCube(map.transform.position, collider.size);
                Gizmos.DrawLine(map.transform.position, unlocker.transform.position);
                Gizmos.DrawSphere(unlocker.transform.position, .3f);
            }
        }
    }
}
