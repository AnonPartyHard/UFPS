using UnityEngine;
using UnityEngine.AI;

public class AIUnitWalkState : AIUnitBaseState
{
	private Transform _currentWaypoint;

	public override void EnterState(AiUnitStateManager unit)
	{
		_currentWaypoint = unit.Determinant.Target.GetRandomWaypoint();
		unit.Determinant.Agent.speed = unit.Determinant.UnitData.PatrolMoveSpeed;
		unit.Determinant.Agent.destination = _currentWaypoint.position;
		
		unit.Determinant.Animator.SetTrigger("Walk");
	}

	public override void UpdateState(AiUnitStateManager unit)
	{
		if (Vector3.Distance(unit.transform.position, _currentWaypoint.position) <=
		    unit.Determinant.Agent.stoppingDistance + 1f)
			unit.SwitchState(unit.ObserveState);
	}

	public override void ExitState(AiUnitStateManager unit)
	{
		unit.Determinant.Animator.ResetTrigger("Walk");
	}
}