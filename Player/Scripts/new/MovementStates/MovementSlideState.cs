using System.Collections;
using UnityEngine;

public class MovementSlideState : MovementBaseState
{
    private Coroutine _stateCoroutine;

    private void TrackForInput(PlayerMovementStatesManager player)
    {
        if (player.Determinant.PlayerInput.GetMovementVector().y < 1)
            player.IsSprinting = false;

        if (player.Determinant.PlayerInput.IsKeyReleased(InputKeys.DUCK))
        {
            if (player.Determinant.CeilSensor.IsOverlap())
            {
                player.SwitchState(player.DuckState);
            }
            else
            {
                if (player.IsSprinting)
                    player.SwitchState(player.SprintState);
                else
                    player.SwitchState(player.RunState);
            }
        }
    }

    public override void EnterState(PlayerMovementStatesManager player)
    {
        player.Determinant.UpperCollider.enabled = false;
        player.Determinant.Rigidbody.drag = player.Determinant.PlayerSetups.SlideDrag;
        _stateCoroutine = player.StartCoroutine(StateCoroutine(player));
        player.Determinant.PlayerRepresentationAnimator.BoundConstraintsToTargets();
        player.Determinant.PlayerRepresentationAnimator.CrossFade("Slide", 0.2f, new int[2] { 0, 1 });
    }

    public override void UpdateState(PlayerMovementStatesManager player)
    {
        TrackForInput(player);
    }

    public override void FixedUpdateState(PlayerMovementStatesManager player)
    {
        player.Determinant.PlayerCamera.PositionOffsetUpdate(Vector3.down * 0.8f,
            player.Determinant.CameraSetups.CameraTransitionsSmooth);

        player.Determinant.PlayerCamera.RotationOffsetUpdate(Quaternion.Euler(-15f,0f,0f),
            player.Determinant.CameraSetups.CameraTransitionsSmooth);

        //CAMERA NOISE
        player.Determinant.CameraShaker.SetGain(0f, 0f, player.Determinant.CameraSetups.CameraTransitionsSmooth);
    }

    public override void ExitState(PlayerMovementStatesManager player)
    {
        player.Determinant.UpperCollider.enabled = true;
        player.Determinant.Rigidbody.drag = player.Determinant.PlayerSetups.GroundDrag;
        player.StopCoroutine(_stateCoroutine);
        _stateCoroutine = null;

        player.Determinant.PlayerRepresentationAnimator.UnBoundConstraints();
    }


    private IEnumerator StateCoroutine(PlayerMovementStatesManager player)
    {
        yield return new WaitForSeconds(player.Determinant.PlayerSetups.SideLength);
        player.SwitchState(player.DuckState);
    }
}