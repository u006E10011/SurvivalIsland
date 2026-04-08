using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YTools;

namespace Ryadevn
{
    internal class MapUnlockConditionDisplay : MonoBehaviour
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

        public void Update()
        {
            LookAt();
        }

        private void LookAt()
        {
            const float offset = 180f;

            if (ServiceLocator.Instance.TryGet<PlayerController>(out var player))
            {
                transform.LookAt(player.transform);
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + offset, 0);
            }
        }
    }
}
