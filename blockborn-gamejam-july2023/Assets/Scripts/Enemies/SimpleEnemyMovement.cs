using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyMovement : MonoBehaviour
{
    private Enemy _enemy;

    private float _walkingSpeed;
    private Enum _walkDirection;
    private enum WalkDirection
    {
        Left,
        Right,
        None
    }

    private void Awake()
    {
        _enemy = GetComponent<EnemyLoop>().GetEnemy();
        _walkingSpeed = _enemy.walkSpeed;
    }

    private void FixedUpdate()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3 playerLeft = new Vector3(transform.position.x - 0.75f, transform.position.y, transform.position.z);
        Vector3 playerRight = new Vector3(transform.position.x + 0.75f, transform.position.y, transform.position.z);
          
        Debug.DrawRay(playerLeft, new Vector3(0, -2, 0), Color.red);
        Debug.DrawRay(playerRight, new Vector3(0, -2, 0), Color.red);
        
        // check if there is a tile below the player
        Ray rayLeft = new Ray(playerLeft, new Vector3(0, -2, 0));
        RaycastHit hitLeft;
       
        if(Physics.Raycast(rayLeft, out hitLeft, 2f))
        {
            if (hitLeft.collider.gameObject.layer == 9)
            {
                Debug.Log("There is ground on the left side!");
            }
        }
       
        Ray rayRight = new Ray(playerRight, new Vector3(0, -2, 0));
        RaycastHit hitRight;
         
        if(Physics.Raycast(rayRight, out hitRight, 2f))
        {
            if (hitRight.collider.gameObject.layer == 9)
            {
                Debug.Log("There is ground on the right side!");
            }
        }
    }
}
