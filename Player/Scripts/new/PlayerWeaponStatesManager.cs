using System;
using UnityEngine;
using UnityEngine.UI;


public class PlayerWeaponStatesManager : MonoBehaviour
{
    [SerializeField] private PlayerDeterminant _determinant;
    [SerializeField] private RaySensor _raySensor;
    [SerializeField] private Transform _weaponContainer;
    [SerializeField] private Animator _animator;

    private WeaponViewMono _secondaryWeapon;
    private WeaponViewMono _primaryyWeapon;
    private WeaponViewMono _currentWeaponMono;
    private WeaponIdentifier _pickingWeapon;
    private WeaponBaseState _currentState;

    public WeaponDrawState DrawState = new WeaponDrawState();
    public WeaponReadyState ReadyState = new WeaponReadyState();

    public PlayerDeterminant Determinant => _determinant;
    // public WeaponViewMono SecondaryWeapon => _secondaryWeapon;
    // public WeaponViewMono PrimaryyWeapon => _primaryyWeapon;
    public WeaponViewMono CurrentWeaponMono => _currentWeaponMono;

    //DEBUG
    [SerializeField] private Text _stateText;

    private void Start()
    {
        _determinant.MovementStateChangeEventChannel.onStateChanged += MovementStateChanged;
        SwitchState(DrawState);
    }

    private void OnDestroy()
    {
        _determinant.MovementStateChangeEventChannel.onStateChanged -= MovementStateChanged;
    }

    private void MovementStateChanged(MovementBaseState newState)
    {
        // Debug.Log(newState);
    }

    private void Update()
    {
        LookForInpit();
        _currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        _currentState.FixedUpdateState(this);
    }

    private void LookForInpit()
    {
        bool pick = _determinant.PlayerInput.IsKeyPressed(InputKeys.ACTION) && _raySensor.IsHit() &&
            _raySensor.Target.collider.gameObject.TryGetComponent(out _pickingWeapon);

        bool close = _pickingWeapon != null &&
            Vector3.Distance(transform.position, _pickingWeapon.transform.position) < 1.25f;

        if (pick)
            _pickingWeapon.GetComponent<Rigidbody>().AddForce(
                (_raySensor.transform.position - _pickingWeapon.transform.position) +
                Vector3.up * 50f * Time.fixedDeltaTime,
                ForceMode.Impulse);

        if (close)
            EquipWeapon(_pickingWeapon);
    }

    private void EquipWeapon(WeaponIdentifier weapon)
    {
        WeaponViewMono instatiatedMono = Instantiate(weapon.FP_Prefab, _weaponContainer).GetComponent<WeaponViewMono>();
        instatiatedMono.gameObject.name = instatiatedMono.WeaponName;
        if (instatiatedMono.WeaponType == WeaponTypes.SECONDARY)
            _secondaryWeapon = instatiatedMono;

        if (instatiatedMono.WeaponType == WeaponTypes.PRIMARY)
            _primaryyWeapon = instatiatedMono;

        DrawWeapon(instatiatedMono);
        Destroy(_pickingWeapon.gameObject);
        _pickingWeapon = null;
        SwitchState(DrawState);
    }

    private void DrawWeapon(WeaponViewMono weapon)
    {
        _currentWeaponMono = weapon;
        _animator.runtimeAnimatorController = _currentWeaponMono.AnimatorController;
        _currentWeaponMono.Animator = _animator;
        _currentWeaponMono.PlayerDeterminant = _determinant;
        _currentWeaponMono.PlayerWeaponStatesManager = this;
        _currentWeaponMono.Weapon.Pick(_currentWeaponMono);
    }

    public void SwitchState(WeaponBaseState state)
    {
        if (_currentState != null)
            _currentState.ExitState(this);

        _currentState = state;
        _currentState.EnterState(this);

        //DEBUG
        if (_stateText != null)
        {
            string stateName = _currentState.ToString();
            string FinalString;
            int Pos1 = stateName.IndexOf("Weapon") + "Weapon".Length;
            int Pos2 = stateName.IndexOf("State");
            FinalString = stateName.Substring(Pos1, Pos2 - Pos1);

            _stateText.text = FinalString;
        }
    }
}