using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using YTools;

namespace Ryadevn
{
    public class Tool : MonoBehaviour
    {
        [SerializeField] private ToolsType _type;
        [SerializeField] private HarvestableObjectType _targetObject;

        [SerializeField, Space(10)] private AudioClip _clip;
        [SerializeField] private AudioMixerGroup _mixer;
        [SerializeField] private Animator _animator;

        public ToolsType Type => _type;
        public HarvestableObjectType TargetType => _targetObject;

        private System.Func<bool> _callback;
        private Coroutine _cooldown;
        private WaitForSeconds _duration;
        private Audio _audio;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
            _duration = new(_animator.runtimeAnimatorController.animationClips.ToList().Find(x => x.name == $"{_type.ToString().ToLower()}_hit").length);
        }

        public void Start() => _audio = AudioController.Get();

        public void PlayHitAnimation(System.Func<bool> callback)
        {
            if (_cooldown != null)
                return;

            _animator.SetTrigger("hit");
            _callback = callback;
            _cooldown = StartCoroutine(Cooldown());
        }

        public void Attack()
        {
            if ((bool)_callback?.Invoke() && _clip != null)
                _audio.PlayOneShot(_clip, pitch: Random.Range(.95f, 1f), mixer: _mixer);
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


