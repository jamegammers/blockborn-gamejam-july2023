using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickUp : MonoBehaviour
{
   [SerializeField] private Abilities.Ability ability;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player found pickUp");
            other.GetComponent<Abilities>().SetAbility(ability);
            Destroy(this);
        }
    }
}
