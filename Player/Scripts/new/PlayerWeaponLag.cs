using System;
using UnityEngine;

public class PlayerWeaponLag : MonoBehaviour
{
	[SerializeField] private Transform _camAnchor;
	[SerializeField] private PlayerDeterminant _determinant;
	[SerializeField] private float _smoothFactor = 1f;
	[SerializeField] private float _inputDelimiter = 15f;
	[SerializeField] private float _velocityDelimiter = 50f;
	private Vector3 _offset;
	private Vector3 _target;

	private void Awake()
	{
		_offset = Vector3.zero;
		_target = Vector3.zero; 
	}

	private void Update()
	{
		CalculateOffset();
		transform.localPosition = _camAnchor.localPosition + _offset;
		transform.localRotation = _camAnchor.localRotation;
	}

	private void CalculateOffset()
	{
		_target = new Vector3(_determinant.PlayerInput.GetMouseVector().x / _inputDelimiter,
			(-_determinant.PlayerInput.GetMouseVector().y / _inputDelimiter) +
			(_determinant.Rigidbody.velocity.y / _velocityDelimiter), 0f);

		_offset = Vector3.Lerp(_offset, -_target, _smoothFactor * Time.fixedDeltaTime);
	}
}