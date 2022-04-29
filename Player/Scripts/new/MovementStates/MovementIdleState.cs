using UnityEngine;

public class MovementIdleState : MovementBaseState
{
	private void TrackForInputs(PlayerMovementStatesManager player)
	{
		if (player.Determinant.PlayerInput.IsKeyPressed(InputKeys.JUMP) && player.Determinant.GroundSensor.IsOverlap())
			player.Determinant.Rigidbody.AddForce(
				Vector3.up * player.Determinant.PlayerSetups.JumpForce *
				player.Determinant.PlayerSetups.JumpForceMultiplier * Time.fixedDeltaTime,
				ForceMode.Impulse);
			

		if (player.Determinant.PlayerInput.IsKeyDown(InputKeys.DUCK))
			player.SwitchState(player.DuckState);

		if (player.Determinant.PlayerInput.GetMovementVector().magnitude > 0)
			player.SwitchState(player.RunState);
	}

	public override void EnterState(PlayerMovementStatesManager player)
	{
		player.Determinant.Rigidbody.drag = player.Determinant.PlayerSetups.GroundDrag;
		player.IsWallSliding = false;
		player.Determinant.RightWallSensor.gameObject.SetActive(false);
		player.Determinant.LeftWallSensor.gameObject.SetActive(false);
	}

	public override void UpdateState(PlayerMovementStatesManager player)
	{
		TrackForInputs(player);
	}

	public override void FixedUpdateState(PlayerMovementStatesManager player)
	{
		//CAMERA SHAKES
		player.Determinant.PlayerCamera.RotationOffsetUpdate(Quaternion.identity,
			player.Determinant.CameraSetups.CameraTransitionsSmooth);
		player.Determinant.PlayerCamera.PositionOffsetUpdate(Vector3.zero,
			player.Determinant.CameraSetups.CameraTransitionsSmooth);

		player.Determinant.CameraShaker.SetFOV(player.Determinant.CameraSetups.FieldOfView,
			player.Determinant.CameraSetups.CameraTransitionsSmooth);

		//CAMERA NOISE
		player.Determinant.CameraShaker.SetGain(player.Determinant.CameraSetups.CameraIdleNoiseAmpFreq[0],
			player.Determinant.CameraSetups.CameraIdleNoiseAmpFreq[1],
			player.Determinant.CameraSetups.CameraTransitionsSmooth);
	}

	public override void ExitState(PlayerMovementStatesManager player)
	{
	}
}