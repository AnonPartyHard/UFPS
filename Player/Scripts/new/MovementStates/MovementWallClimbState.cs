using UnityEngine;

public class MovementWallClimbState : MovementBaseState
{
    private float _timeMark;
    public override void EnterState(PlayerMovementStatesManager player)
    {
        player.Determinant.Rigidbody.useGravity = false;
        _timeMark = Time.time;
        player.CanClimb = false;
    }

    public override void UpdateState(PlayerMovementStatesManager player)
    {
        if (Time.time > _timeMark + player.Determinant.PlayerSetups.ClimbDuration)
            player.SwitchState(player.InAirState);

        if (player.Determinant.PlayerInput.IsKeyReleased(InputKeys.JUMP))
            player.SwitchState(player.InAirState);

        if (!player.Determinant.ForwardWallSensor.IsOverlap())
        {
            //Help to climb up if he is falling
            if (player.Determinant.Rigidbody.velocity.y < 4f)
                player.Determinant.Rigidbody.AddForce(
                    Vector3.up * player.Determinant.PlayerSetups.ClimbUpForce *
                    player.Determinant.PlayerSetups.ClimbUpForceMultiplier *
                    player.Determinant.PlayerSetups.ClimbFallingHelpFactor *
                    Time.fixedDeltaTime,
                    ForceMode.Impulse);
            else
                player.Determinant.Rigidbody.AddForce(
                    Vector3.up * player.Determinant.PlayerSetups.ClimbUpForce *
                    player.Determinant.PlayerSetups.ClimbUpForceMultiplier * Time.fixedDeltaTime,
                    ForceMode.Impulse);


            player.SwitchState(player.InAirState);
        }
    }

    public override void FixedUpdateState(PlayerMovementStatesManager player)
    {
        player.Determinant.Rigidbody.AddForce(
            (Vector3.up * player.Determinant.PlayerSetups.ClimbForce) -
            player.Determinant.ForwardWallSensor.Target.normal * 20f,
            ForceMode.Acceleration);
    }

    public override void ExitState(PlayerMovementStatesManager player)
    {
        player.Determinant.Rigidbody.useGravity = true;
    }
}