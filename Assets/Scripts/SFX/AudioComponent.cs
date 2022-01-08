using Game.Audio;
using UnityEngine;

namespace Game.UI.Sound
{
    public class AudioComponent : MonoBehaviour
    {
        [SerializeField]
        private SoundFx _soundFx;

        private AudioSource _source;

        // Start is called before the first frame update
        private void Start()
        {
            _source = GetComponent<AudioSource>();
        }

        public void Play()
        {
            _source.clip = _soundFx.GetRandomSound();
            _source.Play();
        }
    }
}