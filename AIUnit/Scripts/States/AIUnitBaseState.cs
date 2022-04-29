using UnityEngine;

public abstract class AIUnitBaseState
{
	public abstract void EnterState(AiUnitStateManager unit);

	public abstract void UpdateState(AiUnitStateManager unit);
	
	public abstract void ExitState(AiUnitStateManager unit);

}
