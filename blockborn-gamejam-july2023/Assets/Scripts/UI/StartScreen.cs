using System;
using UnityEngine;
using UnityEngine.Events;

namespace UI {

    public class StartScreen : MonoBehaviour {

        [SerializeField] private UnityEvent _onGameStart;

        public Action OnGameStart;


        private void Start() {
            CoinInserter.WaitForCoin(() => {
                _onGameStart.Invoke();
                OnGameStart?.Invoke();
                Destroy(gameObject);
            });
        }

    }

}