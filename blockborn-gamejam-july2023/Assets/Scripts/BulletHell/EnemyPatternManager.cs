using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatternManager : MonoBehaviour
{
    [SerializeField] private List<FireBullets.BulletPatterns> bulletPatterns;
    FireBullets fireBullets;
    [SerializeField] public float Cooldown = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        fireBullets = this.GameObject().GetComponent<FireBullets>();
        //Start a Coroutine of StartPattern filling in a bulletPattern
        //StartCoroutine(StartPattern(FireBullets.BulletPatterns.Circle));
    }

    private float lastShot; 
    private void StartPattern(FireBullets.BulletPatterns bulletPattern)
    {
        if(Time.time - lastShot<Cooldown) return;
        lastShot = Time.time;
        
        fireBullets.SetActiveBulletPattern(bulletPattern);
        
    }

    private void Patterns()
    {
        if(Time.time - lastShot<Cooldown) return;
        lastShot = Time.time;
    }
}
