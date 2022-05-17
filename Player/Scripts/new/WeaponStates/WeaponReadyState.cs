
public class WeaponReadyState : WeaponBaseState
{
	private void TrackForInputs(PlayerWeaponStatesManager weapon)
    {
		if (weapon.PlayerDeterminant.PlayerInput.IsKeyPressed(InputKeys.FIRE))
			weapon.CurrentWeaponMono.Weapon.FireDown(weapon.CurrentWeaponMono);

		if (weapon.PlayerDeterminant.PlayerInput.IsKeyPressed(InputKeys.DROP))
			weapon.CurrentWeaponMono.Weapon.Drop(weapon.CurrentWeaponMono);

		if (weapon.PlayerDeterminant.PlayerInput.IsKeyPressed(InputKeys.ALTFIRE))
			weapon.CurrentWeaponMono.Weapon.AltFireDown(weapon.CurrentWeaponMono);
	}
	public override void EnterState(PlayerWeaponStatesManager weapon)
	{
		if(weapon.CurrentWeaponMono != null)
			weapon.CurrentWeaponMono.Weapon.Draw(weapon.CurrentWeaponMono);
	}

	public override void UpdateState(PlayerWeaponStatesManager weapon)
	{
		TrackForInputs(weapon);
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
