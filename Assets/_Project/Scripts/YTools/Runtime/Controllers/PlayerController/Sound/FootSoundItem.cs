using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace YTools
{
    [System.Serializable]
    public class FootSoundItem
    {
        public float Volume = 1;
        public AudioMixerGroup AudioMixer;

        [Space(10)]
        public Data.MinMax IntervalWalk = new(0.1f, 0.2f);
        public Data.MinMax IntervalRunning = new(0.05f, 0.1f);
        public Data.MinMax Pich = new(0.95f, 1.05f);

        [Space(5)]
        public List<AudioClip> Clips = new();
    }
}
