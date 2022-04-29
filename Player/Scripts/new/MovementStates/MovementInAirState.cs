using System;
using UnityEngine;
using System.Linq;

public class MovementInAirState : MovementBaseState
{
	private void TrackForInput(PlayerMovementStatesManager player)
	{
		if (player.Determinant.PlayerInput.GetMovementVector().y < 1)
			player.IsSprinting = false;

		if (player.IsWallSliding)
		{
			if (player.Determinant.PlayerInput.IsKeyDown(InputKeys.JUMP))

				if (player.Determinant.RightWallSensor.IsOverlap() || player.Determinant.LeftWallSensor.IsOverlap() &&
				    player.Determinant.Rigidbody.velocity.z != 0)
					player.SwitchState(player.WallSlideState);
		}
		else
		{
			if (player.Determinant.PlayerInput.IsKeyPressed(InputKeys.JUMP))
				if (player.Determinant.RightWallSensor.IsOverlap() || player.Determinant.LeftWallSensor.IsOverlap() &&
				    player.Determinant.Rigidbody.velocity.z != 0)
					player.SwitchState(player.WallSlideState);
		}

		if (player.Determinant.PlayerInput.IsKeyDown(InputKeys.JUMP) &&
		    player.Determinant.ForwardWallSensor.IsOverlap() && player.CanClimb)
			player.SwitchState(player.WallClimbState);
	}

	public override void EnterState(PlayerMovementStatesManager player)
	{
		player.Determinant.Rigidbody.drag = player.Determinant.PlayerSetups.AirDrag;
		player.Determinant.RightWallSensor.gameObject.SetActive(true);
		player.Determinant.LeftWallSensor.gameObject.SetActive(true);
	}

	public override void UpdateState(PlayerMovementStatesManager player)
	{
		TrackForInput(player);

		if (player.Determinant.GroundSensor.IsOverlap())
		{
			if (!player.IsSprinting)
				player.SwitchState(player.IdleState);
			else
				player.SwitchState(player.SprintState);

			player.CanClimb = true;
		}
	}

	public override void FixedUpdateState(PlayerMovementStatesManager player)
	{
		player.Determinant.Rigidbody.AddForce(
			player.MovementForce.normalized * player.Determinant.PlayerSetups.AirMoveSpeed *
			player.Determinant.PlayerSetups.AirControlMultiplier * Time.fixedDeltaTime,
			ForceMode.Acceleration);

		player.Determinant.PlayerCamera.RotationOffsetUpdate(
			Quaternion.Euler(
				player.Determinant.Rigidbody.velocity.y * player.Determinant.CameraSetups.CamJumpOffsetMultiplier, 0,
				0),
			player.Determinant.CameraSetups.CameraTransitionsSmooth);
		//CAMERA NOISE
		player.Determinant.CameraShaker.SetGain(0f, 0f, player.Determinant.CameraSetups.CameraTransitionsSmooth);
	}

	public override void ExitState(PlayerMovementStatesManager player)
	{
		player.Determinant.Rigidbody.drag = player.Determinant.PlayerSetups.GroundDrag;
	}
}