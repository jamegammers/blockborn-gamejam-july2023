using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEffector : MonoBehaviour
{

    [SerializeField] private Collider _collider;
    public LayerMask _playerLayer;
    public LayerMask _noPlayerLayer;

    private void OnTriggerEnter(Collider other)
    {
        _collider.excludeLayers = _playerLayer;
    }

    private void OnTriggerExit(Collider other)
    {
        //_collider.enabled = false;
        _collider.excludeLayers = _noPlayerLayer;
    }

    public void DisablePlatform()
    {
        _collider.excludeLayers = _noPlayerLayer;
    }
}
