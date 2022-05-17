using UnityEngine;

public class MovementWallClimbState : MovementBaseState
{
    private float _timeMark;
    public override void EnterState(PlayerMovementStatesManager player)
    {
        player.Determinant.Rigidbody.useGravity = false;
        _timeMark = Time.time;
        player.CanClimb = false;
        player.Determinant.PlayerRepresentationAnimator.BoundConstraintsToTargets();
        player.Determinant.PlayerRepresentationAnimator.CrossFade("WallClimb", 0.2f, new int[2] { 0, 1 });
    }

    public override void UpdateState(PlayerMovementStatesManager player)
    {
        if (Time.time > _timeMark + player.Determinant.PlayerSetups.ClimbDuration)
        {
            player.Determinant.Rigidbody.velocity = Vector3.zero;
            player.Determinant.Rigidbody.AddForce(
                Vector3.up * player.Determinant.PlayerSetups.ClimbUpForce *
                player.Determinant.PlayerSetups.ClimbUpForceMultiplier * Time.fixedDeltaTime,
                ForceMode.Impulse);

            player.SwitchState(player.InAirState);
        }

        if (player.Determinant.PlayerInput.IsKeyReleased(InputKeys.JUMP))
            player.SwitchState(player.InAirState);

        player.Determinant.PlayerRepresentationAnimator.
            AdjustRepresentationRotation(Quaternion.LookRotation(-player.Determinant.ForwardWallSensor.Target.normal), 2f);


        if (!player.Determinant.ForwardWallSensor.IsOverlap())
        {
            player.Determinant.Rigidbody.velocity = Vector3.zero;
            player.Determinant.Rigidbody.AddForce(
                Vector3.up * player.Determinant.PlayerSetups.ClimbUpForce *
                player.Determinant.PlayerSetups.ClimbUpForceMultiplier * Time.fixedDeltaTime,
                ForceMode.Impulse);

            player.SwitchState(player.InAirState);
        }
    }

    public override void FixedUpdateState(PlayerMovementStatesManager player)
    {
        player.Determinant.PlayerCamera.PositionOffsetUpdate(Vector3.back * 0.3f, 
            player.Determinant.CameraSetups.CameraTransitionsSmooth);

        player.Determinant.Rigidbody.AddForce(
            (Vector3.up * player.Determinant.PlayerSetups.ClimbForce) -
            player.Determinant.ForwardWallSensor.Target.normal * 20f,
            ForceMode.Acceleration);
    }

    public override void ExitState(PlayerMovementStatesManager player)
    {
        player.Determinant.Rigidbody.useGravity = true;
        player.Determinant.PlayerRepresentationAnimator.UnBoundConstraints();
    }
}