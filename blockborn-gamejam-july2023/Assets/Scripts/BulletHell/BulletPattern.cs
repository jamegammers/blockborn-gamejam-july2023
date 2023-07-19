using UnityEngine;
using System.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace BulletHell
{
    public class BulletPattern : MonoBehaviour
    {
        private Coroutine circularFireCoroutine;
        private bool isFiring = false;
        
        [SerializeField] private int bulletsAmount = 10;
        [SerializeField] private float startAngle = 90f, endAngle = 270f;
    
        private Vector2 bulletMoveDirection;

        [SerializeField] private float fireRate;
        [SerializeField] private float spinRate;
        [SerializeField] private float spinModifier;
        [SerializeField] private bool invertSpin;
        [SerializeField] private float maxSpinRate;
        [SerializeField] private float spread;
        [SerializeField] private Vector3 offset;
        
        
        void Start()
        {
            InvokeRepeating("StartCircularFire", 0f,0.5f);
            //StartCircularFire(fireRate, bulletsAmount, startAngle, endAngle, spinRate, spinModifier, invertSpin, maxSpinRate, spread, offset);
        }

        /*public void StartCircularFire(float fireRate, int bulletsAmount, float startAngle, float endAngle,
            float spinRate, float spinModifier, bool invertSpin, float maxSpinRate, float spread, Vector3 offset)
        {
            if (isFiring)
                StopCircularFire();

            circularFireCoroutine = StartCoroutine(CircularFireCoroutine(fireRate, bulletsAmount, startAngle, endAngle,
                spinRate, spinModifier, invertSpin, maxSpinRate, spread, offset));
        }*/
        
        public void StartCircularFire()
        {
            if (isFiring)
                StopCircularFire();

            circularFireCoroutine = StartCoroutine(CircularFireCoroutine(fireRate, bulletsAmount, startAngle, endAngle,
                spinRate, spinModifier, invertSpin, maxSpinRate, spread, offset));
        }

        public void StopCircularFire()
        {
            if (circularFireCoroutine != null)
            {
                StopCoroutine(circularFireCoroutine);
                circularFireCoroutine = null;
            }

            isFiring = false;
        }

        private IEnumerator CircularFireCoroutine(float fireRate, int bulletsAmount, float startAngle, float endAngle,
            float spinRate, float spinModifier, bool invertSpin, float maxSpinRate, float spread, Vector3 offset)
        {
            isFiring = true;

            float angleStep = (endAngle - startAngle) / bulletsAmount;
            float angle = startAngle;
            float currentSpinRate = spinRate;
            
                for (int i = 0; i < bulletsAmount; i++)
                {
                    // Apply spread between bullets
                    float bulletAngle = angle + Random.Range(-spread, spread);

                    // Calculate bullet position
                    float bulDirX = transform.position.x + Mathf.Cos((bulletAngle * Mathf.PI) / 180f) * offset.x;
                    float bulDirY = transform.position.y + Mathf.Sin((bulletAngle * Mathf.PI) / 180f) * offset.y;
                    float bulDirZ = offset.z;

                    Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, bulDirZ);

                    // Apply bullet travel curve or modification (e.g., acceleration, sine wave, etc.)
                    Vector3 modifiedMoveVector = ModifyBulletTravelCurve(bulMoveVector);

                    Vector2 bulDir = (modifiedMoveVector - transform.position).normalized;

                    GameObject bul = BulletPool.Instance.GetBullet();
                    bul.transform.position = transform.position;
                    bul.transform.rotation = transform.rotation;
                    bul.SetActive(true);
                    bul.GetComponent<Bullet>().SetDirection(bulDir);

                    angle += angleStep;
                }

                // Update spin rate and handle spin inversion
                if (invertSpin)
                    currentSpinRate = Mathf.Clamp(currentSpinRate - spinModifier, -maxSpinRate, maxSpinRate);
                else
                    currentSpinRate = Mathf.Clamp(currentSpinRate + spinModifier, -maxSpinRate, maxSpinRate);

                // Apply spin to the angle
                angle += currentSpinRate;

                yield return new WaitForSeconds(fireRate);
        }

        private Vector3 ModifyBulletTravelCurve(Vector3 originalPosition)
        {
            // Apply a sine wave motion to the bullet's travel curve
            float waveAmplitude = 0f;
            float waveFrequency = 0f;
            float waveSpeed = 0f;

            // Calculate the displacement based on time
            float time = Time.time * waveSpeed;
            float displacement = Mathf.Sin(time * waveFrequency) * waveAmplitude;

            // Apply the displacement to the y-axis of the bullet's position
            Vector3 modifiedPosition = originalPosition + new Vector3(0f, displacement, 0f);

            return modifiedPosition;
        }
    }
}