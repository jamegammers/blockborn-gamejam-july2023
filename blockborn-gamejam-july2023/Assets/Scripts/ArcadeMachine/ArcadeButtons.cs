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
            foreach (ArcadeButton button in _buttons)
                switch (button.Type) {
                    case ArcadeButtonType.Button:
                        RegisterButton(button);
                        break;
                    case ArcadeButtonType.Joystick:
                        RegisterJoystick(button);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
        }

        private static void RegisterButton(ArcadeButton button) {
            button.InputAction.action.started += _ =>
                button.Object.localPosition = new Vector3(0, -button.OffsetPressed, 0);
                
            button.InputAction.action.canceled += _ =>
                button.Object.localPosition = Vector3.zero;
                    
            button.InputAction.action.Enable();
        }

        private static void RegisterJoystick(ArcadeButton button) {
            button.InputAction.action.started += ctx => {
                Vector2 inputDir = ctx.ReadValue<Vector2>();
                Vector2 joystickRotation = new (
                    button.RotationOffsetPressed * inputDir.x,
                    button.RotationOffsetPressed * inputDir.y
                );

                // z = x; x = y
                button.Object.localRotation = Quaternion.Euler(joystickRotation.y, 0, -joystickRotation.x);
            };
            
            button.InputAction.action.canceled += _ =>
                button.Object.localRotation = Quaternion.identity;
                    
            button.InputAction.action.Enable();
        }

        private void OnDisable() {
            foreach (ArcadeButton button in _buttons)
                button.InputAction.action.Disable();
        }
    }

}