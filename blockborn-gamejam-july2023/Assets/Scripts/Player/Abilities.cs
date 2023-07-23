using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public enum Ability
    {
        Shield,
        ShootFast,
        HomingMissile
    }
    
    public Transform target;
    public float speed = 5f;
    public float rotateSpeed = 200f;
    [SerializeField] public GameObject MissilePrefab;
    private GameObject missile;
    [SerializeField] public GameObject shield; 
    
    public static Abilities Instance { get; private set; }
    private Ability ability;
    

    private bool abilityActive = false;
    private float abilityDuration;
     
    void Awake()
    {
        Instance = this;
    }

    public void SetAbility(Ability setability)
    {
        ability = setability;
        
        switch (ability)
        {
            case Ability.Shield:
                Debug.Log("Shield activated");
                abilityDuration = 4f;
                StartCoroutine(Shield());
                break;
            case Ability.ShootFast:
                Debug.Log("ShootFast activated");
                abilityDuration = 5f;
                StartCoroutine(ShootFast());
                break;
            case Ability.HomingMissile:
                Debug.Log("Homing Missile activated");
                abilityDuration = 6f;
                StartCoroutine(HomingMissile());
                break;
        }
    }

    private void Update()
    {
        
    }

    public IEnumerator HomingMissile()
    {    abilityActive = true;

        target = GameObject.Find("Enemy").transform;
        missile = Instantiate(MissilePrefab, transform.position, Quaternion.identity);
        Rigidbody rb = missile.GetComponent<Rigidbody>();
        Vector2 direction = target.position - rb.position;
            direction.Normalize();
            //Vector3.Cross(direction, transform.up);
            Vector3 rotateAmount = Vector3.Cross(direction, transform.up);
            rb.angularVelocity = (-rotateAmount) * rotateSpeed;
            rb.velocity = transform.up * speed;
            yield return new WaitForSeconds(abilityDuration);
        Destroy(missile);
        abilityActive = false;
    }
    
    public IEnumerator ShootFast()
    {
        abilityActive = true;
        float oldShootRange = this.GetComponent<PlayerShoot>().getShootCD();
        this.GetComponent<PlayerShoot>().setShootCD(0.2f);
        yield return new WaitForSeconds(abilityDuration);
        this.GetComponent<PlayerShoot>().setShootCD(oldShootRange);
        abilityActive = false;
    }
    
    public IEnumerator Shield()
    {
        abilityActive = true;
        shield.SetActive(true);
        yield return new WaitForSeconds(abilityDuration);
        shield.SetActive(false);
        abilityActive = false;
    }
}
