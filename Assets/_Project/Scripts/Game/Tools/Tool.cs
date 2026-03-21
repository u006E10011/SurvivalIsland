using UnityEngine;
using YTools;

namespace Ryadevn
{
    public class Tool : MonoBehaviour
    {
        [SerializeField] private ToolsType _type;
        [SerializeField] private HarvestableObjectType _targetObject;

        [SerializeField, Space(10)] private AudioClip _clip;
        [SerializeField] private Animator _animator;

        public ToolsType Type => _type;
        public HarvestableObjectType TargetType => _targetObject;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        public void Attack()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _animator.SetTrigger("attack");
                AudioController.Get().Play(_clip);
            }
        }
    }
}
