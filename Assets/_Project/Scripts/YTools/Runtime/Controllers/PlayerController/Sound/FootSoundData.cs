using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace YTools
{
    [CreateAssetMenu(fileName = nameof(FootSoundData), menuName = "YTools/PlayerController/" + nameof(FootSoundData))]
    public class FootSoundData : ScriptableObject
    {
        public float Volume = 1;
        public AudioMixerGroup AudioMixerDefault;
        public AudioMixerGroup AudioMixerGrass;

        [Space(10)]
        public Data.MinMax IntervalWalk = new(0.1f, 0.2f);
        public Data.MinMax IntervalRunning = new(0.05f, 0.1f);
        public Data.MinMax Pich = new(0.95f, 1.05f);

        public AudioClip JumpUpClip;
        public AudioClip JumpDownClip;

        [Space(5)]
        public List<AudioClip> BoardClip = new();
        public List<AudioClip> SandClip = new();
        public List<AudioClip> ShowClip = new();
        public List<AudioClip> GrassClip = new();
        public List<AudioClip> StoneClip = new();
    }
}