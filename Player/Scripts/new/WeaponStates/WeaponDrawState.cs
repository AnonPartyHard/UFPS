using UnityEngine;

public class WeaponDrawState : WeaponBaseState
{
	public override void EnterState(PlayerWeaponStatesManager weapon)
	{
		weapon.CurrentWeaponMono.Weapon.Draw(weapon.CurrentWeaponMono);
	}

	public override void UpdateState(PlayerWeaponStatesManager weapon)
	{
	}

	public override void FixedUpdateState(PlayerWeaponStatesManager weapon)
	{
	}

	public override void ExitState(PlayerWeaponStatesManager weapon)
	{
	}
}