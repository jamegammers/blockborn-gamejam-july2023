using System.Collections;
using UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health = 3;
    [SerializeField] private PlayerAnimation _playerAnimation;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerShoot _playerShoot;
    [SerializeField] private HealthUI _healthUI;
    [SerializeField] private DeathScreen _deathScreen;
    [SerializeField] private float _deathY = -10f;

    private bool _gettingHit;
    private bool _alive = true;
    private Vector3 _startPosition;
    private int _initialHealth;


    private void Start() {
        _startPosition = transform.position;
        _initialHealth = _health;
    }

    private void Update() {
        if (transform.position.y < _deathY && _alive)
            GameOver();
    }

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
        _healthUI.SetHealth(_health);
        if (_health <= 0 && _alive == true) GameOver();
        else if (_health > 0) _playerAnimation.PlayDamageAnimation();
        yield return new WaitForSeconds(1f);
        _gettingHit = false;
    }

    private void GameOver()
    {
        _alive = false;
        // Debug.Log("game over :(");
        _playerAnimation.PlayDeathAnimation();
        _playerMovement.enabled = false;
        _playerShoot.enabled = false;

        int score = (int) (_startPosition.x + transform.position.x);
        _deathScreen.Show(score);
    }

    public void Resurrect() {
        _alive = true;
        _health = _initialHealth;
    }

}
