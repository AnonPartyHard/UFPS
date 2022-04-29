using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AIUnitIKPointsController : MonoBehaviour
{
	[SerializeField] private Transform _rightHandIKTarget;
	[SerializeField] private Transform _leftHandIKTarget;
	[SerializeField] private Rig _weaponHolderRig;

	private AIUnitWeapon _weapon;

	public Rig WeaponHolderRig => _weaponHolderRig;
	private void Awake()
	{
		_weapon = GetComponent<AIUnitWeapon>();
	}

	private void Update()
	{
		HoldHandsOnWeapon();
	}

	private void HoldHandsOnWeapon()
	{
		if (_weapon.WeaponController != null)
		{
			_rightHandIKTarget.position = _weapon.WeaponController.RightHandIKPoint.position;
			_rightHandIKTarget.rotation = _weapon.WeaponController.RightHandIKPoint.rotation;
			_leftHandIKTarget.position = _weapon.WeaponController.LeftHandIKPoint.position;
			_leftHandIKTarget.rotation = _weapon.WeaponController.LeftHandIKPoint.rotation;
		}
	}
}