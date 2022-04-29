using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DirectForceAdder : MonoBehaviour
{
    [SerializeField] private float _forcePower;
    [SerializeField] private ForceMode _forceMode;
    [SerializeField] private bool _enabled = false;

    public bool Enabled
    {
        get => _enabled;
        set => _enabled = value;
    }

    public float ForcePower
    {
        get => _forcePower;
        set => _forcePower = value;
    }

    private void Awake()
    {
        var collider = GetComponent<BoxCollider>();
        if (!collider.isTrigger)
            collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_enabled)
        {
            var rb = other.GetComponent<Rigidbody>();
            if (rb == null)
                rb = other.GetComponentInParent<Rigidbody>();

            if (rb != null)
                other.GetComponentInParent<Rigidbody>()
                    .AddForce(transform.forward * _forcePower * Time.fixedDeltaTime, _forceMode);
        }
    }
}