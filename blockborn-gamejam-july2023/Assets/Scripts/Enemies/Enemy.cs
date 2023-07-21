using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public GameObject enemyPrefab;
    
    public float health;
    
    public GameObject bullet;
    public GameObject vfx;
    public float cooldown;
    public float damage;
    public float walkSpeed;
}
