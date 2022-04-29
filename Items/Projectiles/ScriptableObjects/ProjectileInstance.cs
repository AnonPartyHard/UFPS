using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileInstance : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private ProjectileData _data;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = _data.UseGravity;
    }

    private void OnEnable()
    {
        LaunchProjectile();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_data.BlowForce > 0)
            ApplyExplosionForce();
    
        _rigidbody.velocity = Vector3.zero;
        ObjectsPool.instance.ReturnObject(gameObject);
        ObjectsPool.instance.GetObject(_data.ParticleSystemIdInObjectsPool, collision.contacts[0].point,
            Quaternion.LookRotation(collision.contacts[0].normal));
    }

    private void ApplyExplosionForce()
    {
        var hitColliders = Physics.OverlapSphere(transform.position, _data.BlowRadius);
        if (hitColliders.Length > 0)
            foreach (var hitCollider in hitColliders)
                if (hitCollider.attachedRigidbody != null)
                    hitCollider.attachedRigidbody.AddExplosionForce(_data.BlowForce * Time.fixedDeltaTime,
                        transform.position, _data.BlowRadius);
    }

    private void LaunchProjectile()
    {
        _rigidbody.AddForce(transform.forward * _data.PushForce * Time.fixedDeltaTime, _data.ForceMode);
    }
}