using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowYouShouldntDoAnimations : MonoBehaviour
{
    [SerializeField] private GameObject sprite1;
    [SerializeField] private GameObject sprite2;
    
    [SerializeField] private float _animationSpeed = 0.5f;
    private float _timePassedSinceLastAnimation = 0f;

    // Update is called once per frame
    void Update()
    {
        PlayAnimation();
    }
    
    private void PlayAnimation()
    {
        _timePassedSinceLastAnimation += Time.deltaTime;
        if (_timePassedSinceLastAnimation > _animationSpeed)
        {
            if (sprite1.activeSelf)
            {
                sprite1.SetActive(false);
                sprite2.SetActive(true);
                _timePassedSinceLastAnimation = 0f;
            }
            else
            {
                sprite1.SetActive(true);
                sprite2.SetActive(false);
                _timePassedSinceLastAnimation = 0f;
            }
        }
    }
}
