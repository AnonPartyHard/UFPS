using UnityEngine;

[CreateAssetMenu(fileName = "CameraSetups", menuName = "ScriptableObjects/Player/CameraSetups", order = 1)]

public class CameraSetups : ScriptableObject
{
	[SerializeField] private float _fieldOfView = 75f;
	[SerializeField] private float _camSensitivity = 10f;
	[SerializeField] private float _camSensitivityMultiplier = 5f;
	[SerializeField] private float _camSmooth = 25f;
	[SerializeField] private float _camTransitionsSmooth = 2f;
	[SerializeField] private float _camRunOffset = 2f;
	[SerializeField] private float _camWallRunOffset = 15f;
	[SerializeField] private float _camJumpOffsetMultiplier = 1f;
	[SerializeField] private float _camSprintAdditionalFOV = 15f;
	[SerializeField] private float[] _camIdleNoiseAmpFreq = new float[2];
	[SerializeField] private float[] _camDuckNoiseAmpFreq = new float[2];
	[SerializeField] private float[] _camRunNoiseAmpFreq = new float[2];
	[SerializeField] private float[] _camSprintNoiseAmpFreq = new float[2];
	[Range(1.0f, 89.9f)] [SerializeField] private float _viewLimitAngle = 89f;
	
	public float FieldOfView => _fieldOfView;
	public float CameraSensitivity => _camSensitivity;
	public float CameraSensitivityMultiplier => _camSensitivityMultiplier;
	public float CameraSmooth => _camSmooth;
	public float CameraTransitionsSmooth => _camTransitionsSmooth;
	public float CameraRunOffset => _camRunOffset;
	public float CameraWallRunOffset => _camWallRunOffset;
	public float CamJumpOffsetMultiplier => _camJumpOffsetMultiplier;
	public float CamSprintAdditionalFOV => _camSprintAdditionalFOV;
	public float[] CameraIdleNoiseAmpFreq => _camIdleNoiseAmpFreq;
	public float[] CameraDuckNoiseAmpFreq => _camDuckNoiseAmpFreq;
	public float[] CameraRunNoiseAmpFreq => _camRunNoiseAmpFreq;
	public float[] CameraSprintNoiseAmpFreq => _camSprintNoiseAmpFreq;
	public float CameraAngleLimit => _viewLimitAngle;
}
