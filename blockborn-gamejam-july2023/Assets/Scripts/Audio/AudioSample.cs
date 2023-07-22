using UnityEngine;

namespace Audio {

    [CreateAssetMenu(fileName = "AudioSample", menuName = "AudioSample", order = 2)]
    public class AudioSample : ScriptableObject {

        [SerializeField] private AudioClip _clip;
        [SerializeField] private float _volume = 1f;
        [SerializeField] private float _pitch = 1f;

        public AudioClip clip => _clip;
        public float volume => _volume;
        public float pitch => _pitch;


        internal void SetClip(AudioClip newClip) => _clip = newClip;
        internal void SetVolume(float newVolume) => _volume = newVolume;

    }

}