using System.Collections;
using TMPro;
using UnityEngine;

namespace UI {

    public class HighscoreInput : MonoBehaviour {

        [Header("References")]
        [SerializeField] private TMP_Text _upArrow;
        [SerializeField] private TMP_Text _downArrow;
        [SerializeField] private TMP_Text _nameText;

        [SerializeField] private TMP_Text _scoreText;

        [Space(15), Header("Settings")]
        [SerializeField] private float _blinkingSpeed = 0.3f;

        private int[] _name = {1, 1, 1};
        private int _charIndex;
        private PlayerInput _playerInput;

        private bool _blinkArrows = true;
        private IEnumerator _blinkArrowsCoroutine;

        private const string ALPHABET = " ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly TextAlignmentOptions[] ALIGNMENT_OPTIONS = new TextAlignmentOptions[] {
            TextAlignmentOptions.MidlineLeft, TextAlignmentOptions.Midline, TextAlignmentOptions.MidlineRight
        };


        private void Awake() {
            _playerInput = new PlayerInput();
            _playerInput.Enable();

            _playerInput.Player.Move.performed += ctx => {
                Vector2 move = ctx.ReadValue<Vector2>();

                if (Mathf.Abs(move.y) > 0.25f)
                    UpdateChar(move.y);
                else if (Mathf.Abs(move.x) > 0.25f)
                    UpdateArrows(move.x);
            };

            UpdateText();

            _blinkArrowsCoroutine = BlinkArrows();
            StartCoroutine(_blinkArrowsCoroutine);
        }


        private void OnEnable() {
            _playerInput.Enable();
        }

        private void UpdateChar(float y) {
            _name[_charIndex] = y switch {
                > 0 => (_name[_charIndex] + 1) % 26,
                < 0 => (_name[_charIndex] + 25) % 26,
                _ => _name[_charIndex]
            };

            UpdateText();
        }

        private void UpdateText() {
            _nameText.text = "";
            for (int i = 0; i < 3; i++)
                _nameText.text += ALPHABET[_name[i]];
        }

        private void UpdateArrows(float x) {
            _charIndex = x switch {
                > 0 => (_charIndex + 1) % 3,
                < 0 => (_charIndex + 2) % 3,
                _ => _charIndex
            };

            _upArrow.alignment = ALIGNMENT_OPTIONS[_charIndex];
            _downArrow.alignment = ALIGNMENT_OPTIONS[_charIndex];
        }


        private IEnumerator BlinkArrows() {
            while (_blinkArrows) {
                _upArrow.enabled = !_upArrow.enabled;
                _downArrow.enabled = !_downArrow.enabled;
                yield return new WaitForSeconds(_blinkingSpeed);
            }
        }


        private void OnDisable() {
            _playerInput.Disable();

            _blinkArrows = false;
            StopCoroutine(_blinkArrowsCoroutine);
        }

    }

}