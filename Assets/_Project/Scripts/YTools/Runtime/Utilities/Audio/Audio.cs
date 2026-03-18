using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace YTools
{
    public class Audio : MonoBehaviour, System.IDisposable
    {
        public AudioSource _source;

        private ObjectPool<Audio> _pool;
        private AudioData _data;

        public void Init(string name, ObjectPool<Audio> pool, AudioData data, Transform parent)
        {
            _pool = pool;
            _data = data;
            this.name = name;
            transform.parent = parent;
            _source = gameObject.AddComponent<AudioSource>();
            _source.playOnAwake = false;
        }

        public Audio Play(AudioClip clip, float volume = 1, float pitch = 1, AudioMixerGroup mixer = null)
        {
            if(clip == null)
            {
                Message.Send($"Clip is null".Color(ColorType.Cyan));
                return this;
            }

            _source.clip = clip;
            _source.volume = volume;
            _source.pitch = pitch;
            _source.outputAudioMixerGroup = mixer;
            _source.Play();

            return this;
        }

        public Audio Play(string key, float volume = 1, float pitch = 1, AudioMixerGroup mixer = null)
        {
            Play(_data.Get(key.ToLower().Trim()), volume, pitch, mixer);

#if UNITASK

            WaitForPlaybackCompletion().Forget();
#else
            StartCoroutine(WaitForPlaybackCompletion());
#endif
            return this;
        }

#if UNITASK
        private async Cysharp.Threading.Tasks.UniTaskVoid WaitForPlaybackCompletion()
        {
            await Cysharp.Threading.Tasks.UniTask.WaitUntil(() => !_source.isPlaying);

            _pool.Return(this);
            Dispose();
        }
#else
        private IEnumerator WaitForPlaybackCompletion()
        {
            yield return new WaitUntil(() => !_source.isPlaying);

            _pool.Return(this);
            Dispose();
        }
#endif
#region Settings
        public Audio SetVolume(float volume)
        {
            _source.volume = volume;
            return this;
        }

        public Audio SetLoop(bool loop)
        {
            _source.loop = loop;
            return this;
        }

        public Audio SetPitch(float pitch)
        {
            _source.pitch = pitch;
            return this;
        }

        public Audio SetAudioMixer(AudioMixerGroup mixer)
        {
            _source.outputAudioMixerGroup = mixer;
            return this;
        }

        public Audio SetPosition(Vector3 position)
        {
            _source.transform.position = position;
            return this;
        }

        public Audio SetDistance(float min, float max, float spatialBlend = 1)
        {
            _source.minDistance = min;
            _source.maxDistance = max;
            _source.spatialBlend = spatialBlend;
            return this;
        }
        #endregion

        public void Dispose()
        {
            _source.clip = null;
            _source.outputAudioMixerGroup = null;
            _source.volume = 1;
            _source.pitch = 1;
            _source.minDistance = 0;
            _source.maxDistance = 500;
            _source.spatialBlend = 0;
        }
    }
}