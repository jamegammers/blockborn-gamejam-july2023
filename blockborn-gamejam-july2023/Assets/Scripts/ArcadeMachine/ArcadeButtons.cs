using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = System.Numerics.Vector2;

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
            foreach (ArcadeButton button in _buttons)
                RegisterEvents(button);
        }

        private static void RegisterEvents(ArcadeButton button) {
            switch (button.Type) {
                case ArcadeButtonType.Button:
                    button.InputAction.action.started += _ =>
                        button.Object.localPosition = new Vector3(0, -button.OffsetPressed, 0);
                
                    button.InputAction.action.canceled += _ =>
                        button.Object.localPosition = Vector3.zero;
                    
                    button.InputAction.action.Enable();
                    break;

                case ArcadeButtonType.Joystick:
                    button.InputAction.action.started += ctx => {
                        Vector2 dir = ctx.ReadValue<Vector2>();
                    
                        // z = x
                        // x = y
                    
                        button.Object.localRotation = Quaternion.Euler(0, 0, 0);
                    };
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
    }

}