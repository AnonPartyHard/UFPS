using UnityEngine;

public class MovementIdleState : MovementBaseState
{
	private Quaternion _repForceTargetRot;
	private Quaternion _repSmoothRot;
	private bool _forceRotating = false;
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
		player.Determinant.PlayerRepresentationAnimator.CrossFade("RunningTree", 0.2f, new int[2] { 0, 1 });
		_repForceTargetRot = player.Determinant.PlayerCamera.CameraPivot.localRotation * Quaternion.Euler(0, -20f, 0);
		_repSmoothRot = _repForceTargetRot;

	}

	public override void UpdateState(PlayerMovementStatesManager player)
	{
		player.Determinant.PlayerRepresentationAnimator.AnimateRunTree(5f);
		TrackForInputs(player);
		TrackRepresentationAngle(player);
	}

	private void TrackRepresentationAngle(PlayerMovementStatesManager player)
    {
		float angle = Vector3.Angle(player.Determinant.PlayerCamera.CameraPivot.forward, player.Determinant.PlayerRepresentationAnimator.Reporesentation.transform.forward);
		Vector3 cross = Vector3.Cross(player.Determinant.PlayerCamera.CameraPivot.forward, player.Determinant.PlayerRepresentationAnimator.Reporesentation.transform.forward);

		if(angle > 0f && cross.y > 0 && !_forceRotating)
        {
			player.Determinant.PlayerRepresentationAnimator.CrossFade("TurnLeft", 0.2f, new int[1] {0});

			_repSmoothRot = _repSmoothRot * Quaternion.Euler(0, -85f, 0);
		}
		
		if(angle > 0.1f && cross.y > 0)
        {
			_forceRotating = true;
			player.Determinant.PlayerRepresentationAnimator.Reporesentation.localRotation = player.Determinant.PlayerCamera.CameraPivot.localRotation;
		} else
        {
			_forceRotating = false;
		}

        if (angle > 130f && cross.y < 0 && !_forceRotating)
        {
			player.Determinant.PlayerRepresentationAnimator.CrossFade("TurnRight", 0.2f, new int[1] {0});

            _repSmoothRot = _repSmoothRot * Quaternion.Euler(0, 85F, 0);
        }

        if (angle > 130.1f && cross.y < 0)
        {
            _forceRotating = true;
            player.Determinant.PlayerRepresentationAnimator.Reporesentation.localRotation = player.Determinant.PlayerCamera.CameraPivot.localRotation * 
				Quaternion.Euler(0, -130f, 0);
        }
        else
        {
            _forceRotating = false;
        }

        if (!_forceRotating)
            player.Determinant.PlayerRepresentationAnimator.Reporesentation.localRotation = Quaternion.Slerp(
				player.Determinant.PlayerRepresentationAnimator.Reporesentation.localRotation,
				_repSmoothRot, 1f * Time.fixedDeltaTime);
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
		player.Determinant.PlayerRepresentationAnimator.transform.localRotation = Quaternion.identity;
    }
}