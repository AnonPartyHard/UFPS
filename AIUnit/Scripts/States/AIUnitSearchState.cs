using UnityEngine;
using UnityEngine.AI;

public class AIUnitSearchState : AIUnitBaseState
{
	private NavMeshAgent _agent;
	private Vector3 _lastSeenPoint;
	public override void EnterState(AiUnitStateManager unit)
	{
		_lastSeenPoint = unit.Determinant.Target.LastSeenPoint;
		_agent = unit.Determinant.Agent;
		_agent.speed = unit.Determinant.UnitData.ChasingMoveSpeed;
		_agent.destination = _lastSeenPoint;
		unit.Determinant.Animator.SetTrigger("Search");
	}

	public override void UpdateState(AiUnitStateManager unit)
	{
		if (Vector3.Distance(unit.transform.position, _lastSeenPoint) <= _agent.stoppingDistance + 1f)
			unit.SwitchState(unit.ObserveState);
	}

	public override void ExitState(AiUnitStateManager unit)
	{
		_agent = null;
		_lastSeenPoint = Vector3.zero;
		unit.Determinant.Animator.ResetTrigger("Search");
	} 
}
