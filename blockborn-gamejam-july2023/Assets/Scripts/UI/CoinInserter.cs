using System;
using Audio;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace UI {

    public class CoinInserter : MonoBehaviour {

        [SerializeField, Required] private RectTransform _coinDisplay;
        [SerializeField, Required] private TMP_Text _text;
        [SerializeField, Required] private RectTransform _hintDisplay;
        [SerializeField] private int _coins = 3;

        [Space(15), Header("Settings")]
        [SerializeField] private Vector2 _displayVisibleOffset = new(0, -50);
        [SerializeField] private Vector2 _hintVisibleOffset = new(0, 50);

        [SerializeField] private float _animationDuration = 1f;
        [SerializeField] private LeanTweenType _animationEasing = LeanTweenType.easeOutQuad;
        [SerializeField] private float _displayDuration = 1f;
        [SerializeField, Required] private AudioSample _coinSound;

        private static CoinInserter Instance { get; set; }
        // ReSharper disable once InconsistentNaming
        private static Action OnCoinInserted;

        private PlayerInput _playerInput;


        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            
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
            AudioManager.PlayAudio(_coinSound, transform.position);

            OnCoinInserted?.Invoke();
            HideDisplay(_displayDuration);

            // unsubscribe all listeners
            OnCoinInserted = null;
        }

        public static bool WaitForCoin(Action callback) => Instance.WaitForCoinInstance(callback);

        private bool WaitForCoinInstance(Action callback) {
            ShowDisplay();
            if (_coins <= 0) return false;

            OnCoinInserted += callback;
            return true;
        }
        
        public static void CancelCoin() => Instance.CancelCoinInstance();
        
        private void CancelCoinInstance() {
            OnCoinInserted = null;
            HideDisplay();
        }


        private void ShowDisplay() {
            _text.text = _coins.ToString();
            MoveDisplay(_coinDisplay, _displayVisibleOffset);
            MoveDisplay(_hintDisplay, _hintVisibleOffset);
        }
        private void HideDisplay(float delay = 0) {
            MoveDisplay(_coinDisplay, Vector3.zero, delay);
            MoveDisplay(_hintDisplay, Vector3.zero);
        }

        private void MoveDisplay(RectTransform rectTransform, Vector2 offset, float delay = 0f) {
            LeanTween.value(gameObject,
                    value => rectTransform.anchoredPosition = value,
                    (Vector3) rectTransform.anchoredPosition,
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