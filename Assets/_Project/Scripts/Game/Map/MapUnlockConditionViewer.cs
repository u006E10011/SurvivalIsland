using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ryadevn
{
    internal class MapUnlockConditionViewer : MonoBehaviour
    {
        [SerializeField] private MapUnlockConditionItem _item;
        [SerializeField] private VerticalLayoutGroup _container;

        public void Init(List<InventorySaveDataBase> conditions)
        {
            foreach (var item in conditions)
            {
                var view = Instantiate(_item, _container.transform);
                view.Init(item);
            }
        }
    }
}
