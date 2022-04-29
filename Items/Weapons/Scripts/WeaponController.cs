using System;
using System.Collections;
using UnityEngine;


public class WeaponController : MonoBehaviour
{
    [SerializeField] private Weapon _weaponData;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _rightHandIKPoint;
    [SerializeField] private Transform _leftHandIKPoint;

    private PlayerActionEvents _events;
    private AIUnitEvents _unitEvents;
    private float _lastShotTimeMark;

    public Weapon WeaponData => _weaponData;
    public Transform FirePoint
    {
        get => _firePoint;
        set => _firePoint = value;
    }

    public Transform RightHandIKPoint => _rightHandIKPoint;
    public Transform LeftHandIKPoint => _leftHandIKPoint;

    private void Awake()
    {
        _events = TryToGetComponent<PlayerActionEvents>();
        _unitEvents = TryToGetComponent<AIUnitEvents>();
        gameObject.name = _weaponData.Name;
    }

    private void Start()
    {
        ObjectsPool.instance.AddObjectsPool(_weaponData.BulletData.IdInObjectsPool, _weaponData.BulletData.Prefab,
            _weaponData.BulletData.CountInPool);
        ObjectsPool.instance.AddObjectsPool(_weaponData.BulletData.ParticleSystemIdInObjectsPool,
            _weaponData.BulletData.ParticleSystemPrefab, _weaponData.BulletData.CountInPool);
    }

    private void OnDestroy()
    {
        ClearObjectsPool();
    }

    private void OnDisable()
    {
        // ClearObjectsPool();
    }

    private T TryToGetComponent<T>()
    {
        return GetComponent<T>() == null
            ? GetComponentInParent<T>() == null
                ? GetComponentInChildren<T>()
                : GetComponentInParent<T>()
            : GetComponent<T>();
    }

    private void ClearObjectsPool()
    {
        ObjectsPool.instance.RemoveObjectsFromPool(_weaponData.BulletData.IdInObjectsPool, _weaponData.BulletData.CountInPool);
        ObjectsPool.instance.RemoveObjectsFromPool(_weaponData.BulletData.ParticleSystemIdInObjectsPool, _weaponData.BulletData.CountInPool);
    }

    public void Fire()
    {
        if (_lastShotTimeMark + _weaponData.FireRate < Time.time)
        {
            ObjectsPool.instance.GetObject(_weaponData.BulletData.IdInObjectsPool, _firePoint.position,
                _firePoint.rotation);
            _lastShotTimeMark = Time.time;
            _events?.WeaponShot(_weaponData);
            _unitEvents?.WeaponShot(_weaponData);
        }
    }
}