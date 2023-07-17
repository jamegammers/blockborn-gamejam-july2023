using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public Sprite sprite;
    
    public float health;
    
    public GameObject vfx;
    public float cooldown;
    public float damage;

    public GameObject bullet;
    
    //TODO: define attack pattern
    //TODO: define drop on death
    
    public void HandleLevelScaling(int level)
    {
        //ever 5 levels, increase health and damage by 10%
        if (level % 5 == 0)
        {
            health *= 1.1f;
            damage *= 1.1f;
        }
    }

    public void SpawnEnemy(Vector3 spawnPos)
    {
        //TODO: Spawning
    }
    
    //TODO: Aufrufbare Methode die das Health updated und den Enemy zerstört wenn Health <= 0
}
