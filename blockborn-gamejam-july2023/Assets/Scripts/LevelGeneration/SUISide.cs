using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUISide : MonoBehaviour
{
    private GameObject _player;
    private float _destroyDistance = 200f;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");

    }

    private void Update()
    {
        CheckDistanceToPlayer();
    }

    private void CheckDistanceToPlayer()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) > _destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
