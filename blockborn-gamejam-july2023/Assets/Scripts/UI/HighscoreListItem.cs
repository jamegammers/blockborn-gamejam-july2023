using TMPro;
using UnityEngine;

namespace UI {

    public class HighscoreListItem : MonoBehaviour {

        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _scoreText;

        public void Init(string name, int score) {
            _nameText.text = name;
            _scoreText.text = score.ToString();
        }

    }

}