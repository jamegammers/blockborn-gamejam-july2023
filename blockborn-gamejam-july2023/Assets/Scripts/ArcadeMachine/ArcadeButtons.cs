using System;
using Audio;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ArcadeMachine {

    public enum ArcadeButtonType { Button, Joystick }

    [Serializable]
    public class ArcadeButton {
        
        // references
        // ReSharper disable once NotAccessedField.Local
        [SerializeField] private string _name;
        [FoldoutGroup("Setup"), SerializeField] private Transform _object;
        [FoldoutGroup("Setup"), SerializeField, EnumToggleButtons] private ArcadeButtonType _type;
        [FoldoutGroup("Setup"), SerializeField] private InputActionReference _inputAction;
        
        // settings
        [FoldoutGroup("Settings"), SerializeField, ShowIf("_type", ArcadeButtonType.Button)]
        private float _offsetPressed;
        [FoldoutGroup("Settings"), SerializeField, ShowIf("_type", ArcadeButtonType.Joystick)]
        private float _rotationOffsetPressed;

        // audio
        [FoldoutGroup("Audio"), SerializeField] private AudioSample _audioDown;
        [FoldoutGroup("Audio"), SerializeField] private AudioSample _audioUp;

        // property getters
        public Transform Object => _object;
        public ArcadeButtonType Type => _type;
        public InputActionReference InputAction => _inputAction;
        public float OffsetPressed => _offsetPressed;
        public float RotationOffsetPressed => _rotationOffsetPressed;
        public AudioSample AudioDown => _audioDown;
        public AudioSample AudioUp => _audioUp;

        // ReSharper disable once InconsistentNaming
        [HideInInspector] public Vector2 currentDirection;

    }

    public class ArcadeButtons : MonoBehaviour {

        [SerializeField] private ArcadeButton[] _buttons;

        private void Awake() {
            // register events for all buttons in the list
            foreach (ArcadeButton button in _buttons) {
                if (button.Type == ArcadeButtonType.Button)
                    RegisterButton(button);

                button.InputAction.action.Enable();
            }
        }

        private static void RegisterButton(ArcadeButton button) {
            button.InputAction.action.started += _ => {
                button.Object.localPosition = new Vector3(0, -button.OffsetPressed, 0);
                AudioManager.PlayAudio(button.AudioDown, button.Object.position);
            };

            button.InputAction.action.canceled += _ => {
                button.Object.localPosition = Vector3.zero;
                AudioManager.PlayAudio(button.AudioUp, button.Object.position);
            };
        }

        private void Update() {
            foreach (ArcadeButton button in _buttons) {
                if (button.Type == ArcadeButtonType.Button) continue;

                Vector2 inputDir = button.InputAction.action.ReadValue<Vector2>();
                Vector2 joystickRotation = new (
                    button.RotationOffsetPressed * inputDir.x,
                    button.RotationOffsetPressed * inputDir.y
                );

                Debug.Log($"button dir: {button.currentDirection}");
                if (
                    (button.currentDirection.x == 0 && inputDir.x != 0) ||
                    (button.currentDirection.y == 0 && inputDir.y != 0)
                )
                    AudioManager.PlayAudio(button.AudioDown, button.Object.position);

                else if (
                    (button.currentDirection.x != 0 && inputDir.x == 0) ||
                    (button.currentDirection.y != 0 && inputDir.y == 0)
                )
                    AudioManager.PlayAudio(button.AudioUp, button.Object.position);

                button.Object.localRotation = Quaternion.Euler(-joystickRotation.y, 0, joystickRotation.x);
                button.currentDirection = inputDir;
            }
        }

        private void OnDisable() {
            foreach (ArcadeButton button in _buttons)
                button.InputAction.action.Disable();
        }
    }

}