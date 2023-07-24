using System.Collections;
using TMPro;
using UnityEngine;

namespace UI {

    public class HighscoreListItem : MonoBehaviour {

        [Header("References")]
        [SerializeField] private RectTransform _parent;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Animation _animation;


        public void Init(string name, int score, bool newScore = false) {
            _nameText.text = name;
            _scoreText.text = score.ToString();

            if (newScore)
                _animation.Play();
        }

        private void OnDisable() {
            _animation.Stop();
        }
    }

}