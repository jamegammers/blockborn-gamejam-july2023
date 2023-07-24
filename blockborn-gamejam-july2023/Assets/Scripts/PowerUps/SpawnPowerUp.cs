using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnPowerUp : MonoBehaviour
{
    [SerializeField] private GameObject _gameManager;
    private PowerUpManager _powerUpManager;

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager");
        _powerUpManager = _gameManager.GetComponent<PowerUpManager>();
        _powerUpManager.InstanciatePowerUp(transform.position, transform.gameObject);
    }

}
