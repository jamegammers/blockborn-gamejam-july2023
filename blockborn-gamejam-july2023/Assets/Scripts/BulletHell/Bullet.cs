using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    [SerializeField] private float speed = 30f;

    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
}
