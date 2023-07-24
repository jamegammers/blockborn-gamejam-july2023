using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI {

    public class DeathScreen : MonoBehaviour {

        public Action OnContinue;

        [SerializeField] private TMP_Text _countdownText;
        [SerializeField] private HighscoresList _highscoresList;

        [Space(15)]
        [SerializeField] private UnityEvent _onContinue;

        private int _countdown = 15;


        private void Awake() {
            gameObject.SetActive(false);
        }

        #if UNITY_EDITOR
        [ContextMenu("Show")]
        public void TestShow() {
            Show(6900);
        }
        #endif

        public void Show(int score) {
            gameObject.SetActive(true);

            CoinInserter.WaitForCoin(() => {
                gameObject.SetActive(false);
                _onContinue.Invoke();
                OnContinue?.Invoke();
            });

            StartCoroutine(Countdown(score));
        }

        private IEnumerator Countdown(int score) {
            while (_countdown > 0) {
                _countdownText.text = _countdown.ToString();
                yield return new WaitForSeconds(1f);
                _countdown--;
            }

            CoinInserter.CancelCoin();
            gameObject.SetActive(false);
            _highscoresList.Show(score);
        }

    }

}