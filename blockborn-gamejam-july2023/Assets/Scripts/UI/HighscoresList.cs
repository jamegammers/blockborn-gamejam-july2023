using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Utilities;
using UnityEngine.SceneManagement;
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

        public override string ToString() => $"{_name}:{_score}";

    }

    [Serializable]
    public struct HighscoreArray {

        [SerializeField] private Highscore[] _scores;

        public Highscore[] Scores => _scores;

        public HighscoreArray(Highscore[] scores) {
            _scores = scores;
        }

        public readonly string ToJson() => JsonUtility.ToJson(this);
    }

    public class HighscoresList : MonoBehaviour {

        [Header("Score list")]
        [SerializeField] private RectTransform _listParent;
        [SerializeField] private RectTransform _listBox;
        [SerializeField] private GameObject _listItemPrefab;
        [SerializeField] private RectTransform _inputParent;

        [Space(15), Header("Input")]
        [SerializeField] private HighscoreInput _highscoreInput;

        private List<Highscore> _scores = new();
        private Highscore _newScore;
        private PlayerInput _playerInput;

        private static readonly HighscoreArray DEFAULT_SCORES = new(new[] {
            new Highscore("mer", 1337),
            new Highscore("ben", 4242),
            new Highscore("jan", 6969),
            new Highscore("fan", 4200),
            new Highscore("isa", 1690),
        });


        private void Awake() {
            _listParent.gameObject.SetActive(false);
            _inputParent.gameObject.SetActive(false);

            // only for testing
            // Show(4200);
            // CoinInserter.OnCoinInserted += () => Show(4200);

            _playerInput = new PlayerInput();
            _playerInput.Player.Any.performed += _ => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Show(int newScore) {
            // load scores
            // add new score
            // sort
            // check if list contains new score
            // - true: input name, save, display list
            // - false: display list

            _newScore = new Highscore("AAA", newScore);
            string json = PlayerPrefs.GetString("highscores", DEFAULT_SCORES.ToJson());

            _scores = new List<Highscore>(JsonUtility.FromJson<HighscoreArray>(json).Scores);   // get scores from json
            _scores.Add(_newScore);                                                             // add new score
            _scores.Sort((a, b) => b.Score.CompareTo(a.Score));                                 // sort scores
            if (_scores.Count > 10) _scores.SetLength(10);                                      // keep only top 10 scores

            if (_scores.Contains(_newScore)) {                              // check if list contains new score
                // input name, wait for submit
                _highscoreInput.OnSubmit += OnInputEnded;
                _highscoreInput.StartInput(newScore);
                return;
            }

            // display list if no new highscore
            FillList(_scores.ToArray());
            // Debug.Log($"scores: { scores.ToArray().ToString<Highscore>() }");
        }

        private void OnInputEnded(string name) {
            // update name of new score and save scores
            int index = _scores.IndexOf(_newScore);
            _newScore = new Highscore(name, _newScore.Score);
            _scores[index] = _newScore;
            SaveScores(_scores.ToArray());

            // show scores list
            FillList(_scores.ToArray());

            // Debug.Log($"name: {name}");
            _highscoreInput.OnSubmit -= OnInputEnded;
        }

        private void FillList(IEnumerable<Highscore> scores) {
            _listBox.DestroyChildren();

            foreach (Highscore score in scores) {
                // add new item to the list
                GameObject listItem = Instantiate(_listItemPrefab, _listBox, true);
                HighscoreListItem item = listItem.GetComponent<HighscoreListItem>();
                item.Init(score.Name, score.Score, score.Equals(_newScore));

                // reset rotation and scale
                RectTransform rect = (RectTransform) listItem.transform;
                rect.localScale = Vector3.one;
                rect.localRotation = Quaternion.identity;

                // reset position
                Vector3 pos = rect.localPosition;
                pos.z = 0;
                rect.localPosition = pos;
            }

            _listParent.gameObject.SetActive(true);
            _playerInput.Enable();
        }

        private static void SaveScores(Highscore[] scores) {
            string json = new HighscoreArray(scores).ToJson();
            PlayerPrefs.SetString("highscores", json);
            PlayerPrefs.Save();

            Debug.Log($"saved highscores: { PlayerPrefs.GetString("highscores") }");
        }

        private void OnDisable() => _playerInput.Disable();


        #if UNITY_EDITOR

        [ContextMenu("WriteTestScores")]
        public void WriteTestScores() {
            SaveScores(new [] {
                new Highscore("lil", 10000),
                new Highscore("mer", 9000),
                new Highscore("ben", 8000),
                // new Highscore("lil", 7000),
                // new Highscore("mer", 6000),
                // new Highscore("ben", 5000),
                // new Highscore("lil", 4000),
                // new Highscore("mer", 3000),
                // new Highscore("ben", 2000),
                // new Highscore("ben", 1000),
            });
        }

        [ContextMenu("DeleteHighscores")]
        public void DeleteHighscores() {
            PlayerPrefs.DeleteKey("highscores");
            PlayerPrefs.Save();
            Debug.Log($"deleted highscores { PlayerPrefs.GetString("highscores") }");
        }

        [ContextMenu("TestShowScores")]
        public void TestShowScores() {
            CoinInserter.WaitForCoin(() => {
                Show(4200);
            });
        }

        #endif

    }
}
