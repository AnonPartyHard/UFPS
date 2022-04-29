public abstract class MovementBaseState
{
	public abstract void EnterState(PlayerMovementStatesManager player);

	public abstract void UpdateState(PlayerMovementStatesManager player);
	
	public abstract void FixedUpdateState(PlayerMovementStatesManager player);
	
	public abstract void ExitState(PlayerMovementStatesManager player);
}
