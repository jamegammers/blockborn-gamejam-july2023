using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _playerSprite;

    private int _walkHash = Animator.StringToHash("walking");
    private int _jumpHash = Animator.StringToHash("jump");
    private int _midairHash = Animator.StringToHash("midair");
    private int _deathHash = Animator.StringToHash("Death");
    private int _attackHash = Animator.StringToHash("Attack");
    private int _attackIndexHash = Animator.StringToHash("AttackIndex");
    private int _attackHoldHash = Animator.StringToHash("AttackHold");
    private int _damageHash = Animator.StringToHash("Damage");
    private int _fallingHash = Animator.StringToHash("Falling");
    private int _crouchHash = Animator.StringToHash("Crouch");
    private int _carHash = Animator.StringToHash("Car");
    private int _carTransformHash = Animator.StringToHash("CarTransform");
    private int _transformingHash = Animator.StringToHash("Transforming");
    private int _resetHash = Animator.StringToHash("Reset");

    private Vector3 _startScale;
    private bool _facingRight;


    private void Awake()
    {
        _startScale = _playerSprite.transform.localScale;
    }

    public void SetFacingDirection(bool facingRight)
    {
        if (facingRight && facingRight != _facingRight)
        {
            _playerSprite.transform.localScale = _startScale;
            _facingRight = true;
        }
        else if (!facingRight && facingRight != _facingRight)
        {
            _playerSprite.transform.localScale = new Vector3(-_startScale.x, _startScale.y, _startScale.z);
            _facingRight = false;
        }
    }

    public void SetWalkAnimation(bool walk)
    {
        _animator.SetBool(_walkHash, walk);
    }

    public void PlayJumpAnimation()
    {
        _animator.SetTrigger(_jumpHash);
        StartCoroutine(ResetTriggerAfter(_jumpHash, 1f));
    }

    public void SetMidAirAnimation(bool midair)
    {
        _animator.SetBool(_midairHash, midair);
    }

    public void PlayDeathAnimation()
    {
        _animator.SetBool(_deathHash, true);
        //_animator.enabled = false;
    }

    public void PlayDamageAnimation()
    {
        _animator.SetTrigger(_damageHash);
        StartCoroutine(ResetTriggerAfter(_damageHash, 1f));
    }

    public void PlayFallingAnimation()
    {
        _animator.SetTrigger(_fallingHash);
    }

    public void PlayAttackAnimation(AimDirections direction)
    {
        _animator.SetInteger(_attackIndexHash, (int)direction);
        _animator.SetTrigger(_attackHash);
        StartCoroutine(ResetTriggerAfter(_attackHash, 1f));
    }

    public void SetAttackHoldAnimation(bool hold)
    {
        _animator.SetBool(_attackHoldHash, hold);
    }

    public void SetCrouchAnimation(bool crouch)
    {
        _animator.SetBool(_crouchHash, crouch);
    }

    public void SetCarTransformAnimation(bool isCar)
    {
        _animator.SetBool(_carHash, isCar);
        _animator.SetTrigger(_carTransformHash);
    }

    public void ResetAnimation()
    {
        //_animator.enabled = true;
        _animator.SetBool(_deathHash, false);
        _animator.SetTrigger(_resetHash);
    }

    public void SetTransformingAnimation(bool transforming)
    {
        _animator.SetBool(_transformingHash, transforming);
    }

    private IEnumerator ResetTriggerAfter(int hash, float sec)
    {
        yield return new WaitForSeconds(sec);
        _animator.ResetTrigger(hash);
    }
}