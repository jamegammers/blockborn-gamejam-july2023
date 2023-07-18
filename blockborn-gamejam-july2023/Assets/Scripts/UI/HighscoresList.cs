using System;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace UI {

    [Serializable]
    public struct Highscore {

        [SerializeField] private string _name;
        [SerializeField] private int _score;

        public string Name => _name;
        public int Score => _score;

        public Highscore(string name, int score) {
            _name = name;
            _score = score;
        }

    }

    [Serializable]
    public struct HighscoreArray {

        [SerializeField] private Highscore[] _scores;

        public Highscore[] Scores => _scores;

        public HighscoreArray(Highscore[] scores) {
            _scores = scores;
        }

    }

    public class HighscoresList : MonoBehaviour {

        [SerializeField] private RectTransform _listParent;
        [SerializeField] private GameObject _listItemPrefab;

        private void Awake() {
            Show(0);
        }

        public void Show(int newScore) {
            // load scores, fill list, make visible...
            // check if new score is in list
            // - true: name input + save

            string json = PlayerPrefs.GetString("highscores");
            HighscoreArray scores = JsonUtility.FromJson<HighscoreArray>(json);
            FillList(scores);
        }

        private void FillList(HighscoreArray scores) {
            _listParent.DestroyChildren();

            foreach (Highscore score in scores.Scores) {
                // add new item to the list
                GameObject listItem = Instantiate(_listItemPrefab, _listParent, true);
                listItem.GetComponent<HighscoreListItem>().Init(score.Name, score.Score);

                // reset rotation and scale
                RectTransform rect = (RectTransform) listItem.transform;
                rect.localScale = Vector3.one;
                rect.localRotation = Quaternion.identity;

                // reset position
                Vector3 pos = rect.localPosition;
                pos.z = 0;
                rect.localPosition = pos;
            }
        }

        [ContextMenu("WriteTestScores")]
        public void WriteTestScores() {
            HighscoreArray scores = new(new [] {
                new Highscore("mer", 169),
                new Highscore("ben", 128),
                new Highscore("lil", 420)
            });

            string json = JsonUtility.ToJson(scores);

            PlayerPrefs.SetString("highscores", json);
            PlayerPrefs.Save();

            Debug.Log($"highscores: { PlayerPrefs.GetString("highscores") }");
        }

    }
}
