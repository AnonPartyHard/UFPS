using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSetups", menuName = "ScriptableObjects/Player/Setups", order = 3)]
public class PlayerSetups : ScriptableObject
{
    [SerializeField] private float _jumpForce = 450f;
    [SerializeField] private float _jumpForceMultiplier = 10f;
    [SerializeField] private float _wallPushForce = 250f;
    [SerializeField] private float _groundMoveSpeed = 25f;
    [SerializeField] private float _groundControlMultiplier = 2f;
    [SerializeField] private float _airMoveSpeed = 2f;
    [SerializeField] private float _wallSlideSpeed = 500f;
    [SerializeField] private float _groundDrag = 6f;
    [SerializeField] private float _airDrag = 0f;
    [SerializeField] private float _airControlMultiplier = 0.2f;
    [SerializeField] private float _sprintSpeed = 50f;
    [SerializeField] private float _duckSpeed = 10f;
    [SerializeField] private float _slideDrag = 0.5f;
    [SerializeField] private float _slideLength = 2f;
    [SerializeField] private float _climbDuration = 0.35f;
    [SerializeField] private float _climbForce = 50f;
    [SerializeField] private float _climbUpForce = 1000f;
    [SerializeField] private float _climbUpForceMultiplier = 10f;

    public float JumpForce => _jumpForce;
    public float JumpForceMultiplier => _jumpForceMultiplier;
    public float WallPushForce => _wallPushForce;
    public float GroundMoveSpeed => _groundMoveSpeed;
    public float AirMoveSpeed => _airMoveSpeed;
    public float WallSlideSpeed => _wallSlideSpeed;
    public float GroundDrag => _groundDrag;
    public float AirDrag => _airDrag;
    public float AirControlMultiplier => _airControlMultiplier;
    public float GroundControlMultiplier => _groundControlMultiplier;
    public float SprintSpeed => _sprintSpeed;
    public float DuckSpeed => _duckSpeed;
    public float SlideDrag => _slideDrag;
    public float SideLength => _slideLength;
    public float ClimbDuration => _climbDuration;
    public float ClimbForce => _climbForce;
    public float ClimbUpForce => _climbUpForce;
    public float ClimbUpForceMultiplier => _climbUpForceMultiplier;
}