using Gaskellgames;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YTools;

namespace Ryadevn
{
    internal class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private PlayerController _player;
        [SerializeField] private List<Map> _maps = new();

        private void OnValidate()
        {
            if (_maps.Count == 0)
                _maps = FindObjectsByType<Map>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        }

        private void Awake()
        {
            _inventory.Init();
            _maps.ForEach(x => x.Init());
            _inventory.Preview.UpdatePreview();

            ServiceLocator.Instance.Register(_player);
        }

        private void OnDestroy()
        {
            ServiceLocator.Instance.Clear();
        }

        [Button]
        public void FindAllMaps()
        {
            _maps = FindObjectsByType<Map>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        }
    }
}
