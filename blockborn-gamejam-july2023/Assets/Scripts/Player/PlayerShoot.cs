using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //shooting variables
    [Header("Shooting")]
    [SerializeField, Range(0, 3f)] private float _shootCD = 1f;
    [SerializeField, Range(1f, 10f)] private float _bulletSpeed = 3f;
    private bool _currentlyShooting = false;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private PlayerMovement _playerMovement;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.Player.Shoot.performed += ctx => Shoot();
    }

    private void OnEnable()
    {
        _playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Disable();
    }

    private void Shoot()
    {
        if (!_currentlyShooting && !_playerMovement._carMode)
        {
            _currentlyShooting = true;
            StartCoroutine(ShootBullet());
        }
    }

    private IEnumerator ShootBullet()
    {
        //GameObject _newBullet = Instantiate(_bullet, _weaponHolder.transform.position, Quaternion.Euler(Vector3.zero));
        //Rigidbody _bulletRb = _newBullet.GetComponent<Rigidbody>();
        Debug.Log("shoot");
        Vector3 direction = new Vector3();
        switch (_playerMovement._currentAim)
        {
            case 0:
            case 10: //shoot right
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
        //_bulletRb.AddForce(direction * 10 * _bulletSpeed, ForceMode.Impulse);
        yield return new WaitForSeconds(_shootCD);
        _currentlyShooting = false;
    }
}
