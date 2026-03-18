using System.Collections.Generic;
using UnityEngine;

namespace YTools
{
    [CreateAssetMenu(fileName = nameof(AudioData), menuName = "YTools/" + nameof(AudioData))]
    public class AudioData : ScriptableObject
    {
        public int Preload = 10;
        public List<AudioClipData> Clip = new();

        private readonly Dictionary<string, AudioClip> _clip = new();

        public AudioData Init()
        {
            _clip.Clear();

            foreach (var item in Clip)
            {
                if (!string.IsNullOrEmpty(item.Key) && item.Clip != null)
                    _clip.Add(item.Key.ToLower().Trim(), item.Clip);
            }

            return this;
        }

        public AudioClip Get(string key)
        {
            if (_clip.TryGetValue(key.ToLower().Trim(), out var clip))
                return clip;

            Message.Send($"Clip is null by key [{key.ToLower().Trim().Color(ColorType.Cyan)}]");
            return null;
        }
    }
}