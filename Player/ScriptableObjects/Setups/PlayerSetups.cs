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
    // [SerializeField] private float _fieldOfView = 75f;
    // [SerializeField] private float _camSensitivity = 10f;
    // [SerializeField] private float _camSensitivityMultiplier = 5f;
    // [SerializeField] private float _camSmooth = 25f;
    // [SerializeField] private float _camTransitionsSmooth = 2f;
    // [SerializeField] private float _camRunOffset = 2f;
    // [SerializeField] private float _camWallRunOffset = 15f;
    // [SerializeField] private float _camJumpOffsetMultiplier = 1f;
    // [SerializeField] private float _camSprintAdditionalFOV = 15f;
    // [SerializeField] private float[] _camIdleNoiseAmpFreq = new float[2];
    // [SerializeField] private float[] _camDuckNoiseAmpFreq = new float[2];
    // [SerializeField] private float[] _camRunNoiseAmpFreq = new float[2];
    // [SerializeField] private float[] _camSprintNoiseAmpFreq = new float[2];
    [SerializeField] private float _climbDuration = 0.35f;
    [SerializeField] private float _climbForce = 50f;
    [SerializeField] private float _climbUpForce = 1000f;
    [SerializeField] private float _climbUpForceMultiplier = 10f;
    [SerializeField] private float _climbFallingHelpFactor = 3f;
    // [Range(1.0f, 89.9f)] [SerializeField] private float _viewLimitAngle = 89f;

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
    // public float FieldOfView => _fieldOfView;
    // public float CameraSensitivity => _camSensitivity;
    // public float CameraSensitivityMultiplier => _camSensitivityMultiplier;
    // public float CameraSmooth => _camSmooth;
    // public float CameraTransitionsSmooth => _camTransitionsSmooth;
    // public float CameraRunOffset => _camRunOffset;
    // public float CameraWallRunOffset => _camWallRunOffset;
    // public float CamJumpOffsetMultiplier => _camJumpOffsetMultiplier;
    // public float CamSprintAdditionalFOV => _camSprintAdditionalFOV;
    // public float[] CameraIdleNoiseAmpFreq => _camIdleNoiseAmpFreq;
    // public float[] CameraDuckNoiseAmpFreq => _camDuckNoiseAmpFreq;
    // public float[] CameraRunNoiseAmpFreq => _camRunNoiseAmpFreq;
    // public float[] CameraSprintNoiseAmpFreq => _camSprintNoiseAmpFreq;
    // public float CameraAngleLimit => _viewLimitAngle;
    public float ClimbDuration => _climbDuration;
    public float ClimbForce => _climbForce;
    public float ClimbUpForce => _climbUpForce;
    public float ClimbUpForceMultiplier => _climbUpForceMultiplier;
    public float ClimbFallingHelpFactor => _climbFallingHelpFactor;
}