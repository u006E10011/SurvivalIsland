using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace YTools
{
    public class FootSound : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] private FootSoundData _soundData;
        [SerializeField] private PlayerControllerData _playerData;

        //private SoundControllerInstance _source;
        private Dictionary<int, (List<AudioClip> clip, AudioMixerGroup mixer)> _clips = new();
        private float _time;

        private void Awake()
        {
            //_source = gameObject.AddComponent<SoundControllerInstance>();
            //_source.SetMixer(_soundData.AudioMixerDefault).SetVolume(_soundData.Volume);

            Init();
        }

        //private void OnEnable()
        //{
        //    PlayerControllerEvent.OnRunning += Run;
        //    PlayerControllerEvent.OnWalk += Walk;
        //    PlayerControllerEvent.OnJumpUp += JumpUp;
        //    PlayerControllerEvent.OnJumpDown += JumpDown;
        //}

        //private void OnDisable()
        //{
        //    PlayerControllerEvent.OnRunning -= Run;
        //    PlayerControllerEvent.OnWalk -= Walk;
        //    PlayerControllerEvent.OnJumpUp -= JumpUp;
        //    PlayerControllerEvent.OnJumpDown -= JumpDown;
        //}

        private void Init()
        {
            _clips = new()
            {
                {10, (_soundData.BoardClip, null) },
                {20, (_soundData.BoardClip, null) },

                {11, (_soundData.SandClip, null) },
                {21, (_soundData.SandClip, null) },

                {12, (_soundData.StoneClip, null) },
                {22, (_soundData.StoneClip, null) },

                {13, (_soundData.GrassClip, _soundData.AudioMixerGrass) },
                {23, (_soundData.GrassClip, _soundData.AudioMixerGrass) },
            };
        }

        private void Update() => _time += Time.deltaTime;

        private void Play(Data.MinMax interval)
        {
            var sound = Sound();
            var clips = sound.clip;

            if (clips.Count == 0)
            {
                Debug.Log("Food sound count 0".Color(ColorType.Cyan));
                return;
            }

            if (_time >= Random.Range(interval.Min, interval.Max))
            {
                var index = Random.Range(0, clips.Count);
                var clip = clips[index];

                //_source.PlaySFX(clip)
                //    .SetPitch(Random.Range(_soundData.Pich.Min, _soundData.Pich.Max))
                //    .SetMixer(sound.mixer);

                _time = 0;
            }
        }

        private void Walk() => Play(_soundData.IntervalWalk);
        private void Run() => Play(_soundData.IntervalRunning);
        //private void JumpUp() => _source.PlaySFX(_soundData.JumpUpClip);
        //private void JumpDown() => _source.PlaySFX(_soundData.JumpDownClip);

        private (List<AudioClip> clip, AudioMixerGroup mixer) Sound()
        {
            int layerIndex = _player.Physics.Hit.collider.gameObject.layer;

            if (IsLayerInMask(layerIndex, _playerData.Ground) || IsLayerInMask(layerIndex, _playerData.Attachable))
            {
                if (_clips.TryGetValue(layerIndex,  out var value))
                    return value;
            }

            return (_soundData.BoardClip, null);
        }

        private bool IsLayerInMask(int layerIndex, LayerMask mask)
        {
            return (mask.value & (1 << layerIndex)) != 0;
        }
    }
}