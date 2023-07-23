using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio {

    [ExecuteAlways]
    public class AudioManager : MonoBehaviour {

        // #if UNITY_EDITOR
        // [SerializeField] private AudioClip _testClip;
        // #endif

        private static AudioManager Instance { get; set; }


        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public static void PlayAudio(AudioSample sample, Vector3 position, AudioMixerGroup mixer = null) {
            if (Instance == null) {
                Debug.LogError("Audio instance is null");
                return;
            }

            Instance.PlayAudioInstance(sample, position, mixer);
        }

        private void PlayAudioInstance(AudioSample sample, Vector3 position, AudioMixerGroup mixer = null) {
            // select random clip
            AudioClip clip = sample.Clips[Random.Range(0, sample.Clips.Length)];

            GameObject audioInstance = new() {
                transform = { position = position },
                name = $"AudioInstance ({clip})"
            };

            AudioSource audioSource = audioInstance.AddComponent<AudioSource>();
            audioSource.clip = clip;


            audioSource.volume = sample.volume;
            audioSource.pitch = sample.randomizePitch ? Random.Range(sample.pitchMin, sample.pitchMax) : sample.pitch;
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


        // #if UNITY_EDITOR
        // [ContextMenu("Play Sound")]
        // private void TestPlaySound() {
        //     PlayAudio(_testClip, transform.position);
        // }
        // #endif

    }

}