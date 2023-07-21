using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio {

    [ExecuteAlways]
    public class Audio : MonoBehaviour {

        #if UNITY_EDITOR
        [SerializeField] private AudioClip _testClip;
        #endif

        private static Audio Instance { get; set; }


        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public static void PlayAudio(AudioClip clip, Vector3 position, float volume = 1f, AudioMixerGroup mixer = null) {
            if (Instance == null) {
                Debug.LogError("Audio instance is null");
                return;
            }

            Instance.PlayAudioInstance(clip, position, volume, mixer);
        }

        private void PlayAudioInstance(AudioClip clip, Vector3 position, float volume = 1f, AudioMixerGroup mixer = null) {
            GameObject audioInstance = new() {
                transform = { position = position },
                name = $"AudioInstance ({clip.name})"
            };

            AudioSource audioSource = audioInstance.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.outputAudioMixerGroup = mixer;
            audioSource.spatialBlend = 1f;
            audioSource.Play();

            StartCoroutine(DestroyAudioInstance(audioInstance, clip.length));
        }

        private static IEnumerator DestroyAudioInstance(Object instance, float duration) {
            yield return new WaitForSeconds(duration);

            if (!Application.isPlaying)
                DestroyImmediate(instance);
            else
                Destroy(instance);
        }


        #if UNITY_EDITOR
        [ContextMenu("Play Sound")]
        private void TestPlaySound() {
            PlayAudio(_testClip, transform.position);
        }
        #endif

    }

}