using UnityEngine;

public class ObjectRoot : MonoBehaviour
{
    public enum ObjectKinds
    {
        Player,
        Unit,
        Weapon
    }

    [SerializeField] private ObjectKinds _kind;
    [SerializeField] private Collider _instanceCollider;

    public ObjectKinds Kind => _kind;
    public Collider InstanceCollider => _instanceCollider;
}