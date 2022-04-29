using UnityEngine;

public class TemplateWeapon : BaseWeapon
{
	// public override string Name => "Template Weapon";
	// public override Types Type => Types.SECONDARY;
	// public override GameObject FP_Prefab { get; set; }
	// public override GameObject FS_Prefab { get; set; }
	// public override Animator AnimatorController { get; set; }
	// public override Transform FirePoint { get; set; }
	
	//only this weapon fields
	private float _tick = 0.1f;
	private float _lastTick;
	private bool _alt = false;
	private bool _burst = false;

	public override void Draw(WeaponViewMono weapon){}
	
	public override void Pick(WeaponViewMono weapon) {}

	public override void FireHold(WeaponViewMono weapon)
	{
		if (!_burst)
		{
			if (_lastTick + _tick < Time.time)
			{
				_lastTick = Time.time;
				GameObject b = weapon.CustomFields.GetGameObject("Bullet");
				GameObject ib = GameObject.Instantiate(b, weapon.FirePoint.position, Quaternion.identity);
				ib.GetComponent<Rigidbody>().AddForce(weapon.transform.forward * 1500f * Time.fixedDeltaTime, ForceMode.Impulse);
			}
		}
	}

	public override void AltFireHold(WeaponViewMono weapon)
	{
	}

	public override void ReloadHold(WeaponViewMono weapon)
	{
	}
	
	public override void FireDown(WeaponViewMono weapon)
	{
		if (_burst)
		{
			_lastTick = Time.time;
			for (int i = 0; i < 6; i++)
			{
				GameObject b = weapon.CustomFields.GetGameObject("Bullet");
				GameObject ib = GameObject.Instantiate(b, weapon.FirePoint.position, Quaternion.identity);
				ib.GetComponent<Rigidbody>().AddForce(weapon.transform.forward * 1500f * Time.fixedDeltaTime, ForceMode.Impulse);
			}
		}
	}

	public override void AltFireDown(WeaponViewMono weapon)
	{
		_alt = !_alt;
		_tick = _alt ? 0.05f : 0.1f;
	}

	public override void ReloadDown(WeaponViewMono weapon)
	{
		_burst = !_burst;
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
	}

	public override void Update(WeaponViewMono weapon)
	{
	}
	
	public override void FixedUpdate(WeaponViewMono weapon)
	{
	}
}