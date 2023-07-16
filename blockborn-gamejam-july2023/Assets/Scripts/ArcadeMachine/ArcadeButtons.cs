using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ArcadeMachine {

    public enum ArcadeButtonType { Joystick, Button }

    [Serializable]
    public struct ArcadeButton {
        
        [SerializeField] private Transform _object;
        [SerializeField] private ArcadeButtonType _type;
        [SerializeField] private InputActionReference _inputAction;
        
        public Transform Object => _object;
        public ArcadeButtonType Type => _type;
        
    }

    public class ArcadeButtons : MonoBehaviour {

        [SerializeField] private ArcadeButton[] _buttons;

        private void Awake() {
            foreach (ArcadeButton button in _buttons)
                RegisterEvents(button);
        }

        private void RegisterEvents(ArcadeButton button) {
            
        }
        
    }

}