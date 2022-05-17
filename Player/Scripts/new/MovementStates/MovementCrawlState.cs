using UnityEngine;

public class MovementCrawlState : MovementBaseState
{
    private void TrackForInput(PlayerMovementStatesManager player)
    {
        if (player.Determinant.PlayerInput.GetMovementVector().magnitude < 1f)
            player.SwitchState(player.DuckState);

        if (!player.Determinant.PlayerInput.IsKeyDown(InputKeys.DUCK) && !player.Determinant.CeilSensor.IsOverlap())
            player.SwitchState(player.IdleState);
    }

    public override void EnterState(PlayerMovementStatesManager player)
    {
        player.Determinant.UpperCollider.enabled = false;
        //player.Determinant.PlayerRepresentationAnimator.CrossFade("CrouchTree", 0.3f, new int[2] { 0, 1 });
    }

    public override void UpdateState(PlayerMovementStatesManager player)
    {
        TrackForInput(player);
        player.Determinant.PlayerRepresentationAnimator.AnimateRunTree(1f);
        player.Determinant.PlayerRepresentationAnimator.
            AdjustRepresentationRotation(Quaternion.LookRotation(player.Determinant.PlayerCamera.CameraPivot.forward), 5f);
    }

    public override void FixedUpdateState(PlayerMovementStatesManager player)
    {
        player.Determinant.Rigidbody.AddForce(
            player.MovementForce.normalized * player.Determinant.PlayerSetups.DuckSpeed *
            player.Determinant.PlayerSetups.GroundControlMultiplier * Time.fixedDeltaTime,
            ForceMode.Acceleration);

        player.Determinant.PlayerCamera.PositionOffsetUpdate(Vector3.down * 0.5f,
            player.Determinant.CameraSetups.CameraTransitionsSmooth);

        player.Determinant.CameraShaker.SetFOV(player.Determinant.CameraSetups.FieldOfView,
            player.Determinant.CameraSetups.CameraTransitionsSmooth);

        player.Determinant.PlayerCamera.RotationOffsetUpdate(Quaternion.Euler(0f, 0f, 0f),
            player.Determinant.CameraSetups.CameraTransitionsSmooth);

        //CAMERA NOISE
        player.Determinant.CameraShaker.SetGain(player.Determinant.CameraSetups.CameraDuckNoiseAmpFreq[0],
            player.Determinant.CameraSetups.CameraDuckNoiseAmpFreq[1] * player.Determinant.Rigidbody.velocity.magnitude,
            player.Determinant.CameraSetups.CameraTransitionsSmooth);
    }

    public override void ExitState(PlayerMovementStatesManager player)
    {
        player.Determinant.UpperCollider.enabled = true;
    }
}