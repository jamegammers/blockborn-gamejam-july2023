using UnityEngine;
using UnityEngine.Audio;

namespace Audio {

    [ExecuteAlways]
    public class ArcadeAudio : MonoBehaviour {

        private static ArcadeAudio Instance { get; set; }

        [SerializeField] private AudioMixerGroup _mixer;

        // #if UNITY_EDITOR
        // [SerializeField] private AudioClip _testClip;
        // #endif


        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void PlayAudio(AudioSample sample) {
            if (Instance == null) {
                Debug.LogError("Audio instance is null");
                return;
            }

            AudioManager.PlayAudio(sample, transform.position, _mixer);
        }

        // #if UNITY_EDITOR
        // [ContextMenu("Play Sound")]
        // private void TestPlaySound() {
        //     PlayAudio(_testClip);
        // }
        // #endif

    }

}