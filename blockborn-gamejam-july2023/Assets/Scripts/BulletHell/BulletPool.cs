using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell
{
    public class BulletPool : MonoBehaviour
    {
        public static BulletPool Instance { get; private set; }

        [SerializeField] private GameObject pooledBullet;
        private bool notEnoughBulletsInPool = true;
        
        private List<GameObject> bullets;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            bullets = new List<GameObject>();
        }
        
        public GameObject GetBullet()
        {
            if (bullets.Count > 0)
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (!bullets[i].activeInHierarchy)
                    {
                        return bullets[i];
                    }
                }
            }
            if (notEnoughBulletsInPool)
            {
                GameObject bullet = Instantiate(pooledBullet);
                bullet.SetActive(false);
                bullets.Add(bullet);
                return bullet;
            }
            return null;
        }
    }
}