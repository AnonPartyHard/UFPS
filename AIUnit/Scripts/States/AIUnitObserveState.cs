using System.Collections;
using UnityEngine;

public class AIUnitObserveState : AIUnitBaseState
{
	private float _observeAngle = 60f;
	private float _delay = 2f;
	private bool _turnRight = true;
	private Coroutine _coroutine;
	private Coroutine _stateCoroutine;

	private IEnumerator ObserveCoroutine(AiUnitStateManager unit)
	{
		yield return new WaitForSeconds(_delay);
		Turn(unit);
	}

	private IEnumerator StateCoroutine(AiUnitStateManager unit)
	{
		yield return new WaitForSeconds(Random.Range(6, 10));
		unit.SwitchState(unit.WalkState);
	}

	private void Turn(AiUnitStateManager unit)
	{
		_observeAngle = _turnRight
			? _observeAngle
			: -_observeAngle;
		_turnRight = !_turnRight;
		_coroutine = unit.StartCoroutine(ObserveCoroutine(unit));
	}

	public override void EnterState(AiUnitStateManager unit)
	{
		unit.Determinant.Agent.ResetPath();
		_coroutine = unit.StartCoroutine(ObserveCoroutine(unit));
		_stateCoroutine = unit.StartCoroutine(StateCoroutine(unit));
		unit.Determinant.Animator.SetTrigger("Observe");
	}

	public override void UpdateState(AiUnitStateManager unit)
	{
		Quaternion turn = Quaternion.Euler(0, _observeAngle, 0);
		unit.Determinant.Target.Sight.transform.localRotation =
			Quaternion.Slerp(unit.Determinant.Target.Sight.transform.localRotation, turn, 5f * Time.deltaTime);
	}

	public override void ExitState(AiUnitStateManager unit)
	{
		unit.StopCoroutine(_coroutine);
		unit.StopCoroutine(_stateCoroutine);
		unit.Determinant.Target.Sight.transform.localRotation = Quaternion.identity;
		unit.Determinant.Animator.ResetTrigger("Observe");
	}
}