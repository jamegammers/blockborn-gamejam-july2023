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
    
    [SerializeField] public float Cooldown1 = 1f;
    [SerializeField] public float Cooldown2 = 0.1f;
    [SerializeField] public float Cooldown3 = 0.4f;
    [SerializeField] public float Cooldown4 = 2f;
    [SerializeField] public float Cooldown5 = 0.6f;
    
    public enum BulletPatterns
    {
        Circle,
        Cone,
        Burst,
        Straight,
        Spiral,
        DoubleSpiral,
        Pyramid,
        WaveDecel,
        WayAllRange
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
                InvokeRepeating("Fire", 0f,Cooldown1);
                break;
            case BulletPatterns.Cone:
                bulletsAmount = 10;
                startAngle = 90f;
                endAngle = 270f;
                InvokeRepeating("Fire", 0f,Cooldown1);
                break;
            case BulletPatterns.Burst:
                bulletsAmount = 10;
                startAngle = 0f;
                endAngle = 360f;
                InvokeRepeating("Fire", 0f,Cooldown1);
                break;
            case BulletPatterns.Straight:
                bulletsAmount = 1;
                startAngle = 0f;
                endAngle = 0f;
                InvokeRepeating("Fire", 0f,Cooldown1);
                break;
            case BulletPatterns.Spiral:
                bulletsAmount = 300;
                InvokeRepeating("SpiralFire", 0f,Cooldown2);
                break;
            case BulletPatterns.DoubleSpiral:
                bulletsAmount = 300;
                InvokeRepeating("DoubleSpiralFire", 0f,Cooldown2);
                break;
            case BulletPatterns.Pyramid:
                bulletsAmount = 30;
                startAngle = 0f;
                endAngle = 360f;
                InvokeRepeating("Pyramid", 0f,Cooldown3);
                break;
            case BulletPatterns.WaveDecel:
                bulletsAmount = 10;
                startAngle = 0f;
                endAngle = 40f;
                InvokeRepeating("WaveDecel", 0f,Cooldown3);
                break;
            case BulletPatterns.WayAllRange:
                bulletsAmount = 10;
                startAngle = 0f;
                endAngle = 360f;
                InvokeRepeating("WayAllRange", 0f,Cooldown5);
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
    
    private void DoubleSpiralFire()
        {
            for (int i = 0; i <= 1; i++)
            {
                float bulDirX = transform.position.x + Mathf.Sin(((angle + 180f * i) * Mathf.PI) / 180f);
                float bulDirY = transform.position.y + Mathf.Cos(((angle + 180f * i) * Mathf.PI) / 180f);
                
                Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
                Vector2 bulDir = (bulMoveVector - transform.position).normalized;
                
                GameObject bul = BulletPool.Instance.GetBullet();
                bul.transform.position = transform.position;
                bul.transform.rotation = transform.rotation;
                bul.SetActive(true);
                bul.GetComponent<Bullet>().SetDirection(bulDir);
            }

            angle += 10f;
            
            if(angle >= 360f) angle = 0f;
        }

    private float pyramidSize = 2f;
    
    // Ripple Wave Effect
    public void Pyramid () {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        // Calculate the total number of rows in the pyramid
        int numRows = Mathf.CeilToInt(Mathf.Sqrt(bulletsAmount));

        // Calculate the spacing between each row and column
        float rowSpacing = pyramidSize / (numRows - 1);
        float colSpacing = pyramidSize / 2f;

        for (int row = 0; row < numRows; row++)
        {
            // Calculate the number of bullets in the current row
            int bulletsInRow = Mathf.Min(bulletsAmount - row * row, row + 1);

            // Calculate the starting position of the row
            Vector2 rowStartPosition = transform.position - new Vector3(colSpacing * (bulletsInRow - 1), row * rowSpacing, 0f) / 2f;

            for (int col = 0; col < bulletsInRow; col++)
            {
                // Calculate the position of the current bullet in the pyramid
                Vector2 bulletPosition = rowStartPosition + new Vector2(col * colSpacing, 0f);

                GameObject bul = BulletPool.Instance.GetBullet();
                bul.transform.position = bulletPosition;
                bul.transform.rotation = transform.rotation;
                bul.SetActive(true);
                bul.GetComponent<Bullet>().SetDirection(Vector2.up);

                angle += angleStep;
            }
        }
    }
    
    private float waveAmplitude = 2f;
    private float decelerationPower = 2f;

    private void WaveDecel()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f) * waveAmplitude;
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f) * waveAmplitude;

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

    private float rangeRadius = 2f;
    [SerializeField] private float turnAllRange = 200f;

    private void WayAllRange()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            // Calculate the rotation angle for each bullet
            float rotationAngle = angle + (turnAllRange * i);

            // Calculate the position of the bullet in the turned range
            float bulDirX = transform.position.x + Mathf.Cos((rotationAngle * Mathf.PI) / 180f) * rangeRadius;
            float bulDirY = transform.position.y + Mathf.Sin((rotationAngle * Mathf.PI) / 180f) * rangeRadius;

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
    
}
