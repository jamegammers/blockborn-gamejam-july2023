using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rbody;
    [SerializeField, Range(0.1f, 5)] private float _playerSpeed = 0.3f;
    [SerializeField, Range(0.1f, 20f)] private float _jumpHeight = 12f;
    private Vector2 _moveInput;
    private PlayerInput _playerInput;
    private bool _movementPressed;
    private int _currentAim = 0; //0..right, 1..up-right, 2..up,..., 7..down-right
    private int _checkedAim = 0;
    private AimDirections _aimDirections;
    private bool _crouching;

    private float _groundHeight;

    [SerializeField] private GameObject _weaponHolder;

    private void Awake()
    {
        //Setting up the inputs
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

    private void Start()
    {
        //Raycast for later ground detection
        RaycastHit _hit;
        if (Physics.Raycast(transform.position, Vector3.down, out _hit)) _groundHeight = _hit.distance;
    }

    private void Update()
    {
        //do the moving when moving is pressed
        if (_movementPressed)
        {
            Move();
            CheckAimAngle();
            if (_checkedAim != _currentAim) Aim();
        }
    }

    //Checks in which direction the joystick is facing
    private void CheckAimAngle()
    {
        //Debug.Log("Input:" + _moveInput);
        if (_moveInput.y >= -0.5 && _moveInput.y < 0.5)
        {
            if (_moveInput.x >= 0) //aiming right
            {
                _checkedAim = 0;
                _crouching = false;
            }
            else //aiming left
            {
                _checkedAim = 4;
                _crouching = false;
            }
        } else if (_moveInput.x >= -0.5 && _moveInput.x < 0.5)
        {
            if (_moveInput.y >= 0) //aiming up
            {
                _checkedAim = 2;
                _crouching = false;
            }
            else //aiming down
            {
                _checkedAim = 6;
                _crouching = true;
            }
        }else if (_moveInput.x >= 0.5) 
        {
            if (_moveInput.y >= 0.5) //aiming up right
            {
                _checkedAim = 1;
                _crouching = false;
            }
            else if (_moveInput.y <= -0.5) //aiming down right
            {
                _checkedAim = 7;
                _crouching = true;
            }
        } else if (_moveInput.x <= -0.5)
        {
            if (_moveInput.y >= 0.5) //aiming up left
            {
                _checkedAim = 3;
                _crouching = false;
            }
            else if (_moveInput.y <= -0.5) //aiming down left
            {
                _checkedAim = 5;
                _crouching = true;
            }
        }

        _aimDirections = (AimDirections)_checkedAim;
        //Debug.Log("Aim: " + _aimDirections);
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
                if (!Physics.Raycast(transform.position, Vector3.down, _groundHeight))
                {
                    _weaponHolder.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                } else
                {
                    //crouch and aim forward
                    _currentAim = 10;
                }
                break;
            case 7:
                _weaponHolder.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 225));
                break;
        }
       
        
    }

    private void Move()
    {
        _moveInput = _playerInput.Player.Move.ReadValue<Vector2>();
        //check for crouching and grounded before moving
        if (!_crouching) transform.position = transform.position + new Vector3(_moveInput.x * Time.deltaTime * _playerSpeed * 10, 0, 0); 
        else if (_crouching && !Physics.Raycast(transform.position, Vector3.down, _groundHeight)) transform.position = transform.position + new Vector3(_moveInput.x * Time.deltaTime * _playerSpeed * 10, 0, 0);
        }

    private void Jump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, _groundHeight))
        {
            //_rbody.velocity += _jumpHeight * Vector3.up;
            _rbody.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
            _crouching = false;
        }
        
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


