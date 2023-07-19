using System.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using BulletHell;
using UnityEngine;
public class FireBullets : MonoBehaviour
{
    [SerializeField] private int bulletsAmount = 10;
    [SerializeField] private float startAngle = 90f, endAngle = 270f;
    
    private Vector2 bulletMoveDirection;

    [SerializeField] private BulletPatterns activebulletPattern;
    
    public enum BulletPatterns
    {
        Circle,
        Cone,
        Burst,
        Straight,
        Spiral,
        Random
    }
    
    public void SetActiveBulletPattern(BulletPatterns bulletPattern)
    {
        activebulletPattern = bulletPattern;
        PatternSwitch();
    }

    private void PatternSwitch()
    {
        switch (activebulletPattern)
        {
            case BulletPatterns.Circle:
                bulletsAmount = 10;
                startAngle = 0f;
                endAngle = 360f;
                InvokeRepeating("Fire", 0f,2f);
                break;
            case BulletPatterns.Cone:
                bulletsAmount = 10;
                startAngle = 90f;
                endAngle = 270f;
                InvokeRepeating("Fire", 0f,2f);
                break;
            case BulletPatterns.Burst:
                bulletsAmount = 10;
                startAngle = 0f;
                endAngle = 360f;
                InvokeRepeating("Fire", 0f,2f);
                break;
            case BulletPatterns.Straight:
                bulletsAmount = 1;
                startAngle = 0f;
                endAngle = 0f;
                InvokeRepeating("Fire", 0f,2f);
                break;
            case BulletPatterns.Spiral:
                bulletsAmount = 300;
                InvokeRepeating("SpiralFire", 0f,0.1f);
                break;
            case BulletPatterns.Random:
                bulletsAmount = 10;
                startAngle = 0f;
                endAngle = 360f;
                InvokeRepeating("Fire", 0f,2f);
                break;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        PatternSwitch();
    }

    private void Fire()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
            
            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;
            
            GameObject bul = BulletPool.Instance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetDirection(bulDir);
            
            angle += angleStep;
        }
    }

    private float angle = 0f;
    
    private void SpiralFire()
    {
        float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
            
        Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
        Vector2 bulDir = (bulMoveVector - transform.position).normalized;
            
        GameObject bul = BulletPool.Instance.GetBullet();
        bul.transform.position = transform.position;
        bul.transform.rotation = transform.rotation;
        bul.SetActive(true);
        bul.GetComponent<Bullet>().SetDirection(bulDir);
            
        angle += 10f;
    }
}
