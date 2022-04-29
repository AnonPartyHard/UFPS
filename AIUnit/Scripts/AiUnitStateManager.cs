using System;
using UnityEngine;

public class AiUnitStateManager : MonoBehaviour
{
    private AIUnitDeterminant _determinant;
    public AIUnitBaseState _currentState;
    public AIUnitWalkState WalkState = new AIUnitWalkState();
    public AIUnitObserveState ObserveState = new AIUnitObserveState();
    public AIUnitAttackState AttackState = new AIUnitAttackState();
    public AIUnitSearchState SearchState = new AIUnitSearchState();

    //DEBUG
    public string _currentStateName = "";

    public AIUnitDeterminant Determinant => _determinant;

    private void Awake()
    {
        _determinant = GetComponent<AIUnitDeterminant>();
    }

    private void Start()
    {
        _currentState = ObserveState;
        _currentState.EnterState(this);
        _currentStateName = _currentState.ToString();

        _determinant.Events.onEnemyNoticed += EnemyNoticed;
        _determinant.Events.onEnemyLost += EnemyLost;
    }

    private void OnDestroy()
    {
        _determinant.Events.onEnemyNoticed -= EnemyNoticed;
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    private void EnemyNoticed(GameObject enemyCollider)
    {
        if (_currentState != AttackState)
            SwitchState(AttackState);
    }

    private void EnemyLost(GameObject enemyCollider)
    {
        if (this.enabled)
        {
            GameObject anotherEnemy = _determinant.Target.GetClosestEnemy();
            if (anotherEnemy == null)
            {
                _determinant.Target.RememberLastSeenPoint(enemyCollider.transform.position);
                SwitchState(SearchState);
            }
            else
            {
                SwitchState(AttackState);
            }
        }
    }

    public void SwitchState(AIUnitBaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
        _currentStateName = _currentState.ToString();
    }
}