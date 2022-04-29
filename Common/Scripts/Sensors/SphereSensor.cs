using System;
using UnityEngine;

public class SphereSensor : MonoBehaviour
{
	[SerializeField] private LayerMask _layerMask;
	[SerializeField] private float _distance;
	[SerializeField] private float _radius;
	[SerializeField] private Color _debugColor = Color.cyan;
	private RaycastHit _target;

	public RaycastHit Target => _target;

	public bool IsOverlap()
	{
		bool _isOverlap = Physics.SphereCast(transform.position, _radius, transform.forward, out _target, _distance,
			_layerMask);
		return _isOverlap;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = _debugColor;
		Gizmos.DrawLine(transform.position, transform.position + transform.forward * _distance);
		Gizmos.DrawSphere(transform.position + transform.forward * _distance, _radius);
	}
}