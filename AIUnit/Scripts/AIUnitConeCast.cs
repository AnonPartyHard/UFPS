using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIUnitConeCast : MonoBehaviour
{
	[Header("If it is not AI Unit, assign sight props here")] [SerializeField]
	private float _radius;

	[SerializeField] private float _angle;
	[SerializeField] private LayerMask _sightLayers;
	[SerializeField] private LayerMask _enemiesLayers;

	[Header("If it is AI Unit, reassign sight props from SO")] [SerializeField]
	private AIUnitData _unitData;

	[Header("Basic fields")] [SerializeField]
	private AIUnitEvents _events;

	[Header("Cast frequency, WARNING - too small tick leads to bad performance!")] [SerializeField]
	private float _castTick = 0.5f;

	[SerializeField] private int _maxSphereOverlapColliders;
	[SerializeField] private LayerMask _sightLayersToIgnore;

	[Header("Debug sight.")] [SerializeField]
	private bool _debugSight = true;

	[SerializeField] private bool _debugSphere = true;
	[SerializeField] private bool _debugObservableObjects = true;

	private List<GameObject> _observableObjects;
	private List<GameObject> _enemies;
	private List<Collider> _inRadiusCols;
	private Collider[] cols;
	private int oldCount = 0;
	private int newCount = 0;
	private float lastTick = 0f;

	public List<GameObject> ObservableObjects => _observableObjects;
	public List<GameObject> EnemiesList => _enemies;

	private void Awake()
	{
		ApplyUnitProperties();
		cols = new Collider[_maxSphereOverlapColliders];
		_inRadiusCols = new List<Collider>();
		_enemies = new List<GameObject>();
		_observableObjects = new List<GameObject>();
	}

	private void ApplyUnitProperties()
	{
		_radius = _unitData.SightRadius;
		_angle = _unitData.SightAngle;
		_sightLayers = _unitData.SightLayers;
		_enemiesLayers = _unitData.EnemiesLayers;
	}

	private void Update()
	{
		if (Time.time > lastTick + _castTick)
		{
			lastTick = Time.time;
			Recast();
		}
	}

	public void Recast()
	{
		Cast();
		Track();
	}

	private void Cast()
	{
		newCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, cols, _sightLayers);

		if (oldCount != newCount)
		{
			if (newCount > 0)
			{
				Collider[] newCols = Physics.OverlapSphere(transform.position, _radius, _sightLayers);
				for (var i = 0; i < _inRadiusCols.Count; i++)
				{
					if (!newCols.Contains(_inRadiusCols[i]))
					{
						RemoveObservableObject(_inRadiusCols[i].gameObject);
						_inRadiusCols.Remove(_inRadiusCols[i]);
					}
				}

				foreach (Collider col in newCols)
					if (!_inRadiusCols.Contains(col))
						_inRadiusCols.Add(col);
			}
			else
			{
				ClearObservableObjects();
				_inRadiusCols.Clear();
			}

			oldCount = newCount;
		}
	}

	private void Track()
	{
		if (_inRadiusCols.Count > 0)
		{
			foreach (Collider col in _inRadiusCols)
			{
				GameObject go = col.gameObject;
				if (ObjectIsInSight(go) && NoObstaclesBetween(go, _sightLayersToIgnore))
					AddObservableObject(go);
				else
					RemoveObservableObject(go);
			}
		}
		else
		{
			ClearObservableObjects();
		}
	}

	public void RemoveObservableObject(GameObject obj)
	{
		if (ObjectIsInLayerMask(obj, _enemiesLayers))
		{
			if (_enemies.Contains(obj))
			{
				_enemies.Remove(obj);
				_events.EnemyLost(obj);
			}
		}
		else
		{
			if (_observableObjects.Contains(obj))
			{
				_observableObjects.Remove(obj);
				_events.ObjectLost(obj);
			}
		}
	}

	private void AddObservableObject(GameObject obj)
	{
		if (ObjectIsInLayerMask(obj, _enemiesLayers))
		{
			if (!_enemies.Contains(obj))
			{
				_enemies.Add(obj);
				_events.EnemyNoticed(obj);
			}
		}
		else
		{
			if (!_observableObjects.Contains(obj))
			{
				_observableObjects.Add(obj);
				_events.ObjectNoticed(obj);
			}
		}
	}

	private void ClearObservableObjects()
	{
		if (_enemies.Count > 0)
			for (int i = _enemies.Count - 1; i >= 0; i--)
				RemoveObservableObject(_enemies[i]);

		if (_observableObjects.Count > 0)
			for (int i = _observableObjects.Count - 1; i >= 0; i--)
				RemoveObservableObject(_observableObjects[i]);
	}

	private bool ObjectIsInSight(GameObject obj)
	{
		return Vector3.Angle(transform.forward, obj.transform.position - transform.position) <= _angle;
	}

	public bool NoObstaclesBetween(GameObject obj, LayerMask ignoreLayers)
	{
		Vector3 targetPos = obj.transform.position;
		Vector3 originPos = transform.position;
		RaycastHit hit;
		if (Physics.Linecast(originPos, targetPos, out hit, ~ignoreLayers))
			return GameObject.Equals(hit.collider.gameObject, obj);

		return false;
	}

	private bool ObjectIsInLayerMask(GameObject go, LayerMask _layers)
	{
		return _layers == (_layers | (1 << go.layer));
	}

	private void OnDrawGizmos()
	{
		if (_debugSphere)
		{
			Gizmos.color = new Color(1, 0, 0, 0.3f);
			Gizmos.DrawSphere(transform.position, _radius);
		}

		if (_debugSight)
		{
			float angle = _unitData ? _unitData.SightAngle : _angle;
			float radius = _unitData ? _unitData.SightRadius : _radius;
			GizmosDrawSight(angle, radius);
		}

		if (_debugObservableObjects && _observableObjects != null)
		{
			Gizmos.color = Color.yellow;
			if (_observableObjects.Count > 0)
				for (int i = 0; i < _observableObjects.Count; i++)
					Gizmos.DrawLine(transform.position, _observableObjects[i].transform.position);

			Gizmos.color = Color.red;
			if (_enemies.Count > 0)
				for (int i = 0; i < _enemies.Count; i++)
					Gizmos.DrawLine(transform.position, _enemies[i].transform.position);
		}
	}

	private void GizmosDrawSight(float angle, float radius)
	{
		Vector3 forward = transform.forward;
		Vector3 pos = transform.position;
		Vector3 firstEdge = Quaternion.Euler(0, -angle, 0) * forward;
		Vector3 secondEdge = Quaternion.Euler(0, angle, 0) * forward;
		Vector3 thirdEdge = Quaternion.Euler(-angle, 0, 0) * forward;
		Vector3 fourthdEdge = Quaternion.Euler(angle, 0, 0) * forward;
		Gizmos.color = Color.green;
		Gizmos.DrawRay(pos, firstEdge * radius);
		Gizmos.DrawRay(pos, secondEdge * radius);
		Gizmos.DrawRay(pos, thirdEdge * radius);
		Gizmos.DrawRay(pos, fourthdEdge * radius);
	}
}