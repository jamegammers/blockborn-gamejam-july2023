using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class ShieldSound : MonoBehaviour
{
    [SerializeField] private AudioSample _shieldSound;

    private void OnEnable()
    {
        ArcadeAudio.PlayAudio(_shieldSound);
    }
}
