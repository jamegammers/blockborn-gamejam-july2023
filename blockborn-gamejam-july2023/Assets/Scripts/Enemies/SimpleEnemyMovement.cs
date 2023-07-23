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

    [SerializeField] private GameObject playerLeft;
    [SerializeField] private GameObject playerRight;

    [SerializeField] private PlayerAnimation _playerAnimation;

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
        
        
        
        //Vector3 playerLeft = new Vector3(transform.position.x - 0.75f, transform.position.y, transform.position.z);
        //Vector3 playerRight = new Vector3(transform.position.x + 0.75f, transform.position.y, transform.position.z);

        //--------------------------------------------------------------------------------------------------------------
        
        //Ray rayLeft = new Ray(playerLeft, new Vector3(0, -_raycastDistance, 0));
        RaycastHit raycastHit;
   
        //Ray rayRight = new Ray(playerRight, new Vector3(0, -_raycastDistance, 0));
        RaycastHit hitRight;

        if (!isShooting)
        {
            _playerAnimation.SetWalkAnimation(true);
            CheckForGroundOnSide();
            // if player is walking to left
            if (walkDirection == WalkDirection.Left)
            {
                // if ground is on left
                if (Physics.Raycast(playerLeft.transform.position, new Vector3(0, -1, 0), out raycastHit, _raycastDistance))
                {
                    if (raycastHit.collider.gameObject.layer == 9)
                    {
                        // walk left
                        transform.Translate(Vector3.left * _walkingSpeed);
                        walkDirection = WalkDirection.Left;
                        _playerAnimation.SetFacingDirection(false);
                    }

                    // if there is no ground on left side
                    else
                    {
                        //walk right
                        transform.Translate(Vector3.right * _walkingSpeed);
                        walkDirection = WalkDirection.Right;
                        _playerAnimation.SetFacingDirection(true);
                    }
                }
                // if there is no ground on left side
                else
                {
                    // if there is ground on right side
                    if (Physics.Raycast(playerRight.transform.position, new Vector3(0, -1, 0), out raycastHit, _raycastDistance))
                    {
                        //walk right
                        transform.Translate(Vector3.right * _walkingSpeed);
                        walkDirection = WalkDirection.Right;
                        _playerAnimation.SetFacingDirection(true);
                    }
                }
            }

            // if player is walking to right
            else
            {
                //if ground is on right
                if (Physics.Raycast(playerRight.transform.position, new Vector3(0, -1, 0), out raycastHit, _raycastDistance))
                {
                    if (raycastHit.collider.gameObject.layer == 9)
                    {
                        // walk right
                        transform.Translate(Vector3.right * _walkingSpeed);
                        walkDirection = WalkDirection.Right;
                        _playerAnimation.SetFacingDirection(true);
                    }
                }

                // if there is no ground on right side
                else
                {
                    // if there is ground on left side
                    if (Physics.Raycast(playerLeft.transform.position, new Vector3(0, -1, 0), out raycastHit, _raycastDistance))
                    {
                        //walk left
                        transform.Translate(Vector3.left * _walkingSpeed);
                        walkDirection = WalkDirection.Left;
                        _playerAnimation.SetFacingDirection(true);
                    }
                }
            }
        }
        else _playerAnimation.SetWalkAnimation(false);
        
        //Debug.DrawRay(playerLeft.transform.position, new Vector3(0, -1 * _raycastDistance, 0), Color.red);
        //Debug.DrawRay(playerRight.transform.position, new Vector3(0, -1 * _raycastDistance, 0), Color.red);
        
    }

    private void CheckForGroundOnSide()
    {
        //Debug.DrawRay(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), new Vector3(-0.5f, 0, 0), Color.green);
        //Debug.DrawRay(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), new Vector3(0.5f, 0, 0), Color.green);

        RaycastHit raycastHit;
        
        //check for ground on left side
        if (Physics.Raycast(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z),
                new Vector3(-0.5f, 0, 0), out raycastHit, 0.75f))
        {
            if (raycastHit.collider.gameObject.layer == 8 || raycastHit.collider.gameObject.layer == 9)
            {
                walkDirection = WalkDirection.Right;
            }
            
        } 
        
        else if (Physics.Raycast(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z),
                      new Vector3(0.5f, 0, 0), out raycastHit, 0.75f))
        {
            if (raycastHit.collider.gameObject.layer == 8 || raycastHit.collider.gameObject.layer == 9)
            {
                walkDirection = WalkDirection.Left;
            }
        }
        
    }


    // if this doesnt work Ill seriously cry
    
    // it works so I dont need to cry 
    //
}
