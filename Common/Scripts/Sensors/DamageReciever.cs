using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class DamageReciever : MonoBehaviour
{
    [SerializeField] private float _damageMultiplier;
    private HealthPointsManager _hpManager;

    private void Awake()
    {
        _hpManager = GetComponentInParent<HealthPointsManager>();
        // GetComponent<Rigidbody>().isKinematic = true;
    }

    public void RecieveDamage(float damage)
    {
        _hpManager.RecieveDamage(damage * _damageMultiplier);
    }
}