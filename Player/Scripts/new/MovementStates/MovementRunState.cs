using UnityEngine;

public class MovementRunState : MovementBaseState
{
	private void TrackForInputs(PlayerMovementStatesManager player)
	{
		if (player.Determinant.PlayerInput.GetMovementVector().y < 1)
			player.IsSprinting = false;

		if (player.Determinant.PlayerInput.IsKeyPressed(InputKeys.JUMP))
			player.Determinant.Rigidbody.AddForce(
				Vector3.up * player.Determinant.PlayerSetups.JumpForce *
				player.Determinant.PlayerSetups.JumpForceMultiplier * Time.fixedDeltaTime,
				ForceMode.Impulse);
		if (player.Determinant.PlayerInput.IsKeyDown(InputKeys.DUCK))
			player.SwitchState(player.DuckState);

		if (player.Determinant.PlayerInput.GetMovementVector().magnitude < 1)
			player.SwitchState(player.IdleState);

		if (player.Determinant.PlayerInput.IsKeyDown(InputKeys.SPRINT) &&
		    player.Determinant.PlayerInput.GetMovementVector().y > 0)
			player.SwitchState(player.SprintState);
	}

	public override void EnterState(PlayerMovementStatesManager player)
	{
        //player.Determinant.PlayerRepresentationAnimator.CrossFade("RunningTree", 0.2f, new int[2] { 0, 1 });
    }

	public override void UpdateState(PlayerMovementStatesManager player)
	{
		TrackForInputs(player);
		player.Determinant.PlayerRepresentationAnimator.AnimateRunTree(5f);
		player.Determinant.PlayerRepresentationAnimator.
			AdjustRepresentationRotation(Quaternion.LookRotation(player.Determinant.PlayerCamera.CameraPivot.forward), 5f);
	}

	public override void FixedUpdateState(PlayerMovementStatesManager player)
	{
		player.Determinant.Rigidbody.AddForce(
			player.MovementForce.normalized * player.Determinant.PlayerSetups.GroundMoveSpeed *
			player.Determinant.PlayerSetups.GroundControlMultiplier * Time.fixedDeltaTime,
			ForceMode.Acceleration);

		//CAMERA
		player.Determinant.CameraShaker.SetGain(
			player.Determinant.CameraSetups.CameraRunNoiseAmpFreq[0],
			player.Determinant.CameraSetups.CameraRunNoiseAmpFreq[1] * player.Determinant.Rigidbody.velocity.magnitude,
			player.Determinant.CameraSetups.CameraTransitionsSmooth);

		player.Determinant.CameraShaker.SetFOV(player.Determinant.CameraSetups.FieldOfView,
			player.Determinant.CameraSetups.CameraTransitionsSmooth);

		player.Determinant.PlayerCamera.PositionOffsetUpdate(Vector3.zero,
			player.Determinant.CameraSetups.CameraTransitionsSmooth);

		player.Determinant.PlayerCamera.RotationOffsetUpdate(
			Quaternion.Euler(0, 0,
				-player.Determinant.PlayerInput.GetMovementVector().x *
				player.Determinant.CameraSetups.CameraRunOffset),
			player.Determinant.CameraSetups.CameraTransitionsSmooth);
	}

	public override void ExitState(PlayerMovementStatesManager player)
	{
	}
}