using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyMovement : MonoBehaviour
{
    private Enemy _enemy;

    [SerializeField] private float _walkingSpeed = 1;
    
    private enum WalkDirection
    {
        Left,
        Right,
        None
    }
    
    [SerializeField] private WalkDirection walkDirection = WalkDirection.Left;
    private bool currentlyWalking;

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
        
        //--------------------------------------------------------------------------------------------------------------
        
        Ray rayLeft = new Ray(playerLeft, new Vector3(0, -2, 0));
        RaycastHit hitLeft;
   
        Ray rayRight = new Ray(playerRight, new Vector3(0, -2, 0));
        RaycastHit hitRight;

        // if player is walking to left
        if (walkDirection == WalkDirection.Left)
        {
            // if ground is on left
            if (Physics.Raycast(playerLeft, new Vector3(0, -2, 0), out hitLeft, 2f))
            {
                if (hitLeft.collider.gameObject.layer == 9)
                {
                    // walk left
                    transform.Translate(Vector3.left * _walkingSpeed);
                    walkDirection = WalkDirection.Left;
                }
                
                // if there is no ground on left side
                else
                {
                    //walk right
                    transform.Translate(Vector3.right * _walkingSpeed);
                    walkDirection = WalkDirection.Right;
                }
            }
            // if there is no ground on left side
            else
            {
                // if there is ground on right side
                if (Physics.Raycast(playerRight, new Vector3(0, -2, 0), out hitLeft, 2f))
                {
                    //walk right
                    transform.Translate(Vector3.right * _walkingSpeed);
                    walkDirection = WalkDirection.Right;
                }
            }
        }
        
        // if player is walking to right
        else
        {
            //if ground is on right
            if (Physics.Raycast(playerRight, new Vector3(0, -2, 0), out hitLeft, 2f))
            {
                if (hitLeft.collider.gameObject.layer == 9)
                {
                    // walk right
                    transform.Translate(Vector3.right * _walkingSpeed);
                    walkDirection = WalkDirection.Right;
                }
            }
            
            // if there is no ground on right side
            else
            {
                // if there is ground on left side
                if (Physics.Raycast(playerLeft, new Vector3(0, -2, 0), out hitLeft, 2f))
                {
                    //walk left
                    transform.Translate(Vector3.left * _walkingSpeed);
                    walkDirection = WalkDirection.Left;
                }
            }
        }
    }
    
    // if this doesnt work Ill seriously cry
    
}
