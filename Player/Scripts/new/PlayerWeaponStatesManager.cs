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
    private WeaponViewMono _currentWeapon;
    private WeaponIdentifier _pickingWeapon;
    private WeaponBaseState _currentState;

    public PlayerDeterminant Determinant => _determinant;
    public WeaponViewMono SecondaryWeapon => _secondaryWeapon;
    public WeaponViewMono PrimaryyWeapon => _primaryyWeapon;
    public WeaponViewMono CurrentWeapon => _currentWeapon;

    //DEBUG
    [SerializeField] private Text _stateText;

    private void Start()
    {
    }

    private void Update()
    {
        LookForInpit();
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

        _animator.runtimeAnimatorController = instatiatedMono.AnimatorController;
        instatiatedMono.Animator = _animator;
        instatiatedMono.PlayerDeterminant = _determinant;
        _currentWeapon = instatiatedMono;
        _currentWeapon.Weapon.Pick(_currentWeapon);
        Destroy(_pickingWeapon.gameObject);
        _pickingWeapon = null;
    }

    private void SwitchState(WeaponBaseState state)
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