using UI;
using UnityEngine;

namespace Player {

    public class PlayerCheckpoint : MonoBehaviour {

        [SerializeField] private DeathScreen _deathScreen;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerShoot _playerShoot;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private Transform _cameraTransform;

        private Vector3 _lastCheckpoint;


        private void Start() {
            _deathScreen.OnContinue += OnContinue;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.layer != LayerMask.NameToLayer("Border"))
                return;

            _lastCheckpoint = other.transform.position;
            // Debug.Log($"Checkpoint: {_lastCheckpoint}");
        }

        private void OnContinue() {
            _characterController.enabled = false;
            transform.position = _lastCheckpoint;
            _characterController.enabled = true;

            _cameraTransform.position =
                new Vector3(_lastCheckpoint.x, _cameraTransform.position.y, _cameraTransform.position.z);

            _playerHealth.Resurrect();
            _playerMovement.enabled = true;
            _playerShoot.enabled = true;

            // Debug.Log($"Player respawned at {_lastCheckpoint}");
        }
    }

}