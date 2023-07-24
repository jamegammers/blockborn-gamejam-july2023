using System;
using System.Collections;
using System.Collections.Generic;
using BulletHell;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatternManager : MonoBehaviour
{
    FireBullets fireBullets;
    [SerializeField] private List<BulletPatterns> patterns;
    [SerializeField] private float[] patternDurations;
    public bool useAlternateDurations;
    [SerializeField] private Collider ProximityField;
    public float Cooldown;
    private bool isOnCooldown;
    public float patternDuration;
    public bool isFiring = false;
    
    private bool playerClose = false;

    private BulletPool pool;
    
    // Start is called before the first frame update
    void Start()
    {
        fireBullets = this.GameObject().GetComponent<FireBullets>();
        pool = BulletPool.Instance;
        // StartFiringPatterns();
        //Start a Coroutine of StartPattern filling in a bulletPattern
    }
    
    private void StartFiringPatterns()
    {
        if (!isFiring && !isOnCooldown && playerClose)
        {
            StartCoroutine(ReadBulletPatterns());
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerClose = true;
            Debug.Log("Player is Close");
            StartFiringPatterns();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerClose = false;
            isFiring = false;
            StopCoroutine(ReadBulletPatterns());
        }
    }

    private IEnumerator ReadBulletPatterns()
    {
        while (playerClose)
        {
            foreach (BulletPatterns pattern in patterns)
        {
            Debug.Log("Calling Pattern " + pattern.patternName);
            if (pattern.patternType == BulletPatternEnum.BulletPatternsEnum.None)
            {
                StopCoroutine(StartPattern(pattern));
                isFiring = false;
                if (useAlternateDurations) patternDuration = patternDurations[patterns.IndexOf(pattern)];
                else  patternDuration = pattern.patternDuration;
                Cooldown = pattern.Cooldown;
                fireBullets.SetBulletPatternNone();
                yield return new WaitForSeconds(pattern.patternDuration);
            }
            else
            {
                StartCoroutine(StartPattern(pattern));
            }

            if (Cooldown > 0f)
            {
                // Set the isOnCooldown flag to true and start the cooldown timer
                isOnCooldown = true;
                yield return new WaitForSeconds(Cooldown);
                isOnCooldown = false;
            }
        }
            Debug.Log("End of Patterns reached");
            isFiring = false;
            StopCoroutine(ReadBulletPatterns());
            StartFiringPatterns();
        }
        
        isFiring = false;
       
    }

    private IEnumerator StartPattern(BulletPatterns pattern)
    {
        isFiring = true;
        if (useAlternateDurations) patternDuration = patternDurations[patterns.IndexOf(pattern)];
        else  patternDuration = pattern.patternDuration;
        BulletPool.Instance.GetEnemyBulletPrefab().GetComponent<Bullet>().SetSpeed(pattern.BulletSpeed);
        Cooldown = pattern.Cooldown;
        fireBullets.SetBulletPattern(pattern.patternType, pattern.bulletBehaviour, pattern.bulletAmount, 
            pattern.startAngle, pattern.endAngle, pattern.isAiming, pattern.FireRate);
        yield return new WaitForSeconds(pattern.patternDuration);
        fireBullets.SetBulletPatternNone();
    }
}
