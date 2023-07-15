using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] private Rigidbody _rbody;
    [SerializeField, Range(0.1f, 5)] private float _playerSpeed = 2.0f;
    private Vector2 _moveInput;
    private PlayerInput _playerInput;
    private bool _movementPressed;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.Player.Move.performed += ctx => {  
            _moveInput = ctx.ReadValue<Vector2>();
            _movementPressed = _moveInput.x != 0 || _moveInput.y != 0;
        };
        _playerInput.Player.Jump.performed += ctx => Jump();
    }

    private void OnEnable()
    {
        _playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Disable();
    }

    private void Update()
    {
        if (_movementPressed) Move();
    }

    private void Move()
    {
        _moveInput = _playerInput.Player.Move.ReadValue<Vector2>();
        transform.position = transform.position + new Vector3(_moveInput.x * Time.deltaTime * _playerSpeed * 10, 0, 0);
    }

    private void Jump()
    {
        Debug.Log("Jump");
    }

}
