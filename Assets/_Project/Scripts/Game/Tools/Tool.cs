using System.Collections;
using System.Linq;
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

        private System.Action _callback;
        private Coroutine _cooldown;
        private WaitForSeconds _duration;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
            _duration = new(_animator.runtimeAnimatorController.animationClips.ToList().Find(x => x.name == $"{_type.ToString().ToLower()}_hit").length);
        }

        public void PlayHitAnimation(System.Action callback)
        {
            if (_cooldown != null)
                return;

            _animator.SetTrigger("hit");
            _callback = callback;
            _cooldown = StartCoroutine(Cooldown());
        }

        public void Attack()
        {
            _callback?.Invoke();
            AudioController.Get().Play(_clip);
        }

        private IEnumerator Cooldown()
        {
            yield return _duration;
            _callback = null;
            _cooldown = null;
        }

        private void OnDisable()
        {
            if (_cooldown != null)
                StopCoroutine(_cooldown);

            _callback = null;
            _cooldown = null;
        }
    }
}
