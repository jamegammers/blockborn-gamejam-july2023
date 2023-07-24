using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class WalkSounds : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private AudioSample _audioEngine;

    public void WalkSound()
    {
        _playerMovement.WalkSound();
    }

    public void EngingeSound()
    {
        ArcadeAudio.PlayAudio(_audioEngine);
    }
}
