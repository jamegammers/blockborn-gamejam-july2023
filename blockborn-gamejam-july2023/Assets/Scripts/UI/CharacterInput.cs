using System;
using TMPro;
using UnityEngine;

namespace UI {

    public class CharacterInput : MonoBehaviour {

        [Header("References")]
        [SerializeField] private RectTransform _parent;
        [SerializeField] private TMP_Text _upArrow;
        [SerializeField] private TMP_Text _downArrow;
        [SerializeField] private TMP_Text _nameText;

        [SerializeField] private TMP_Text _scoreText;

        // ReSharper disable once StringLiteralTypo
        [Space(15), Header("Settings")]
        [SerializeField] private string _alphabet = " ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public Action<string> OnSubmit;


        private int[] _value;
        private int _charIndex;
        private PlayerInput _playerInput;


        private static readonly TextAlignmentOptions[] ALIGNMENT_OPTIONS = {
            TextAlignmentOptions.MidlineLeft, TextAlignmentOptions.Midline, TextAlignmentOptions.MidlineRight
        };

        private void Awake() {
            _playerInput = new PlayerInput();

            _playerInput.Player.Move.performed += ctx => {
                Vector2 move = ctx.ReadValue<Vector2>();

                if (Mathf.Abs(move.y) > 0.25f)
                    UpdateChar(move.y);
                else if (Mathf.Abs(move.x) > 0.25f)
                    UpdateArrows(move.x);
            };

            _playerInput.Player.Jump.performed += _ => {
                OnSubmit?.Invoke(_nameText.text);
                _playerInput.Disable();
                _parent.gameObject.SetActive(false);
            };
        }

        public void StartInput(int[] defaultValue) {
            _value = defaultValue;
            _playerInput.Enable();
            UpdateText();
            // _parent.gameObject.SetActive(true);
        }


        // private void OnEnable() {
        //     _playerInput.Enable();
        // }

        private void UpdateChar(float y) {
            _value[_charIndex] = y switch {
                > 0 => (_value[_charIndex] + 1) % _alphabet.Length,
                < 0 => (_value[_charIndex] + 25) % _alphabet.Length,
                _ => _value[_charIndex]
            };

            UpdateText();
        }

        private void UpdateText() {
            _nameText.text = "";
            for (int i = 0; i < 3; i++)
                _nameText.text += _alphabet[_value[i]];
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


        private void OnDisable() {
            _playerInput.Disable();
        }

    }

}