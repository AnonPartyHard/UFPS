using System.Collections;
using UnityEngine;

public class MovementDuckState : MovementBaseState
{
    private void TrackForInput(PlayerMovementStatesManager player)
    {
        if(player.Determinant.PlayerInput.GetMovementVector().magnitude > 0f)
            player.SwitchState(player.CrawlState);
        
        if(player.Determinant.PlayerInput.IsKeyReleased(InputKeys.DUCK) && !player.Determinant.CeilSensor.IsOverlap())
            player.SwitchState(player.IdleState);
    }
    
    public override void EnterState(PlayerMovementStatesManager player)
    {
        player.Determinant.UpperCollider.enabled = false;
    }

    public override void UpdateState(PlayerMovementStatesManager player)
    {
        TrackForInput(player);
    }

    public override void FixedUpdateState(PlayerMovementStatesManager player)
    {
        player.Determinant.PlayerCamera.PositionOffsetUpdate(Vector3.down,
            player.Determinant.CameraSetups.CameraTransitionsSmooth);

        player.Determinant.CameraShaker.SetFOV(player.Determinant.CameraSetups.FieldOfView,
            player.Determinant.CameraSetups.CameraTransitionsSmooth);
        
        player.Determinant.PlayerCamera.RotationOffsetUpdate(Quaternion.Euler(0f,0f,0f),
            player.Determinant.CameraSetups.CameraTransitionsSmooth);
        
        //CAMERA NOISE
        player.Determinant.CameraShaker.SetGain(0f, 0f, player.Determinant.CameraSetups.CameraTransitionsSmooth);
    }

    public override void ExitState(PlayerMovementStatesManager player)
    {
        player.Determinant.UpperCollider.enabled = true;
    }
}