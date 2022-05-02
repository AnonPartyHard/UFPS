using System.Collections;
using UnityEngine;

public class OldFriendWeapon : BaseWeapon
{
	//only this weapon fields
	private float _lastShotTime;
	private float _fireRate;
	private float _recoilStrength;
	private float _recoilQuench;

	private float _xRecoil = 0f;
	private float _yRecoil = 0f;
	private Vector3 _recoilVector = Vector3.zero;

	private int _burstFired = 0;
	private int _burstCount = 3;
	private float _readyTime = 0.3f;

	public override void Draw(WeaponViewMono weapon)
	{
		weapon.Animator.Play("OldFriend_draw");
		weapon.StartCoroutine(ReadyCoroutine(weapon));
	}

	public override void Pick(WeaponViewMono weapon)
	{
		_fireRate = weapon.CustomFields.GetFloat("DefaultFireRate");
		_recoilStrength = weapon.CustomFields.GetFloat("RecoilStrength");
		_recoilQuench = weapon.CustomFields.GetFloat("RcoilQuench");
	}

	public override void FireHold(WeaponViewMono weapon)
	{
	}

	public override void AltFireHold(WeaponViewMono weapon)
	{
		// if (Time.time >= _lastShotTime + _fireRate)
		// {
		// 	_lastShotTime = Time.time;
		// 	Shot(weapon);
		// }
	}

	public override void ReloadHold(WeaponViewMono weapon)
	{
	}

	public override void FireDown(WeaponViewMono weapon)
	{
		if (Time.time >= _lastShotTime + _fireRate)
			Shot(weapon);
	}

	public override void AltFireDown(WeaponViewMono weapon)
	{
		while (_burstFired <= _burstCount)
		{
			Shot(weapon);
			_burstFired++;
		}

		_burstFired = 0;
	}

	public override void ReloadDown(WeaponViewMono weapon)
	{
	}

	public override void FireRelease(WeaponViewMono weapon)
	{
	}

	public override void AltFireRelease(WeaponViewMono weapon)
	{
	}

	public override void ReloadRelease(WeaponViewMono weapon)
	{
	}

	public override void Drop(WeaponViewMono weapon)
	{
		GameObject dropped =
			GameObject.Instantiate(weapon.FS_Prefab, weapon.transform.position, weapon.transform.rotation);
		dropped.GetComponent<Rigidbody>()
			.AddForce(weapon.transform.forward * 200f * Time.fixedDeltaTime, ForceMode.Impulse);
		GameObject.Destroy(weapon.gameObject);
		//must be done from weapon state manager
		weapon.Animator.runtimeAnimatorController = null;
	}

	public override void Update(WeaponViewMono weapon)
	{
	}

	public override void FixedUpdate(WeaponViewMono weapon)
	{
		if (_xRecoil > 0f)
		{
			_recoilVector = new Vector3(_xRecoil, _yRecoil, 0f);

			_xRecoil -= _recoilQuench;

			if (_yRecoil < 0f)
				_yRecoil += _recoilQuench;
			else
				_yRecoil -= _recoilQuench;

			Quaternion _recoilRotation = Quaternion.Euler(-_recoilVector);
			_recoilRotation.z = 0;
			weapon.PlayerDeterminant.PlayerCamera.RotationOffsetUpdate(_recoilRotation, 20f * Time.fixedDeltaTime);
		}
	}

	private void Shot(WeaponViewMono weapon)
	{
		_lastShotTime = Time.time;

		weapon.Animator.Play("Fire", -1, 0f);
		ObjectsPool.instance.GetObject("of_bullet", weapon.FirePoint.position,
			weapon.FirePoint.rotation);

		if (_xRecoil < 180 - _recoilStrength)
			_xRecoil += _recoilStrength;

		_yRecoil += Random.value < 0.5f ? _recoilStrength : -_recoilStrength;
	}

	//Custom methods
	private IEnumerator ReadyCoroutine(WeaponViewMono weapon)
	{
		yield return new WaitForSeconds(_readyTime);
		weapon.PlayerWeaponStatesManager.SwitchState(weapon.PlayerWeaponStatesManager.ReadyState);
	}
}