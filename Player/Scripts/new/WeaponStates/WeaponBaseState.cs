public abstract class WeaponBaseState
{
	public abstract void EnterState(PlayerWeaponStatesManager weapon);

	public abstract void UpdateState(PlayerWeaponStatesManager weapon);
	
	public abstract void FixedUpdateState(PlayerWeaponStatesManager weapon);
	
	public abstract void ExitState(PlayerWeaponStatesManager weapon);
}
