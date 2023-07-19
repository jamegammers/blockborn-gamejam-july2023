using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ArcadeMachine {

    public enum ArcadeButtonType { Button, Joystick }

    [Serializable]
    public struct ArcadeButton {
        
        // references
        [SerializeField] private string _name;
        [SerializeField] private Transform _object;
        [SerializeField, EnumToggleButtons] private ArcadeButtonType _type;
        [SerializeField] private InputActionReference _inputAction;
        
        // settings
        [SerializeField, ShowIf("_type", ArcadeButtonType.Button)] private float _offsetPressed;
        [SerializeField, ShowIf("_type", ArcadeButtonType.Joystick)] private float _rotationOffsetPressed;
        
        // property getters
        public Transform Object => _object;
        public ArcadeButtonType Type => _type;
        public InputActionReference InputAction => _inputAction;
        public float OffsetPressed => _offsetPressed;
        public float RotationOffsetPressed => _rotationOffsetPressed;
        
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
            button.InputAction.action.started += _ =>
                button.Object.localPosition = new Vector3(0, -button.OffsetPressed, 0);
                
            button.InputAction.action.canceled += _ =>
                button.Object.localPosition = Vector3.zero;
        }

        private void Update() {
            foreach (ArcadeButton button in _buttons) {
                if (button.Type == ArcadeButtonType.Button) continue;

                Vector2 inputDir = button.InputAction.action.ReadValue<Vector2>();
                Vector2 joystickRotation = new (
                    button.RotationOffsetPressed * inputDir.x,
                    button.RotationOffsetPressed * inputDir.y
                );

                button.Object.localRotation = Quaternion.Euler(joystickRotation.y, 0, -joystickRotation.x);
            }
        }

        private void OnDisable() {
            foreach (ArcadeButton button in _buttons)
                button.InputAction.action.Disable();
        }
    }

}