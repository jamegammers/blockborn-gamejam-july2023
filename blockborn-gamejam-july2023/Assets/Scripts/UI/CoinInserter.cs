using System;
using TMPro;
using UnityEngine;

namespace UI {

    public class CoinInserter : MonoBehaviour {

        public static Action OnCoinInserted;

        [SerializeField] private RectTransform _textTransform;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private int _coins = 3;

        [Space(15), Header("Settings")]
        [SerializeField] private Vector2 _displayVisibleOffset = new(0, -50);
        [SerializeField] private float _animationDuration = 1f;
        [SerializeField] private LeanTweenType _animationEasing = LeanTweenType.easeOutQuad;
        [SerializeField] private float _displayDuration = 1f;

        private PlayerInput _playerInput;


        private void Awake() {
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
            _text.text = _coins.ToString();
            OnCoinInserted?.Invoke();
            HideDisplay(_displayDuration);

            // unsubscribe all listeners
            OnCoinInserted = null;
        }

        private bool WaitForCoin(Action callback) {
            ShowDisplay();
            if (_coins <= 0) return false;

            OnCoinInserted += callback;
            return true;
        }

        private void ShowDisplay() => MoveDisplay(_displayVisibleOffset);
        private void HideDisplay(float delay = 0) => MoveDisplay(Vector3.zero, delay);

        private void MoveDisplay(Vector2 offset, float delay = 0f) {
            LeanTween.value(gameObject,
                    value => _textTransform.anchoredPosition = value,
                    (Vector3) _textTransform.anchoredPosition,
                    (Vector3) offset,
                    _animationDuration)
                .setEase(_animationEasing)
                .setDelay(delay);
        }


        #if UNITY_EDITOR
        [ContextMenu("TestCoin")]
        private void TestCoin() {
            bool result = WaitForCoin(() => Debug.Log("Coin inserted"));
            Debug.Log(result ? "Coin inserted" : "No coins left");
        }
        #endif

    }

}