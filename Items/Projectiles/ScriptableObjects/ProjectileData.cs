using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Projectile/Basic", order = 1)]
public class ProjectileData : ScriptableObject
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _pushForce;
    [SerializeField] private bool _useGravity;
    [SerializeField] private float _blowForce;
    [SerializeField] private float _blowRadius;
    [SerializeField] private ForceMode _forceMode;
    [SerializeField] private string _idInObjectsPool;
    [SerializeField] private GameObject _particleSystemPrefab;
    [SerializeField] private string _particleSystemIdInObjectsPool;
    [SerializeField] private int _countInPool;

    public GameObject Prefab => _prefab;

    public float PushForce => _pushForce;

    public bool UseGravity => _useGravity;

    public ForceMode ForceMode => _forceMode;

    public string IdInObjectsPool => _idInObjectsPool;

    public float BlowForce => _blowForce;

    public float BlowRadius => _blowRadius;

    public GameObject ParticleSystemPrefab => _particleSystemPrefab;

    public string ParticleSystemIdInObjectsPool => _particleSystemIdInObjectsPool;

    public int CountInPool => _countInPool;
}