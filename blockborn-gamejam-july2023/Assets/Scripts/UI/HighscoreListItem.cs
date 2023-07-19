using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UI {

    public class HighscoreListItem : MonoBehaviour {

        [Header("References")]
        [SerializeField] private RectTransform _parent;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _scoreText;

        [Space(15), Header("Settings")]
        [SerializeField] private float _blinkingSpeed = 0.3f;

        private bool _blink = true;
        private IEnumerator _blinkCoroutine;


        public void Init(string name, int score, bool newScore = false) {
            _nameText.text = name;
            _scoreText.text = score.ToString();

            Debug.Log($"new score: {newScore}");
            if (newScore) {
                // _blinkCoroutine = Blink();
                StartCoroutine(Blink());
            }
        }

        private IEnumerator Blink() {
            while (_blink) {
                _parent.gameObject.SetActive(!_parent.gameObject.activeSelf);
                // Debug.Log($"Blinking {_parent.gameObject.activeSelf}");
                yield return new WaitForSeconds(_blinkingSpeed);
            }
        }

        private void OnDisable() {
            _blink = false;
            // StopCoroutine(_blinkCoroutine);
        }
    }

}