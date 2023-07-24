using BulletHell;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class PlayerShoot : MonoBehaviour
{
    //shooting variables
    [Header("Shooting")]
    [SerializeField, Range(0, 3f)] private float _shootCD = 1f;
    private bool _currentlyShooting = false;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAnimation _playerAnimation;

    private PlayerInput _playerInput;
    private bool _shoot = false;

    [Header("Sounds")]
    [SerializeField] private AudioSample _shootSound;

    public float getShootCD()
    {
        return _shootCD;
    }
    
    public void setShootCD(float newCD)
    {
        _shootCD = newCD;
    }
    
    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.Player.Shoot.performed += ctx =>
        {
            _shoot = !_shoot;
            _playerAnimation.SetAttackHoldAnimation(_shoot);
        };
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
        if (_shoot) Shoot();
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
        Vector3 direction = new Vector3();
        switch (_playerMovement._currentAim)
        {
            case 0:
            case 10://shoot right
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
        GameObject bul = BulletPool.Instance.GetBulletPlayer();
        bul.transform.position = _playerMovement._weaponHolder.transform.position;
        bul.transform.rotation = transform.rotation;
        bul.SetActive(true);
        bul.GetComponent<Bullet>().SetDirection(direction);

        _playerAnimation.PlayAttackAnimation((AimDirections)_playerMovement._currentAim);
        ArcadeAudio.PlayAudio(_shootSound);
        yield return new WaitForSeconds(_shootCD);
        _currentlyShooting = false;
    }
}
