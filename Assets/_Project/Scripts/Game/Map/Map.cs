using System.Collections.Generic;
using UnityEngine;

namespace Ryadevn
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private MapData _data;
        [SerializeField] private MapUnlocker _mapUnlocker;

        public void Init()
        {
            if (_data != null && !YG.YG2.saves.Maps.Contains(_data.ID))
            {
                _mapUnlocker.Init(this, _data);
                var isUnlock = _data.IsUnloced;
                transform.localScale = isUnlock ? Vector3.one : Vector3.zero;
                gameObject.SetActive(isUnlock);
            }
        }
    }
}
