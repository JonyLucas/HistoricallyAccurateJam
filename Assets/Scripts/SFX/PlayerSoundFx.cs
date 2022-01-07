using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Audio
{
    [CreateAssetMenu(fileName = "Audio Clips", menuName = "Audio Clips", order = 53)]
    public class PlayerSoundFx : ScriptableObject
    {
        [SerializeField]
        private List<AudioClip> _audioClips;

        public List<AudioClip> AudioClips
        { get { return _audioClips; } }

        public AudioClip GetRandomSound()
        {
            var count = _audioClips.Count;
            var index = (int)Random.Range(0, count);
            return _audioClips[index];
        }
    }
}