using System;
using UnityEngine;

public class MovementWallSlideState : MovementBaseState
{
	private SphereSensor _activeRay;
	private Vector3 _originVelocity;

	private void TrackForInput(PlayerMovementStatesManager player)
	{
		if (player.Determinant.PlayerInput.IsKeyReleased(InputKeys.JUMP))
		{
			player.Determinant.Rigidbody.AddForce(
				(-_activeRay.transform.forward * player.Determinant.PlayerSetups.WallPushForce *
					player.Determinant.PlayerSetups.JumpForceMultiplier * Time.fixedDeltaTime) +
				Vector3.up * player.Determinant.PlayerSetups.WallPushForce *
				player.Determinant.PlayerSetups.JumpForceMultiplier * Time.fixedDeltaTime, ForceMode.Impulse);

			player.SwitchState(player.InAirState);
		}
	}

	public override void EnterState(PlayerMovementStatesManager player)
	{
		_activeRay = player.Determinant.RightWallSensor.IsOverlap()
			? player.Determinant.RightWallSensor
			: player.Determinant.LeftWallSensor;
		_originVelocity = player.Determinant.Rigidbody.velocity;
		_originVelocity.x = 0;
		_originVelocity.y = 0;

		player.Determinant.Rigidbody.useGravity = false;
		player.IsWallSliding = true;
		// player.Determinant.PlayerCamera.Lock(60f, 60f, Quaternion.identity);
	}

	public override void UpdateState(PlayerMovementStatesManager player)
	{
		TrackForInput(player);

		if (!_activeRay.IsOverlap())
			player.SwitchState(player.InAirState);

		if (player.transform.InverseTransformDirection(player.Determinant.Rigidbody.velocity).z < 1f)
		{
			player.IsWallSliding = false;
			player.SwitchState(player.InAirState);
		}
	}

	public override void FixedUpdateState(PlayerMovementStatesManager player)
	{
		Vector3 dir = Quaternion.AngleAxis(90f * _activeRay.transform.up.y, Vector3.up) * _activeRay.Target.normal;

		player.Determinant.Rigidbody.AddForce(
			(dir * player.Determinant.PlayerSetups.WallSlideSpeed * Time.fixedDeltaTime) -
			_activeRay.Target.normal * 20f,
			ForceMode.Acceleration);

		player.Determinant.PlayerCamera.RotationOffsetUpdate(
			Quaternion.Euler(Vector3.forward * _activeRay.transform.up.y *
				player.Determinant.CameraSetups.CameraWallRunOffset),
			player.Determinant.CameraSetups.CameraTransitionsSmooth);

		//CAMERA NOISE
		player.Determinant.CameraShaker.SetGain(0f, 0f, player.Determinant.CameraSetups.CameraTransitionsSmooth);

		// player.Determinant.PlayerCamera.UpdateLockRotation(Quaternion.LookRotation(dir),
		//     player.Determinant.PlayerSetups.CameraTransitionsSmooth);
	}

	public override void ExitState(PlayerMovementStatesManager player)
	{
		player.Determinant.Rigidbody.useGravity = true;
		// player.Determinant.PlayerCamera.Unlock();
	}
}