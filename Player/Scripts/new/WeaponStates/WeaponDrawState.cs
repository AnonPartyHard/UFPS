using UnityEngine;

public class WeaponDrawState : WeaponBaseState
{
	private float _timeMark;

	public override void EnterState(PlayerWeaponStatesManager weapon)
	{
		weapon.CurrentWeapon.Weapon.Draw(weapon.CurrentWeapon);
		_timeMark = Time.time;
	}

	public override void UpdateState(PlayerWeaponStatesManager weapon)
	{
		if(Time.time > _timeMark + 0.3f)
			weapon.SwitchState(weapon.ReadyState);
	}

	public override void FixedUpdateState(PlayerWeaponStatesManager weapon)
	{
	}

	public override void ExitState(PlayerWeaponStatesManager weapon)
	{
	}
}