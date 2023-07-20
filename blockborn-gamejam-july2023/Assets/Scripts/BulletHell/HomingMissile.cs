using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Util;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody))]
public class HomingMissile : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float rotateSpeed = 200f;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Spiral").transform;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector2 direction = target.position - rb.position;
        direction.Normalize();
        //Vector3.Cross(direction, transform.up);
        Vector3 rotateAmount = Vector3.Cross(direction, transform.up);
        rb.angularVelocity = (-rotateAmount) * rotateSpeed;
        rb.velocity = transform.up * speed;
    }
}
