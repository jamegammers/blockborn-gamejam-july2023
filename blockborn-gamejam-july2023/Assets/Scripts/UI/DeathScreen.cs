using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI {

    public class DeathScreen : MonoBehaviour {

        public Action OnContinue;
        public Action OnReturnToStart;

        [SerializeField] private TMP_Text _countdownText;

        [Space(15)]
        [SerializeField] private UnityEvent _onContinue;
        [SerializeField] private UnityEvent _onReturnToStart;

        private int _countdown = 15;


        private void Awake() {
            gameObject.SetActive(false);
        }

        #if UNITY_EDITOR
        [ContextMenu("Show")]
        #endif
        public void Show() {
            gameObject.SetActive(true);

            CoinInserter.WaitForCoin(() => {
                gameObject.SetActive(false);
                _onContinue.Invoke();
                OnContinue?.Invoke();
            });

            StartCoroutine(Countdown());
        }

        private IEnumerator Countdown() {
            while (_countdown > 0) {
                _countdownText.text = _countdown.ToString();
                yield return new WaitForSeconds(1f);
                _countdown--;
            }

            CoinInserter.CancelCoin();
            gameObject.SetActive(false);
            _onReturnToStart.Invoke();
        }

    }

}