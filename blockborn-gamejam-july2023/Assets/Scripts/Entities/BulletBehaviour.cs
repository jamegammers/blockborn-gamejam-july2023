using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField, Range(0, 2f)] private float _bulletVisibleAfter = 0.5f;

    private void Start()
    {
        StartCoroutine(ShowBullet());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bullet collision");
        if (other.gameObject.GetComponent<HealthManager>() != null)
        {
            other.gameObject.GetComponent<HealthManager>().GetDamage(1);
        }
        Destroy(gameObject);
    }


    private IEnumerator ShowBullet()
    {
        yield return new WaitForSeconds(_bulletVisibleAfter);
        _meshRenderer.enabled = true;
    }

}
