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
                    button.InputAction.action.performed += ctx => {
                        Vector2 dir = ctx.ReadValue<Vector2>();
                        // Debug.Log($"input performed: {dir}");
                        
                        float x = 0, y = 0;
                        
                        if (dir.x > 0) x = button.RotationOffsetPressed;
                        else if (dir.x < 0) x = -button.RotationOffsetPressed;
                        
                        if (dir.y > 0) y = button.RotationOffsetPressed;
                        else if (dir.y < 0) y = -button.RotationOffsetPressed;
                    
                        // z = x; x = y
                        button.Object.localRotation = Quaternion.Euler(y, 0, -x);
                    };
                    
                    button.InputAction.action.canceled += _ =>
                        button.Object.localRotation = Quaternion.identity;
                    
                    button.InputAction.action.Enable();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnDisable() {
            foreach (ArcadeButton button in _buttons)
                button.InputAction.action.Disable();
        }
    }

}