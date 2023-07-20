using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject _playerCamera;

    private void Update()
    {
        if (transform.position.x > _playerCamera.transform.position.x) _playerCamera.transform.position = new Vector3(transform.transform.position.x, _playerCamera.transform.position.y, _playerCamera.transform.position.z);
    }
}
