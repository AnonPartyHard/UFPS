using UnityEngine;

public class RaySensor : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _layerMask;

    private RaycastHit _target;

    public RaycastHit Target => _target;

    public bool IsHit()
    {
        return Physics.Raycast(transform.position, transform.forward, out _target, _distance, _layerMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * _distance);
    }
}