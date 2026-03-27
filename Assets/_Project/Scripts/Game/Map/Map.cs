using UnityEngine;

namespace Ryadevn
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private MapData _data;
        [SerializeField] private MapUnlocker _mapUnlocker;

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
    }
}
