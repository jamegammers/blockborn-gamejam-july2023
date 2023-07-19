using System;
using TMPro;
using UnityEngine;

namespace UI {

    public class CoinInserter : MonoBehaviour {

        // public static CoinInserter Instance { get; private set; }
        public static Action OnCoinInserted;

        [SerializeField] private TMP_Text _text;
        [SerializeField] private int _coins = 3;

        private PlayerInput _playerInput;


        private void Awake() {
            // if (Instance != null) {
            //     Destroy(gameObject);
            //     return;
            // }
            //
            // Instance = this;

            _playerInput = new PlayerInput();
            _playerInput.Enable();
            _playerInput.Player.InsertCoin.performed += _ => InsertCoin();
        }

        private void InsertCoin() {
            // skip if no listeners are subscribed
            if (OnCoinInserted == null) return;

            // do nothing if coins are empty
            if (_coins <= 0) {
                _coins = 0;
                return;
            }

            _coins--;
            OnCoinInserted?.Invoke();
            _text.text = _coins.ToString();

            // unsubscribe all listeners
            OnCoinInserted = null;
        }

    }

}