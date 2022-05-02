
using UnityEngine;

public class WeaponReadyState : WeaponBaseState
{
	public override void EnterState(PlayerWeaponStatesManager weapon)
	{
		
	}

	public override void UpdateState(PlayerWeaponStatesManager weapon)
	{
		if(weapon.Determinant.PlayerInput.IsKeyPressed(InputKeys.FIRE))
			weapon.CurrentWeaponMono.Weapon.FireDown(weapon.CurrentWeaponMono);
		
		if(weapon.Determinant.PlayerInput.IsKeyPressed(InputKeys.DROP))
			weapon.CurrentWeaponMono.Weapon.Drop(weapon.CurrentWeaponMono);
		
		if(weapon.Determinant.PlayerInput.IsKeyPressed(InputKeys.ALTFIRE))
			weapon.CurrentWeaponMono.Weapon.AltFireDown(weapon.CurrentWeaponMono);
		
		weapon.CurrentWeaponMono.Weapon.Update(weapon.CurrentWeaponMono);
	}

	public override void FixedUpdateState(PlayerWeaponStatesManager weapon)
	{
		weapon.CurrentWeaponMono.Weapon.FixedUpdate(weapon.CurrentWeaponMono);
	}

	public override void ExitState(PlayerWeaponStatesManager weapon)
	{
		
	}
}
