using System.Collections;
using UnityEngine;

public class AIUnitWeapon : MonoBehaviour
{
	[SerializeField] private Transform _gunHolder;
	private AIUnitDeterminant _determinant;
	private WeaponController _weaponController;
	private AIUnitEvents _events;
	private bool _delay;
	private int _currentBurstCount = 0;
	private int _currentMaxBurstCount = 0;

	public Transform GunHolder => _gunHolder;
	public WeaponController WeaponController => _weaponController;

	private void Awake()
	{
		_determinant = GetComponent<AIUnitDeterminant>();
		_weaponController = Instantiate(_determinant.UnitData.Weapon.ViewModel, _gunHolder)
			.GetComponent<WeaponController>();
		
		_currentMaxBurstCount = _determinant.UnitData.GetRandomBurstCount();
		_events = GetComponent<AIUnitEvents>();
		_events.onWeaponShot += WeaponShot;
		_delay = false;
	}

	private void OnDestroy()
	{
		_events.onWeaponShot -= WeaponShot;
	}

	private void WeaponShot(Weapon weapon)
	{
		_currentBurstCount++;
		if (_currentBurstCount >= _currentMaxBurstCount)
			StartCoroutine(DelayCoroutine(_determinant.UnitData.GetRandomBurstDelay()));
	}

	public void Fire()
	{
		if (!_delay)
			_weaponController.Fire();
	}

	private IEnumerator DelayCoroutine(float time)
	{
		_delay = true;
		yield return new WaitForSeconds(time);
		_currentMaxBurstCount = _determinant.UnitData.GetRandomBurstCount();
		_currentBurstCount = 0;
		_delay = false;
	}
}