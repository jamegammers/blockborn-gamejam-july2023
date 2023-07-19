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
        [SerializeField] private float _animationDuration = 1f;
        [SerializeField] private LeanTweenType _animationEasing = LeanTweenType.easeOutQuad;

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
            HideDisplay(1f);

            // unsubscribe all listeners
            OnCoinInserted = null;
        }

        private void WaitForCoin(Action callback) {
            OnCoinInserted += callback;
            ShowDisplay();
        }

        private void ShowDisplay() => MoveDisplay(-_textTransform.rect.height);
        private void HideDisplay(float delay = 0) => MoveDisplay(0, delay);

        private void MoveDisplay(float newY, float delay = 0f) {
            LeanTween.value(gameObject, value => {
                    _textTransform.anchoredPosition = new Vector2(_textTransform.anchoredPosition.x, value);
                }, _textTransform.anchoredPosition.y, newY, _animationDuration)
                .setEase(_animationEasing)
                .setDelay(delay);
        }


        #if UNITY_EDITOR
        [ContextMenu("TestCoin")]
        private void TestCoin() {
            WaitForCoin(() => Debug.Log("Coin inserted"));
        }
        #endif

    }

}