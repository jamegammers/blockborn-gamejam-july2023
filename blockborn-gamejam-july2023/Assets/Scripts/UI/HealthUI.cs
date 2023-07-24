using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{

    [SerializeField] private List<GameObject> _healthBars;

    public void SetHealth(int health)
    {
        for (int i = 5; i > health; i--)
        {
            _healthBars[i - 1].SetActive(false);
        }
    }

    public void Reset() {
        foreach (GameObject bar in _healthBars)
            bar.SetActive(true);
    }

}
