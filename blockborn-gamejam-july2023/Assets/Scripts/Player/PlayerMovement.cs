using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    //[SerializeField] private Rigidbody _rbody;
    [SerializeField] private CharacterController _cController;
    [SerializeField, Range(0.1f, 5)] private float _playerSpeed = 0.3f;   
    [SerializeField, Range(0.1f, 20f)] private float _jumpHeight = 12f;
    private bool _grounded;
    private Vector2 _moveInput;
    private PlayerInput _playerInput;
    private bool _movementPressed;
    private int _currentAim = 0; //0..right, 1..up-right, 2..up,..., 7..down-right
    private int _checkedAim = 0;
    //private AimDirections _aimDirections;
    private bool _crouching;
    private Vector3 _weaponHolderPosition;

    private float _groundHeight;    

    //shooting variables
    [Header("Shooting")]
    [SerializeField, Range(0, 3f)] private float _shootCD = 1f;
    [SerializeField, Range(1f, 10f)] private float _bulletSpeed = 3f;
    private bool _currentlyShooting = false;
    [SerializeField] private GameObject _bullet;

    [SerializeField] private GameObject _weaponHolder;

    //transform variables
    [Header("Tranform")]
    [SerializeField, Range(0.1f, 5)] private float _playerCarSpeed = 0.6f;
    private bool _carMode = false;
    [SerializeField, Range(0, 3f)] private float _transformationSpeed = 0.5f;
    private bool _transforming;
    [SerializeField] private GameObject _robotSprite;
    [SerializeField] private GameObject _carSprite;

    private void Awake()
    {
        //Setting up the inputs
        _playerInput = new PlayerInput();

        _playerInput.Player.Move.performed += ctx => {  
            _moveInput = ctx.ReadValue<Vector2>();
            _movementPressed = _moveInput.x != 0 || _moveInput.y != 0;
            Crouch(false);
        };
        _playerInput.Player.Jump.performed += ctx => Jump();
        _playerInput.Player.Shoot.performed += ctx => Shoot();
        _playerInput.Player.Transform.performed += ctx => Transform();

        
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
        //if (Physics.Raycast(transform.position, Vector3.down, out _hit)) transform.position = new Vector3(transform.position.x, transform.position.y - _hit.distance, transform.position.z);
        if (Physics.Raycast(transform.position, Vector3.down, out _hit)) _groundHeight = _hit.distance;
        _weaponHolderPosition = _weaponHolder.transform.localPosition;
    }

    private void Update()
    {
        //do the moving when moving is pressed. look if carMode or not and not currently transforming
        if (_movementPressed && !_carMode && !_transforming)
        {
            Move();
            CheckAimAngle();
            if (_checkedAim != _currentAim) Aim();
        } else if (_movementPressed && _carMode && !_transforming)
        {
            CarMove();
            CheckAimAngle();
            if (_checkedAim != _currentAim) Aim();
        }


        if (!_grounded) CheckForGround();
        if (!_cController.isGrounded) _cController.Move(Vector3.down * 9.81f * Time.deltaTime);
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
                Crouch(false);
            }
            else //aiming left
            {
                _checkedAim = 4;
                Crouch(false);
            }
        } else if (_moveInput.x >= -0.5 && _moveInput.x < 0.5)
        {
            if (_moveInput.y >= 0) //aiming up
            {
                _checkedAim = 2;
                Crouch(false);
            }
            else //aiming down
            {
                _checkedAim = 6;
                Crouch(true);
            }
        }else if (_moveInput.x >= 0.5) 
        {
            if (_moveInput.y >= 0.5) //aiming up right
            {
                _checkedAim = 1;
                Crouch(false);
            }
            else if (_moveInput.y <= -0.5) //aiming down right
            {
                _checkedAim = 7;
                Crouch(true);
            }
        } else if (_moveInput.x <= -0.5)
        {
            if (_moveInput.y >= 0.5) //aiming up left
            {
                _checkedAim = 3;
                Crouch(false);
            }
            else if (_moveInput.y <= -0.5) //aiming down left
            {
                _checkedAim = 5;
                Crouch(true);
            }
        }

        //_aimDirections = (AimDirections)_checkedAim;
        //Debug.Log("Aim: " + _aimDirections);
    }

    //placeholder until i do the shooting
    private void Aim()
    { 
        _currentAim = _checkedAim;
        switch (_currentAim)
        {
            case 0: 
                _weaponHolder.transform.localRotation = Quaternion.Euler(new Vector3( 0, 0, -90));
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
                if (!_grounded)
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
        //old
        //if (_moveInput.x > 0.3) transform.position = transform.position + new Vector3(1 * Time.deltaTime * _playerSpeed * 10, 0, 0);
        //else if (_moveInput.x < -0.3) transform.position = transform.position + new Vector3(-1 * Time.deltaTime * _playerSpeed * 10, 0, 0);
        //goes through walls, when to fast
        //if (_moveInput.x > 0.3) _rbody.MovePosition(transform.position + new Vector3(1 * Time.deltaTime * _playerSpeed * 100, 0, 0));
        //else if (_moveInput.x < -0.3) _rbody.MovePosition(transform.position + new Vector3(-1 * Time.deltaTime * _playerSpeed * 100, 0, 0));
        if (_moveInput.x > 0.3) _cController.Move(new Vector3(1 * Time.deltaTime * _playerSpeed * 50, 0, 0));
        else if (_moveInput.x < -0.3) _cController.Move(new Vector3(-1 * Time.deltaTime * _playerSpeed * 50, 0, 0));
        Crouch(false);
    }

    private void CarMove()
    {
        _moveInput = _playerInput.Player.Move.ReadValue<Vector2>();
        if (_moveInput.x > 0.3) _cController.Move(new Vector3(1 * Time.deltaTime * _playerCarSpeed * 50, 0, 0));
        else if (_moveInput.x < -0.3) _cController.Move(new Vector3(-1 * Time.deltaTime * _playerCarSpeed * 50, 0, 0));
    }

    private void Jump()
    {
        RaycastHit hit;Debug.Log("jump");
        Debug.DrawRay(transform.position, Vector3.down, Color.blue, _groundHeight);
        if (Physics.Raycast(transform.position, Vector3.down, out hit, _groundHeight))
        {
            //check if on platform and crouching to pass throuhg
            if (_currentAim == 10 && hit.transform.gameObject.layer == 11)
            {
                hit.transform.gameObject.GetComponentInChildren<PlatformEffector>().DisablePlatform();
            } else if (_carMode && hit.transform.gameObject.layer == 11 && (_currentAim == 10 || _currentAim ==5 || _currentAim ==7)) //check same, but for car and down left or down right
            {
                hit.transform.gameObject.GetComponentInChildren<PlatformEffector>().DisablePlatform();
            } else //jump normaly
            {
                //_rbody.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
                _cController.Move(Vector3.up * Time.deltaTime * _jumpHeight * 100);
                
                Crouch(false);
                StartCoroutine(ChangeGroundedAfterSeconds(0.1f));
            }
            
        }
        
    }

    private void Shoot()
    {
        if (!_currentlyShooting && !_carMode)
        {
            _currentlyShooting = true;
            StartCoroutine(ShootBullet());
        }
    }

    private void Crouch(bool crouch)
    {
        if (crouch != _crouching)
        {
            if (crouch && _currentAim == 10)
            {
                _weaponHolder.transform.localPosition = new Vector3(_weaponHolderPosition.x, _weaponHolderPosition.y - 0.5f, _weaponHolderPosition.z);
                _weaponHolder.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
            }
            else _weaponHolder.transform.localPosition = _weaponHolderPosition;
            _crouching = crouch;
        }
        
    }

    private IEnumerator ShootBullet()
    {
        GameObject _newBullet = Instantiate(_bullet, _weaponHolder.transform.position, Quaternion.Euler(Vector3.zero));
        Rigidbody _bulletRb = _newBullet.GetComponent<Rigidbody>();
        Vector3 direction = new Vector3();
        switch (_currentAim)
        {
            case 0: case 10: //shoot right
                direction = Vector3.right;
                break;
            case 1: //shoot up right
                direction = new Vector3(0.71f, 0.71f, 0);
                break;
            case 2: //shoot up
                direction = Vector3.up;
                break;
            case 3: //shoot up left
                direction = new Vector3(-0.71f, 0.71f, 0);
                break;
            case 4: //shoot left
                direction = Vector3.left;
                break;
            case 5: //shoot down left
                direction = new Vector3(-0.71f, -0.71f, 0);
                break;
            case 6: //shoot down
                direction = Vector3.down;
                break;
            case 7: //shoot down right
                direction = new Vector3(0.71f, -0.71f, 0);
                break;
        }
        _bulletRb.AddForce(direction * 10 * _bulletSpeed, ForceMode.Impulse);
        yield return new WaitForSeconds(_shootCD);
        _currentlyShooting = false;
    }

    //used to transform
    private void Transform()
    {
        if (!_transforming)
        {
            StartCoroutine(TransformCar());
            Crouch(false);
        }
    }

    private IEnumerator TransformCar()
    {
        _transforming = true;
        _carMode = !_carMode;
        _weaponHolder.SetActive(!_carMode);
        yield return new WaitForSeconds(_transformationSpeed);
        _carSprite.SetActive(_carMode);
        _robotSprite.SetActive(!_carMode);
        _transforming = false;
    }

    private void CheckForGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, _groundHeight))
        {
            _grounded = true;
            _checkedAim = 0;
            Aim();
        }
    }

    private IEnumerator ChangeGroundedAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        _grounded = false;
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


