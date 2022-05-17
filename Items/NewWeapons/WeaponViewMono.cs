using System;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponTypes
{
    PRIMARY,
    SECONDARY,
    BARE_HANDS
}

[System.Serializable]
public class ObjectToPool
{
    [SerializeField] private string _name;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private int _count;
    
    public string Name => _name;
    public GameObject GameObject => _gameObject;
    public int Count => _count;
}

public class WeaponViewMono : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private string _weaponClassName;
    [SerializeField] private WeaponTypes _type;
    [SerializeField] private GameObject _fsPrefab;
    [SerializeField] private RuntimeAnimatorController _animatorController;
    [SerializeField] private CustomFieldsSO _customFields;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private List<ObjectToPool> _objectsToPool;

    [Header("IK Points to link with animation rigs")]
    [SerializeField] private IKHandPoints _rightHandIKPoints;
    [SerializeField] private IKHandPoints _leftHandIKPoints;

    private PlayerWeaponStatesManager _playerWeaponStatesManager;
    private PlayerDeterminant _playerDeterminant;
    private Animator _animator;
    private BaseWeapon _weapon;
    public string WeaponName => _name;
    public WeaponTypes WeaponType => _type;
    public CustomFieldsSO CustomFields => _customFields;
    public Transform FirePoint => _firePoint;
    public GameObject FS_Prefab => _fsPrefab;

    public IKHandPoints RightHandIKPoints => _rightHandIKPoints;
    public IKHandPoints LeftHandIKPoints => _leftHandIKPoints;


    public Animator Animator
    {
        get { return _animator; }
        set { _animator = value; }
    }
    public RuntimeAnimatorController AnimatorController => _animatorController;
    public BaseWeapon Weapon => _weapon;

    public PlayerDeterminant PlayerDeterminant
    {
        get { return _playerDeterminant; }
        set { _playerDeterminant = value; }
    }
    public PlayerWeaponStatesManager PlayerWeaponStatesManager
    {
        get { return _playerWeaponStatesManager; }
        set { _playerWeaponStatesManager = value; }
    }
    

    private void Awake()
    {
        if (_weaponClassName != null)
            _weapon = (BaseWeapon) Activator.CreateInstance(Type.GetType(_weaponClassName));
    }

    private void Start()
    {
        foreach (ObjectToPool obj in _objectsToPool)
            ObjectsPool.instance.AddObjectsPool(obj.Name, obj.GameObject, obj.Count);
    }
}