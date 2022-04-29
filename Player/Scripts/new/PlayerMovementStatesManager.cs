using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementStatesManager : MonoBehaviour
{
    [SerializeField] private PlayerDeterminant _determinant;

    private Vector3 _movementForce;
    public bool _isSprinting;
    private bool _isWallSliding;
    private bool _canClimb;

    private MovementBaseState _currentState;
    public MovementIdleState IdleState = new MovementIdleState();
    public MovementRunState RunState = new MovementRunState();
    public MovementSprintState SprintState = new MovementSprintState();
    public MovementInAirState InAirState = new MovementInAirState();
    public MovementDuckState DuckState = new MovementDuckState();
    public MovementCrawlState CrawlState = new MovementCrawlState();
    public MovementSlideState SlideState = new MovementSlideState();
    public MovementWallSlideState WallSlideState = new MovementWallSlideState();
    public MovementWallClimbState WallClimbState = new MovementWallClimbState();

    //DEBUG
    public Text _UIStateText;
    public PlayerDeterminant Determinant => _determinant;
    public Vector3 MovementForce => _movementForce;

    public bool IsSprinting
    {
        get { return _isSprinting; }
        set { _isSprinting = value; }
    }

    public bool IsWallSliding
    {
        get { return _isWallSliding; }
        set { _isWallSliding = value; }
    }
    
    public bool CanClimb
    {
        get { return _canClimb; }
        set { _canClimb = value; }
    }

    private void Start()
    {
        _canClimb = true;
        _currentState = InAirState;
        _currentState.EnterState(this);
    }

    private void UpdateMoveForce()
    {
        _movementForce = _determinant.transform.forward * _determinant.PlayerInput.GetMovementVector().y +
            _determinant.transform.right * _determinant.PlayerInput.GetMovementVector().x;
    }

    private void Update()
    {
        if (!_determinant.GroundSensor.IsOverlap() && _currentState != WallSlideState &&
            _currentState != WallClimbState && _currentState != InAirState)
            SwitchState(InAirState);

        UpdateMoveForce();
        _currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        _currentState.FixedUpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
        _determinant.MovementStateChangeEventChannel.StateChanged(state);

        //DEBUG
        if (_UIStateText != null)
        {
            string stateName = _currentState.ToString();
            string FinalString;
            int Pos1 = stateName.IndexOf("Movement") + "Movement".Length;
            int Pos2 = stateName.IndexOf("State");
            FinalString = stateName.Substring(Pos1, Pos2 - Pos1);

            _UIStateText.text = FinalString;
        }
    }
}