using Sirenix.OdinInspector;
using UnityEngine;

namespace Audio {

    [CreateAssetMenu(fileName = "AudioSample", menuName = "AudioSample", order = 2)]
    // [ExecuteAlways]
    public class AudioSample : ScriptableObject {

        [SerializeField] private AudioClip _clip;
        [SerializeField] private float _volume = 1f;
        [SerializeField] private float _pitch = 1f;

        public AudioClip clip => _clip;
        public float volume => _volume;
        public float pitch => _pitch;


        internal void SetClip(AudioClip newClip) => _clip = newClip;
        internal void SetVolume(float newVolume) => _volume = newVolume;


        [PropertySpace(100), Button(ButtonSizes.Gigantic)]
        #if UNITY_EDITOR
        public void TestPlay() {
            if (!Application.isPlaying) {
                Debug.LogWarning("AudioSample can only be played in play mode");
                return;
            }

            AudioManager.PlayAudio(this, Camera.main.transform.position);
        }
        #endif

    }

}