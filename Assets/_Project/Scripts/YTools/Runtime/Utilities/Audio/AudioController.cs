using UnityEngine;

namespace YTools
{
    public class AudioController : PersistentSingleton<AudioController>
    {
        private static ObjectPool<Audio> _pool;
        private static Transform _transform => Instance.transform;
        private static AudioData _data;

        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            _data = Resources.Load<AudioData>($"{nameof(YTools)}/{nameof(AudioData)}")?.Init();

            if (_data == null)
            {
                Message.Send("AudioData in null", DebugType.Error);
                return;
            }

            var source = new GameObject().AddComponent<Audio>();

            _pool = new ObjectPool<Audio>(source, _data.Preload, true, parent: _transform).ForEach((x, index) =>
            {
                x.Init($"Audio {index}", _pool, _data, _transform);
            });
        }

        public static Audio Get(System.Action<Audio> callback = null)
        {
            var source = _pool.Get(item => item.Init($"[Created] Audio", _pool, _data, _transform));
            callback?.Invoke(source);
            return source;
        }
    }
}