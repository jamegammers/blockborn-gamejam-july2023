using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Utilities;
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

        public override string ToString() {
            return $"{_name}:{_score}";
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

        [SerializeField] private Canvas _canvas;
        [SerializeField] private RectTransform _listParent;
        [SerializeField] private GameObject _listItemPrefab;


        private void Awake() {
            Show(4100);
        }

        public void Show(int newScore) {
            // load scores
            // add new score
            // sort
            // check if list contains new score
            // - true: input name, save, display list
            // - false: display list

            Highscore newScoreObj = new("abc", newScore);
            string json = PlayerPrefs.GetString("highscores");

            List<Highscore> scores = new(JsonUtility.FromJson<HighscoreArray>(json).Scores);    // get scores from json
            scores.Add(newScoreObj);                                                            // add new score
            scores.Sort((a, b) => b.Score.CompareTo(a.Score));                                  // sort scores
            scores.SetLength(10);                                                               // keep only top 10 scores

            if (scores.Contains(newScoreObj)) {                              // check if list contains new score
                // input name
                // save
            } else {

            }

            FillList(scores.ToArray());

            // Debug.Log($"scores: { scores.ToArray().ToString<Highscore>() }");

            _canvas.enabled = true;
        }

        private void FillList(Highscore[] scores) {
            _listParent.DestroyChildren();

            foreach (Highscore score in scores) {
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
                new Highscore("lil", 10000),
                new Highscore("mer", 9000),
                new Highscore("ben", 8000),
                new Highscore("lil", 7000),
                new Highscore("mer", 6000),
                new Highscore("ben", 5000),
                new Highscore("lil", 4000),
                new Highscore("mer", 3000),
                new Highscore("ben", 2000),
                new Highscore("ben", 1000),
            });

            string json = JsonUtility.ToJson(scores);

            PlayerPrefs.SetString("highscores", json);
            PlayerPrefs.Save();

            Debug.Log($"highscores: { PlayerPrefs.GetString("highscores") }");
        }

    }
}
