using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField, Range(1, 10f)] private int _health;
    public void GetDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0) Death();
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
