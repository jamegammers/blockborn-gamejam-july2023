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
    private int _currentAim = 0; //0..right, 1..up-right, 2..up,..., 7..down-right
    private int _checkedAim = 0;
    private AimDirections _aimDirections;

    [SerializeField] private GameObject _weaponHolder;

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
        if (_movementPressed)
        {
            Move();
            CheckAimAngle();
            if (_checkedAim != _currentAim) Aim();
        }
    }

    private void CheckAimAngle()
    {
        //Debug.Log("Input:" + _moveInput);
        if (_moveInput.y >= -0.5 && _moveInput.y < 0.5)
        {
            if (_moveInput.x >= 0) _checkedAim = 0;
            else _checkedAim = 4;
        } else if (_moveInput.x >= -0.5 && _moveInput.x < 0.5)
        {
            if (_moveInput.y >= 0) _checkedAim = 2;
            else _checkedAim = 6;
        }else if (_moveInput.x >= 0.5)
        {
            if (_moveInput.y >= 0.5) _checkedAim = 1;
            else if (_moveInput.y <= -0.5) _checkedAim = 7;
        } else if (_moveInput.x <= -0.5)
        {
            if (_moveInput.y >= 0.5) _checkedAim = 3;
            else if (_moveInput.y <= -0.5) _checkedAim = 5;
        }

        _aimDirections = (AimDirections)_checkedAim;
        Debug.Log("Aim: " + _aimDirections);
    }

    //placeholder until i do the shooting
    private void Aim()
    { 
        _currentAim = _checkedAim;
        switch (_currentAim)
        {
            case 0: _weaponHolder.transform.localRotation = Quaternion.Euler(new Vector3( 0, 0, -90));
                break;
            case 1:
                _weaponHolder.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -45));
                break;
            case 2:
                _weaponHolder.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
            case 3:
                _weaponHolder.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 45));
                break;
            case 4:
                _weaponHolder.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                break;
            case 5:
                _weaponHolder.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 135));
                break;
            case 6:
                _weaponHolder.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                break;
            case 7:
                _weaponHolder.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 225));
                break;
        }
       
        
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
    private enum AimDirections
    {
        right = 0,
        upRight = 1,
        up = 2,
        upLeft = 3,
        left = 4,
        downLeft = 5,
        down = 6,
        downRight = 7,
    }
}


