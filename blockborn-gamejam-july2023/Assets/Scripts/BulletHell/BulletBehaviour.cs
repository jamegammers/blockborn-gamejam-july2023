using System.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using BulletHell;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.ProBuilder.MeshOperations;
using Random = UnityEngine.Random;

namespace BulletHell
{
    
    public class BulletBehaviour : MonoBehaviour
    {
        public float amplitude; // The amplitude of the sinus curve.
        public float frequency; // The frequency of the sinus curve.
        [SerializeField] private GameObject player;
        
        public enum BulletBehaviours
        {
            SineCurve,
            Circle,
            Bounce,
            Follow
        }
        
        public BulletBehaviours activebulletBehaviour;

        private void BehaviourSwitch()
        {
            switch (activebulletBehaviour)
            {
                case BulletBehaviours.SineCurve:
                    InvokeRepeating("SineCurve", 0f, 0.5f);
                    break;
                case BulletBehaviours.Follow:
                    InvokeRepeating("Follow", 0f, 1f);
                    break;
                case BulletBehaviours.Circle:
                    InvokeRepeating("Circle", 0f, 1f);
                    break;
            }
        }

        public void SetBehaviour(BulletBehaviours bulletBehaviour)
        {
            activebulletBehaviour = bulletBehaviour;
        }

        public void Awake()
        {
            player = GameObject.FindWithTag("Player");
        }

        public void Start()
        {
            BehaviourSwitch();
        }

        //Create a Bullet Behaviour pattern where the bullet follows the player
        private void Follow()
        {
            //Create a Bullet Behaviour pattern where the bullet follows the player
            Vector3 pos = transform.position;
            Vector2 bulDir = Vector2.MoveTowards(pos, player.transform.position, 1f);
            this.GetComponent<Bullet>().SetDirection(bulDir);
        }

        //Create a Bullet Behaviour pattern where the bullet bounces off the walls
        private void Bounce()
        {
            
        }
        
        
        private void Circle()
        {
            //Create a Bullet Behaviour pattern where the bullet moves in a circle
            Vector3 pos = transform.position;
            Vector2 bulDir = new Vector2(Mathf.Sin(Time.time), Mathf.Cos(Time.time));
            this.GetComponent<Bullet>().SetDirection(bulDir);
        }


        private void SineCurve()
        {
            //Create Method Bullet Behaviour pattern where the bullet follows a sine curve
            Vector3 pos = transform.position;
            Vector3 velocity = new Vector3(0f, amplitude * Mathf.Sin(frequency * Time.time), 0f);
            Vector2 bulDir =  pos + velocity * Time.deltaTime;
            this.GetComponent<Bullet>().SetDirection(bulDir);
        }
    }

    
}