using System;
using UnityEngine;
using UnityEngine.UI;


public class PlayerWeaponStatesManager : MonoBehaviour
{
    [SerializeField] private PlayerDeterminant _playerDeterminant;
    [SerializeField] private RaySensor _raySensor;
    [SerializeField] private Transform _weaponContainer;
    [SerializeField] private Animator _weaponAnimator;
    //[SerializeField] private WeaponViewMono _bareHandsWeapon;
    
    private WeaponViewMono _secondaryWeapon;
    private WeaponViewMono _primaryyWeapon;
    private WeaponViewMono _currentWeaponMono;
    private WeaponIdentifier _pickingWeapon;
    private WeaponBaseState _currentState;

    public WeaponReadyState ReadyState = new WeaponReadyState();
    private WeaponViewMono _weapon;
    public PlayerDeterminant PlayerDeterminant => _playerDeterminant;
    // public WeaponViewMono SecondaryWeapon => _secondaryWeapon;
    // public WeaponViewMono PrimaryyWeapon => _primaryyWeapon;
    public WeaponViewMono CurrentWeaponMono => _currentWeaponMono;

    //DEBUG
    [SerializeField] private Text _stateText;

    private void Start()
    {
        //_playerDeterminant.MovementStateChangeEventChannel.onStateChanged += MovementStateChanged;
        SwitchState(ReadyState);
    }

    private void OnDestroy()
    {
        //_playerDeterminant.MovementStateChangeEventChannel.onStateChanged -= MovementStateChanged;
    }

    //private void MovementStateChanged(MovementBaseState newState)
    //{
    //}

    private void Update()
    {
        TrackForInputs();
        if(_currentWeaponMono != null)
            _currentState.UpdateState(this);

        //_playerDeterminant.PlayerRepresentationAnimator.RightArmIK.position = _weapon.RightHandIKPoint.position;
        //_playerDeterminant.PlayerRepresentationAnimator.LeftArmIK.position = _weapon.LeftHandIKPoint.position;
    }

    private void FixedUpdate()
    {
        if (_currentWeaponMono != null)
            _currentState.FixedUpdateState(this);
    }

    private void TrackForInputs()
    {
        bool pick = _playerDeterminant.PlayerInput.IsKeyPressed(InputKeys.ACTION) && _raySensor.IsHit() &&
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
        SwitchState(ReadyState);
    }

    private void DrawWeapon(WeaponViewMono weapon)
    {
        _currentWeaponMono = weapon;
        _weaponAnimator.runtimeAnimatorController = _currentWeaponMono.AnimatorController;
        _currentWeaponMono.Animator = _weaponAnimator;
        _currentWeaponMono.PlayerDeterminant = _playerDeterminant;
        _currentWeaponMono.PlayerWeaponStatesManager = this;
        _currentWeaponMono.Weapon.Pick(_currentWeaponMono);

        //_playerDeterminant.PlayerRepresentationAnimator.GiveControllToPoints(new RigLimbs[2] { RigLimbs.RIGHT_ARM, RigLimbs.LEFT_ARM });
        //_playerDeterminant.PlayerRepresentationAnimator.CrossFade("Default", 0.2f, new int[1] { 1 });
        _weapon = weapon;
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