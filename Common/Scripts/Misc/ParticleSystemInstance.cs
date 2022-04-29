using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemInstance : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private float _lifeTime;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _lifeTime = _particleSystem.main.startLifetimeMultiplier;
    }

    private void OnEnable()
    {
        _particleSystem.Play();
        StartCoroutine(ReturnToPoolCoroutine());
    }

    private IEnumerator ReturnToPoolCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        ObjectsPool.instance.ReturnObject(gameObject);
    }
}