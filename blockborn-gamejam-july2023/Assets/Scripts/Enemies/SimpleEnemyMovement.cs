using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyMovement : MonoBehaviour
{
    private Enemy _enemy;

    [SerializeField, Range(0, 0.2f)] private float _walkingSpeed = 0.05f;
    
    private enum WalkDirection
    {
        Left,
        Right,
        None
    }
    
    [SerializeField] private float _raycastDistance = 2f;
    [SerializeField] private WalkDirection walkDirection = WalkDirection.Left;
    [HideInInspector] public bool isShooting = false;

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

        //--------------------------------------------------------------------------------------------------------------
        
        //Ray rayLeft = new Ray(playerLeft, new Vector3(0, -_raycastDistance, 0));
        RaycastHit raycastHit;
   
        //Ray rayRight = new Ray(playerRight, new Vector3(0, -_raycastDistance, 0));
        RaycastHit hitRight;

        if (!isShooting)
        {
            // if player is walking to left
            if (walkDirection == WalkDirection.Left)
            {
                // if ground is on left
                if (Physics.Raycast(playerLeft, new Vector3(0, -1, 0), out raycastHit, _raycastDistance))
                {
                    if (raycastHit.collider.gameObject.layer == 9)
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
                    if (Physics.Raycast(playerRight, new Vector3(0, -1, 0), out raycastHit, _raycastDistance))
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
                if (Physics.Raycast(playerRight, new Vector3(0, -1, 0), out raycastHit, _raycastDistance))
                {
                    if (raycastHit.collider.gameObject.layer == 9)
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
                    if (Physics.Raycast(playerLeft, new Vector3(0, -1, 0), out raycastHit, _raycastDistance))
                    {
                        //walk left
                        transform.Translate(Vector3.left * _walkingSpeed);
                        walkDirection = WalkDirection.Left;
                    }
                }
            }
        }
        
        Debug.DrawRay(playerLeft, new Vector3(0, -1 * _raycastDistance, 0), Color.red);
        Debug.DrawRay(playerRight, new Vector3(0, -1 * _raycastDistance, 0), Color.red);
        
    }
        
        
    
    // if this doesnt work Ill seriously cry
    
    // it works so I dont need to cry 
    //
}
