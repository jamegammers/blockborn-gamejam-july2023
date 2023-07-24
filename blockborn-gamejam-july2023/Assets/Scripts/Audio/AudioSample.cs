using UnityEngine;
using Sirenix.OdinInspector;

namespace Audio {

    [CreateAssetMenu(fileName = "AudioSample", menuName = "AudioSample", order = 2)]
    public class AudioSample : ScriptableObject {

        [SerializeField] private AudioClip[] _clips;
        [SerializeField, Range(0, 2f)] private float _volume = 1f;
        [SerializeField] private bool _loop;
        [SerializeField, Range(0, 2f), DisableIf("_randomizePitch")] private float _pitch = 1f;
        [SerializeField] private bool _randomizePitch;
        [SerializeField, Range(0, 2f), ShowIf("_randomizePitch")] private float _pitchMin = 0.9f;
        [SerializeField, Range(0, 2f), ShowIf("_randomizePitch")] private float _pitchMax = 1.1f;

        public AudioClip[] Clips => _clips;
        public float volume => _volume;
        public bool loop => _loop;
        public float pitch => _pitch;
        public bool randomizePitch => _randomizePitch;
        public float pitchMin => _pitchMin;
        public float pitchMax => _pitchMax;


        internal void SetClips(AudioClip[] newClip) => _clips = newClip;
        internal void SetVolume(float newVolume) => _volume = newVolume;


        [PropertySpace(100), Button(ButtonSizes.Gigantic), HideInEditorMode]
        #if UNITY_EDITOR
        public void TestPlay() {
            AudioManager.PlayAudio(this, Camera.main.transform.position);
        }
        #endif

    }

}