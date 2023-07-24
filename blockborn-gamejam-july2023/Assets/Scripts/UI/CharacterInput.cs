using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace UI {

    public class CharacterInput : MonoBehaviour {

        [Header("References")]
        [SerializeField] private TMP_Text _upArrow;
        [SerializeField] private TMP_Text _downArrow;
        [SerializeField] private TMP_Text _nameText;

        // ReSharper disable once StringLiteralTypo
        [Space(15), Header("Settings")]
        [SerializeField] private string _alphabet = " ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public Action<string> OnSubmit;


        private int[] _value;
        private int _charIndex;
        private PlayerInput _playerInput;


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
            };
        }

        public void StartInput(int inputLength) {
            int[] defaultValue = new int[inputLength];
            for (int i = 0; i < defaultValue.Length; i++)
                defaultValue[i] = UnityEngine.Random.Range(0, _alphabet.Length);

            StartInput(defaultValue);
        }

        public void StartInput(int[] defaultValue) {
            _value = defaultValue;
            _playerInput.Enable();
            UpdateText();
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
            for (int i = 0; i < _value.Length; i++)
                _nameText.text += _alphabet[_value[i]];
        }

        private void UpdateArrows(float x) {
            _charIndex = x switch {
                > 0 => (_charIndex + 1) % _value.Length,
                < 0 => (_charIndex + (_value.Length -1)) % _value.Length,
                _ => _charIndex
            };

            string arrow = "";

            for (int i = 0; i < _value.Length; i++)
                arrow += i == _charIndex ? "v" : " ";

            _upArrow.text = arrow;
            _downArrow.text = arrow;
        }


        private void OnDisable() {
            _playerInput.Disable();
        }

    }

}