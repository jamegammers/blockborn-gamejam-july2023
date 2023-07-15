using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rbody;
    //private Vector3 _playerVelocity;
    //private bool _groundedPlayer;
    [SerializeField, Range(0.1f, 5)] private float _playerSpeed = 2.0f;
    //[SerializeField, Range(1, 10)] private float _jumpHeight = 1.0f;
    //private float _gravityValue = -9.81f;
    private Vector2 _moveInput;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.Player.Move.performed += ctx => { Move(); _playerInput = va};
    }

    private void OnEnable()
    {
        _playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Disable();
    }

 
    private void Move()
    {
        Debug.Log("Input: " + )
    }

}
