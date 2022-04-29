using UnityEngine;

public class OldFriendWeapon : BaseWeapon
{
	//only this weapon fields
	// private WeaponViewMono _weapon;
	// private GameObject _bullet;
	private float _lastShotTime;

	private float _fireRate;

	// private float _altFireRate;
	// private int _burstCount;
	// private int _burstFired = 0;

	private float _recoilStrength;
	private float _recoilQuench;

	private float _xRecoil = 0f;
	private float _yRecoil = 0f;
	private Vector3 _recoilVector = Vector3.zero;

	// private float _recoilSmooth;
	// private float _recoilStep = 0f;

	// private float _recoil = 0f;
	// private Vector3 _currentRecoilVector = Vector3.zero;

	public override void Draw(WeaponViewMono weapon)
	{
		weapon.Animator.Play("OldFriend_draw");
	}

	public override void Pick(WeaponViewMono weapon)
	{
		// _weapon = weapon;
		// _bullet = weapon.CustomFields.GetGameObject("OldFriendBullet");
		_fireRate = weapon.CustomFields.GetFloat("DefaultFireRate");
		// _altFireRate = weapon.CustomFields.GetFloat("AltFireRate");
		// _burstCount = weapon.CustomFields.GetInt("BurstCount");
		_recoilStrength = weapon.CustomFields.GetFloat("RecoilStrength");
		_recoilQuench = weapon.CustomFields.GetFloat("RcoilQuench");
		// _recoilSmooth = weapon.CustomFields.GetFloat("RecoilSmooth");
	}

	public override void FireHold(WeaponViewMono weapon)
	{
	}

	public override void AltFireHold(WeaponViewMono weapon)
	{
		if (Time.time >= _lastShotTime + _fireRate)
		{
			_lastShotTime = Time.time;
			Shot(weapon);
		}
	}

	public override void ReloadHold(WeaponViewMono weapon)
	{
	}

	public override void FireDown(WeaponViewMono weapon)
	{
		if (Time.time >= _lastShotTime + _fireRate)
		{
			_lastShotTime = Time.time;
			Shot(weapon);
		}
	}

	public override void AltFireDown(WeaponViewMono weapon)
	{
		// while (_burstFired <= _burstCount)
		// {
		// 	if (Time.time >= _lastShotTime + _fireRate)
		// 	{
		// 		_lastShotTime = Time.time;
		// 		Shot(weapon);
		// 		_burstFired++;
		// 	}
		// }

		// _burstFired = 0;
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
		weapon.Animator.Play("Fire", -1, 0f);
		ObjectsPool.instance.GetObject("of_bullet", weapon.FirePoint.position,
			weapon.FirePoint.rotation);

		if (_xRecoil < 180 - _recoilStrength)
			_xRecoil += _recoilStrength;
		
		_yRecoil += Random.value < 0.5f ? _recoilStrength : -_recoilStrength;
	}
}