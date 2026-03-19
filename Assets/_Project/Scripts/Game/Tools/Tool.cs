using UnityEngine;
using YTools;

namespace Ryadevn
{
    public class Tool : MonoBehaviour
    {
        [SerializeField] ToolsType _type;

        [SerializeField, Space(10)] private AudioClip _clip;
        [SerializeField] private Animator _animator;

        public ToolsType Type => _type;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        public void Update()
        {
            Attack();
        }

        private void Attack()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _animator.SetTrigger("attack");
                AudioController.Get().Play(_clip);
            }
        }
    }
}
