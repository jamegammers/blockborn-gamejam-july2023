using UnityEngine;

namespace UI {

    public class LogoAnimator : MonoBehaviour {

        [SerializeField] private float _frequency = 1f;
        [SerializeField] private float _amplitude = 100f;

        private RectTransform _rectTransform;
        private Vector2 _startPosition;


        private void Awake() {
            _rectTransform = (RectTransform) transform;
            _startPosition = _rectTransform.anchoredPosition;
        }

        private void Update() {
            float offset = Mathf.Sin(Time.time * _frequency) * _amplitude;
            _rectTransform.anchoredPosition = _startPosition + new Vector2(0, offset);
        }

    }

}