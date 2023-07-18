using System;
using System.Collections.Generic;
using UnityEngine;

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

        [SerializeField] private GameObject _listItemPrefab;

        public void Show(int newScore) {
            // load scores, fill list, make visible...
            // check if new score is in list
            // - true: name input + save
        }

        [ContextMenu("WriteTestScores")]
        public void WriteTestScores() {
            HighscoreArray scores = new( new [] {
                    new Highscore("mer", 169),
                    new Highscore("ben", 128),
                    new Highscore("lil", 420)
                }
            );

            string json = JsonUtility.ToJson(scores);

            PlayerPrefs.SetString("highscores", json);
            PlayerPrefs.Save();

            Debug.Log($"highscores: { PlayerPrefs.GetString("highscores") }");
            // Debug.Log($"highscores: {json}");
        }

    }
}
