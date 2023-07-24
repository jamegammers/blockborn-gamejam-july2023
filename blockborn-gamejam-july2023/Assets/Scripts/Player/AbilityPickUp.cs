using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class AbilityPickUp : MonoBehaviour
{
   [SerializeField] private Abilities.Ability ability;
    [SerializeField] private AudioSample _audioPowerUp;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            ArcadeAudio.PlayAudio(_audioPowerUp);
            other.GetComponent<Abilities>().SetAbility(ability);
            Destroy(gameObject);
        }
    }
}
