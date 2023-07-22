using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    [SerializeField] public float speed = 30f;

    [SerializeField] private ParticleSystem impactEffect;

    private void OnEnable()
    {
        Invoke("Destroy", 5f);
        impactEffect.Stop();
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    
    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
    
    private void Destroy()
    {
        gameObject.SetActive(false);
    }
    
    private void OnDisable()
    {
        CancelInvoke();
    }

    /*private void OnCollisionEnter(Collision other)
    {
        impactEffect.Play();
        StartCoroutine(WaitForParticleSystem());
    } */

    private void OnTriggerEnter(Collider other)
    {
        impactEffect.Play();
        StartCoroutine(WaitForParticleSystem());
    }

    private IEnumerator WaitForParticleSystem()
    {
        yield return new WaitForSeconds(impactEffect.main.duration);
        Destroy();
    }
}
