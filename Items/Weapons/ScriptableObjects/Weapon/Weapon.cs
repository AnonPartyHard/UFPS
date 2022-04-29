using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon/Basic", order = 1)]
public class Weapon : ScriptableObject
{
    public enum WeaponTypes
    {
        Primary,
        Secondary,
        Melee
    }

    public enum WeaponFireModes
    {
        Auto,
        Manual,
        Multiple
    }

    [SerializeField] private string _name;
    [SerializeField] private GameObject _propModel;
    [SerializeField] private GameObject _viewModel;
    [SerializeField] private ProjectileData _bulletData;
    [SerializeField] private WeaponTypes _weaponType;
    [SerializeField] private RuntimeAnimatorController _runtimeAnimatorController;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _capacity;
    [SerializeField] private float _damage;
    [SerializeField] private float _recoilStrengh;
    [SerializeField] private WeaponFireModes _weaponFireMode;

    public string Name => _name;

    public GameObject PropModel => _propModel;

    public GameObject ViewModel => _viewModel;

    public WeaponTypes WeaponType => _weaponType;

    public RuntimeAnimatorController RuntimeAnimatorController => _runtimeAnimatorController;

    public ProjectileData BulletData => _bulletData;

    public float FireRate => _fireRate;

    public float Capacity => _capacity;

    public float Damage => _damage;

    public float RecoilStrengh => _recoilStrengh;

    public WeaponFireModes WeaponFireMode => _weaponFireMode;
}