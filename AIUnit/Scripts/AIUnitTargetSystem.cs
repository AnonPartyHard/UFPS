using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIUnitDeterminant))]
public class AIUnitTargetSystem : MonoBehaviour
{
	[SerializeField] private Transform _wayPointsContainer;
	[SerializeField] private AIUnitConeCast _sight;
	private Transform _currentTarget;

	private Vector3 _lastSeenPoint;
	private List<Transform> _wayPoints;

	public Transform CurrentTarget => _currentTarget;
	public Vector3 LastSeenPoint => _lastSeenPoint;
	public AIUnitConeCast Sight => _sight;

	private void Awake()
	{
		_wayPoints = new List<Transform>();

		if (_wayPointsContainer != null)
		{
			foreach (Transform child in _wayPointsContainer)
				_wayPoints.Add(child.transform);
		}
		else
		{
			Debug.LogWarning(gameObject.name + " don't have waypoints!");
		}
			
	}

	public GameObject GetClosestEnemy()
	{
		if (_sight.EnemiesList.Count > 0)
		{
			GameObject closestEnemy = _sight.EnemiesList[0];
			float leastDistance = Vector3.Distance(transform.position, closestEnemy.transform.position);
			foreach (GameObject enemy in _sight.EnemiesList)
			{
				float distance = Vector3.Distance(transform.position, enemy.transform.position);
				if (distance < leastDistance)
				{
					closestEnemy = enemy;
					leastDistance = distance;
				}
			}
			return closestEnemy;
		}
		else
		{
			return null;
		}
	}

	public Transform GetRandomWaypoint()
	{
		return _wayPoints[Random.Range(0, _wayPoints.Count-1)]; 
	}

	public void RememberLastSeenPoint(Vector3 point)
	{
		_lastSeenPoint = point;
	}
}