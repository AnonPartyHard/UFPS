using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class ProjectileMono : MonoBehaviour
{
	[SerializeField] private string _projectileClassName;
	[SerializeField] private CustomFieldsSO _customFields;
	private BaseProjectile _projectile;

	public CustomFieldsSO CustomFields => _customFields;
	private void Awake()
	{
		if (_projectileClassName != null)
			_projectile = (BaseProjectile) Activator.CreateInstance(Type.GetType(_projectileClassName));
	}

	private void Start()
	{
		_projectile.Fired(this);
	}

	private void OnCollisionEnter(Collision collision)
	{
		_projectile.Collided(this, collision);
	}

	private void FixedUpdate()
	{
		_projectile.FixedUpdate(this);
	}

	private void OnEnable()
	{
		_projectile.Fired(this);
	}

	private void OnDisable()
	{
		_projectile.Destroyed(this);
	}

	private void OnDestroy()
	{
		_projectile.Destroyed(this);
	}
}
