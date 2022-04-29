using UnityEngine;

[CreateAssetMenu(fileName = "AIUnit", menuName = "ScriptableObjects/AI/Unit", order = 1)]
public class AIUnitData : ScriptableObject
{
    public enum AIUnitsTypes
    {
        range,
        melee
    }

    [Header("Basic props")] [SerializeField]
    private string _name;

    [SerializeField] private AIUnitsTypes _type;

    [Header("Move speed")] [SerializeField]
    private float _patrolMoveSpeed;

    [SerializeField] private float _chasingMoveSpeed;
    [SerializeField] private float _attackMoveSpeed;

    [Header("Sight props")] [SerializeField]
    private float _sightRadius;

    [SerializeField] private float _sightAngle;
    [SerializeField] private LayerMask _sightLayers;
    [SerializeField] private LayerMask _enemiesLayers;
    [SerializeField] private LayerMask _obstaclesToIgnoreWhileChaseLayers;

    [Header("Attack props")] [SerializeField]
    private Weapon _weapon;

    [SerializeField] private float _attackRadius;
    [SerializeField] private int _burstFireMinCount;
    [SerializeField] private int _burstFireMaxCount;
    [SerializeField] private float _burstDelayMinTime;
    [SerializeField] private float _burstDelayMaxTime;

    public string Name => _name;
    public AIUnitsTypes Type => _type;
    public float PatrolMoveSpeed => _patrolMoveSpeed;
    public float ChasingMoveSpeed => _chasingMoveSpeed;
    public float AttackMoveSpeed => _attackMoveSpeed;
    public Weapon Weapon => _weapon;
    public float SightRadius => _sightRadius;
    public float SightAngle => _sightAngle;
    public LayerMask SightLayers => _sightLayers;
    public LayerMask EnemiesLayers => _enemiesLayers;
    public LayerMask ObstaclesToIgnoreWhileChaseLayers => _obstaclesToIgnoreWhileChaseLayers;
    public int BurstFireMinCount => _burstFireMinCount;
    public int BurstFireMaxCount => _burstFireMaxCount;
    public float BurstDelayMinTime => _burstDelayMinTime;
    public float BurstDelayMaxTime => _burstDelayMaxTime;

    public int GetRandomBurstCount()
    {
        return Random.Range(_burstFireMinCount, _burstFireMaxCount);
    }

    public float GetRandomBurstDelay()
    {
        return Random.Range(_burstDelayMinTime, _burstDelayMaxTime);
    }
}