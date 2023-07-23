using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health = 3;
    [SerializeField] private PlayerAnimation _playerAnimation;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerShoot _playerShoot;
    private bool _gettingHit = false;
    private bool _alive = true;

    public void GetDamage(int damage)
    {
        if (!_gettingHit && _alive == true) StartCoroutine(Damage(damage));
        if (_health <= 0 && _alive == true) GameOver();
    }

    private IEnumerator Damage(int damage)
    {
        Debug.Log("player get damage");
        _gettingHit = true;
        _health -= damage;
        if (_health <= 0 && _alive == true) GameOver();
        else if (_health > 0) _playerAnimation.PlayDamageAnimation();
        yield return new WaitForSeconds(1f);
        _gettingHit = false;
    }

    private void GameOver()
    {
        _alive = false;
        Debug.Log("game over :(");
        _playerAnimation.PlayDeathAnimation();
        _playerMovement.enabled = false;
        _playerShoot.enabled = false;
    }
}
