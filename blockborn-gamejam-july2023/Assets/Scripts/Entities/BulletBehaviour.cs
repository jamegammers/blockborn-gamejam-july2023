using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        //Debug.Log("Bullet collision");
        if (other.gameObject.layer == 8)
        {
            other.GetComponent<EnemyLoop>().GetHit(1);
        }
        Destroy(gameObject);
    }


    private IEnumerator ShowBullet()
    {
        yield return new WaitForSeconds(_bulletVisibleAfter);
        _meshRenderer.enabled = true;
    }

}
