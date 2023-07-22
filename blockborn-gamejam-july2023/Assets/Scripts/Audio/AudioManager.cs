using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio {

    [ExecuteAlways]
    public class AudioManager : MonoBehaviour {

        #if UNITY_EDITOR
        [SerializeField] private AudioClip _testClip;
        #endif

        private static AudioManager Instance { get; set; }


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

            AudioSample sample = ScriptableObject.CreateInstance<AudioSample>();
            sample.SetClip(clip);
            sample.SetVolume(volume);

            Instance.PlayAudioInstance(sample, position, volume, mixer);
        }

        public static void PlayAudio(AudioSample sample, Vector3 position) {
            if (Instance == null) {
                Debug.LogError("Audio instance is null");
                return;
            }

            Instance.PlayAudioInstance(sample, Instance.transform.position);
        }

        private void PlayAudioInstance(AudioSample sample, Vector3 position, float volume = 1f, AudioMixerGroup mixer = null) {
            GameObject audioInstance = new() {
                transform = { position = position },
                name = $"AudioInstance ({sample.clip.name})"
            };

            AudioSource audioSource = audioInstance.AddComponent<AudioSource>();
            audioSource.clip = sample.clip;
            audioSource.volume = sample.volume;
            audioSource.pitch = sample.pitch;
            audioSource.outputAudioMixerGroup = mixer;
            audioSource.spatialBlend = 1f;
            audioSource.Play();

            StartCoroutine(DestroyAudioInstance(audioInstance, sample.clip.length));
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