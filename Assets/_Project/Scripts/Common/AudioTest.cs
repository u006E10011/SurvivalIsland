using Gaskellgames;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTest : MonoBehaviour
{
    [Title("Settings")]
    [SerializeField] private int _index;
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private List<AudioClip> _clips = new();

    [Title("Info")]
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioSource _source;

    private void OnValidate()
    {
		if(_clips.Count == 0)
			return;
			
        _index = Mathf.Clamp(_index, 0, _clips.Count - 1);
        _clip = _clips[_index];
    }

    [Button]
    public void PlayTarget()
    {
        _source.PlayOneShot(_clip);
    }

    [Button]
    public void PlayRandom()
    {
		var index = Random.Range(0, _clips.Count);
		_index = index;
		_clip = _clips[index];
        _source.PlayOneShot(_clips[index]);
    }
}