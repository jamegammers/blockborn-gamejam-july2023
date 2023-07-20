using System.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using BulletHell;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.ProBuilder.MeshOperations;
using Random = UnityEngine.Random;


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

    [SerializeField] private GameObject player;
    [SerializeField] public bool isAiming = false;

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
        WayAllRange,
        Aimed
    }

    public void SetActiveBulletPattern(BulletPatterns bulletPattern, int bullets, float startAngle, float endAngle, bool Aiming)
    {
        activebulletPattern = bulletPattern;
        bulletsAmount = bullets;
        this.startAngle = startAngle;
        this.endAngle = endAngle;
        isAiming = Aiming;
        PatternSwitch();
    }

    private void PatternSwitch()
    {
        switch (activebulletPattern)
        {
            case BulletPatterns.Circle:
                //Can Aim
                bulletsAmount = 10;
                startAngle = 0f;
                endAngle = 360f;
                InvokeRepeating("CircleFire", 0f, Cooldown1);
                break;
            case BulletPatterns.Cone:
                //Can Aim
                bulletsAmount = 10;
                startAngle = 90f;
                endAngle = 270f;
                InvokeRepeating("Cone", 0f, Cooldown1);
                break;
            case BulletPatterns.Burst:
                // Not Implemented
                bulletsAmount = 30;
                startAngle = 0f;
                endAngle = 360f;
                InvokeRepeating("CircleFire", 0f, Cooldown1);
                break;
            case BulletPatterns.Straight:
                //Can Aim
                bulletsAmount = 10;
                startAngle = 0f;
                endAngle = 0f;
                InvokeRepeating("CircleFire", 0f, Cooldown1);
                break;
            case BulletPatterns.Spiral:
                //Cannot Aim
                bulletsAmount = 300;
                InvokeRepeating("SpiralFire", 0f, Cooldown2);
                break;
            case BulletPatterns.DoubleSpiral:
                //Cannot Aim
                bulletsAmount = 300;
                InvokeRepeating("DoubleSpiralFire", 0f, Cooldown2);
                break;
            case BulletPatterns.Pyramid:
                //Can Aim
                bulletsAmount = 30;
                InvokeRepeating("Pyramid", 0f, Cooldown3);
                break;
            case BulletPatterns.WaveDecel:
                bulletsAmount = 10;
                startAngle = 0f;
                endAngle = 40f;
                InvokeRepeating("WaveDecel", 0f, Cooldown3);
                break;
            case BulletPatterns.WayAllRange:
                bulletsAmount = 10;
                startAngle = 0f;
                endAngle = 360f;
                InvokeRepeating("WayAllRange", 0f, Cooldown5);
                break;
            case BulletPatterns.Aimed:
                bulletsAmount = 10;
                startAngle = 0f;
                endAngle = 360f;
                InvokeRepeating("Aimed", 0f, Cooldown5);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        PatternSwitch();
    }

    private Vector2 bulDir;
    
    private void Cone()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            // Calculate the direction towards the player using Aimed() method
            Vector2 bulDir = isAiming ? (-Aim()) : RandomAim();

            // Rotate the bullet direction by the cone pattern angle
            float rotatedAngle = angle * Mathf.Deg2Rad;
            float cosAngle = Mathf.Cos(rotatedAngle);
            float sinAngle = Mathf.Sin(rotatedAngle);

            // Rotate the direction vector
            float rotatedX = bulDir.x * cosAngle - bulDir.y * sinAngle;
            float rotatedY = bulDir.x * sinAngle + bulDir.y * cosAngle;

            // Create a new vector with the rotated direction
            bulDir = new Vector2(rotatedX, rotatedY);

            // Spawn and set direction for the bullet
            GameObject bul = BulletPool.Instance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetDirection(bulDir);
            
            angle += angleStep;
        }
    }
    
    private void PwettyPattern()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            // Calculate the direction towards the player using Aimed() method
            Vector2 bulDir = isAiming ? (Aim()) : RandomAim();

            // Rotate the bullet direction by the cone pattern angle
            float rotatedAngle = angle * Mathf.Deg2Rad;
            float cosAngle = Mathf.Cos(rotatedAngle);
            float sinAngle = Mathf.Sin(rotatedAngle);

            // Rotate the direction vector
            float rotatedX = bulDir.x * cosAngle - bulDir.y * sinAngle;
            float rotatedY = bulDir.x * sinAngle + bulDir.y * cosAngle;

            // Create a new vector with the rotated direction
            bulDir = new Vector2(rotatedX, rotatedY);

            // Spawn and set direction for the bullet
            GameObject bul = BulletPool.Instance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetDirection(bulDir);
            bul.GetComponent<BulletHell.BulletBehaviour>().SetBehaviour(BulletHell.BulletBehaviour.BulletBehaviours.SineCurve, bulDir);
            //bul.GetComponent<Bullet>().SetDirection(bulDir);
            
            angle += angleStep;
        }
    }
    
    private void CircleFire()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            // Calculate the direction towards the player using Aimed() method
            Vector2 bulDir = isAiming ? (Aim()) : RandomAim();

            // Rotate the bullet direction by the cone pattern angle
            float rotatedAngle = angle * Mathf.Deg2Rad;
            float cosAngle = Mathf.Cos(rotatedAngle);
            float sinAngle = Mathf.Sin(rotatedAngle);

            // Rotate the direction vector
            float rotatedX = bulDir.x * cosAngle - bulDir.y * sinAngle;
            float rotatedY = bulDir.x * sinAngle + bulDir.y * cosAngle;

            // Create a new vector with the rotated direction
            bulDir = new Vector2(rotatedX, rotatedY);

            // Spawn and set direction for the bullet
            GameObject bul = BulletPool.Instance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetDirection(bulDir);
            bul.GetComponent<BulletHell.BulletBehaviour>().SetBehaviour(BulletHell.BulletBehaviour.BulletBehaviours.Follow, bulDir);
            //bul.GetComponent<Bullet>().SetDirection(bulDir);
            
            angle += angleStep;
        }
    }

    private float angle = 0f;

    private void SpiralFire()
    {
        float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

        Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
        bulDir = (bulMoveVector - transform.position).normalized;

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
             bulDir = (bulMoveVector - transform.position).normalized;

             GameObject bul = BulletPool.Instance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetDirection(bulDir);
        }

        angle += 10f;

        if (angle >= 360f) angle = 0f;
    }

    private float pyramidSize = 2f;

    // Ripple Wave Effect
    public void Pyramid()
    {
        int numRows = Mathf.CeilToInt(Mathf.Sqrt(bulletsAmount));

        // Calculate the spacing between each row and column
        float rowSpacing = pyramidSize / (numRows - 1);
        float colSpacing = pyramidSize / 2f;
        int bulletsInBottomRow = 5;

        float rotationAngle = -Aim().x * Mathf.Rad2Deg;
        
        // Calculate the total number of bullets in the pyramid
        int totalBullets = 0;
        for (int row = 0; row < numRows; row++)
        {
            totalBullets += bulletsInBottomRow - row;
        }

        // Calculate the rotation angle for the entire pyramid
        Quaternion rotation = Quaternion.Euler(0f, 0f, rotationAngle);

        int bulletCount = 0;
        for (int row = 0; row < numRows; row++)
        {
            int bulletsInRow = bulletsInBottomRow - row;
            float xOffset = -(bulletsInRow - 1) * colSpacing * 0.5f;
            float yOffset = row * rowSpacing;

            for (int col = 0; col < bulletsInRow; col++)
            {
                float xPosition = col * colSpacing + xOffset;
                float yPosition = yOffset;

                Vector3 position = rotation * new Vector3(xPosition, yPosition, 0f) + transform.position; ;
                
                GameObject bul = BulletPool.Instance.GetBullet();
                bul.transform.position = position;
                bul.transform.rotation = transform.rotation;
                bul.SetActive(true);
                bul.GetComponent<Bullet>().SetDirection((position - transform.position).normalized);

                bulletCount++;

                if (bulletCount >= totalBullets)
                {
                    // If we've spawned all bullets, exit the loop early
                    return;
                }
            }
        }
    }

    private void Pyramid2()
    {
        // Calculate the total number of rows in the pyramid
        int numRows = Mathf.CeilToInt(Mathf.Sqrt(bulletsAmount));

        // Calculate the spacing between each row and column
        float rowSpacing = pyramidSize / (numRows - 1);
        float colSpacing = pyramidSize / 2f;
        float rotationAngle = -Aim().x * Mathf.Rad2Deg;
        
        int bulletCount = 0;
        for (int row = 0; row < numRows; row++)
        {
            int bulletsInRow = bulletsAmount - bulletCount < numRows ? bulletsAmount - bulletCount : numRows - row;
            float xOffset = -(bulletsInRow - 1) * colSpacing * 0.5f;
            float yOffset = row * rowSpacing;

            for (int col = 0; col < bulletsInRow; col++)
            {
                float xPosition = col * colSpacing + xOffset;
                float yPosition = yOffset;

                Quaternion rotation = Quaternion.Euler(0f, 0f, rotationAngle);
                Vector3 position = rotation * new Vector3(xPosition, yPosition, 0f) + transform.position;

                GameObject bul = BulletPool.Instance.GetBullet();
                bul.transform.position = position;
                bul.transform.rotation = transform.rotation;
                bul.SetActive(true);
                bul.GetComponent<Bullet>().SetDirection((position - transform.position).normalized);

                bulletCount++;

                if (bulletCount >= bulletsAmount)
                {
                    // If we've spawned all bullets, exit the loop early
                    return;
                }
            }
        }
    }

    private float waveAmplitude = 40f;

    private void WaveDecel()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            // Calculate the direction towards the player using Aimed() method
            bulDir = isAiming ? (Aim()) : RandomAim();

            // Rotate the bullet direction by the cone pattern angle
            float rotatedAngle = angle * Mathf.Deg2Rad;
            float cosAngle = Mathf.Cos(rotatedAngle);
            float sinAngle = Mathf.Sin(rotatedAngle);

            // Rotate the direction vector
            float rotatedX = bulDir.x * cosAngle - bulDir.y * sinAngle;
            float rotatedY = bulDir.x * sinAngle + bulDir.y * cosAngle;

            // Create a new vector with the rotated direction
            bulDir = new Vector2(rotatedX, rotatedY);
            
            /*float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f) * waveAmplitude;
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f) * waveAmplitude;

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            bulDir = (bulMoveVector - transform.position).normalized;*/

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

    private void WayTurnAllRange()
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

    void WayAllRange()
    {
        float angle = 0;
        while (angle > -2 * Mathf.PI)
        {
            float bulDirX = transform.position.x + Mathf.Cos(angle) * 20;
            float bulDirY = transform.position.y + Mathf.Sin(angle) * 20;
            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.Instance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetDirection(bulDir);

            angle = (float)(angle - Mathf.PI / 2f);
        }
    }

    private Vector2 Aim()
    {
            float angle = (float)Math.Atan2(player.transform.position.y - transform.position.y,
            player.transform.position.x - transform.position.x);
            float bulDirX = transform.position.x + Mathf.Cos(angle) * 10;
            float bulDirY = transform.position.y + Mathf.Sin(angle) * 10;
            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;
            return bulDir;
    }

    private Vector2 RandomAim()
    {
        float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

        Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
        bulDir = (bulMoveVector - transform.position).normalized;
        return bulDir;
    }

    private void Aimed()
    {
        float angle = (float)Math.Atan2(player.transform.position.y - transform.position.y,
            player.transform.position.x - transform.position.x);
        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Cos(angle) * 10;
            float bulDirY = transform.position.y + Mathf.Sin(angle) * 10;
            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;
            GameObject bul = BulletPool.Instance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetDirection(bulDir);

        }
    }


}