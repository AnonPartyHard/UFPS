using System;
using UnityEngine;

public class MovementWallSlideState : MovementBaseState
{
	private SphereSensor _activeRay;
	private Vector3 dir;

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
		player.Determinant.Rigidbody.useGravity = false;
		player.IsWallSliding = true;

		player.Determinant.PlayerRepresentationAnimator.BoundConstraintsToTargets();

		if (player.Determinant.RightWallSensor.IsOverlap())
        {
            player.Determinant.PlayerRepresentationAnimator.CrossFade("WallSlide_R", 0.2f, new int[1] { 0 });
            _activeRay = player.Determinant.RightWallSensor;

		} else
        {
            player.Determinant.PlayerRepresentationAnimator.CrossFade("WallSlide_L", 0.2f, new int[1] { 0 });
            _activeRay = player.Determinant.LeftWallSensor;
		}
	}

	public override void UpdateState(PlayerMovementStatesManager player)
	{
        TrackForInput(player);

		player.Determinant.PlayerRepresentationAnimator.AdjustRepresentationRotation(Quaternion.LookRotation(dir), 5f);

		if (!_activeRay.IsOverlap())
			player.SwitchState(player.InAirState);

		if (player.Determinant.PlayerCamera.CameraPivot.InverseTransformDirection(player.Determinant.Rigidbody.velocity).z < 1f)
		{
			player.IsWallSliding = false;
			player.SwitchState(player.InAirState);
		}
	}

	public override void FixedUpdateState(PlayerMovementStatesManager player)
	{
		dir = Quaternion.AngleAxis(90f * _activeRay.transform.up.y, Vector3.up) * _activeRay.Target.normal;

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
	}

	public override void ExitState(PlayerMovementStatesManager player)
	{
		player.Determinant.Rigidbody.useGravity = true;
		player.Determinant.PlayerRepresentationAnimator.UnBoundConstraints();
	}
}