using System;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(PlayerCamera))]
public class CameraShaker : MonoBehaviour
{
    [SerializeField] private NoiseSettings _runNoiseProfile;
    private PlayerDeterminant _determinant;
    private PlayerCamera _playerCamera;
    private float _currentAmplitude = 0f;
    private float _currentFrequency = 0f;
    private float _currentFOV;
    private CinemachineBasicMultiChannelPerlin _perlin; //private

    public NoiseSettings RunNoiseProfile => _runNoiseProfile;

    private void Awake()
    {
        _determinant = GetComponent<PlayerDeterminant>();
        _playerCamera = GetComponent<PlayerCamera>();
    }

    private void Start()
    {
        _perlin = GetComponent<PlayerCamera>().VirtualCamera
            .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _perlin.m_NoiseProfile = _runNoiseProfile;
        _perlin.m_AmplitudeGain = 0f;
        _perlin.m_FrequencyGain = 0f;
        _currentFOV = _determinant.CameraSetups.FieldOfView;
    }

    private void Update()
    {
        _perlin.m_FrequencyGain = _currentFrequency;
        _perlin.m_AmplitudeGain = _currentAmplitude;
        _playerCamera.VirtualCamera.m_Lens.FieldOfView = _currentFOV;
    }

    public void SetGain(float amplitude, float frequency, float smooth)
    {
        _currentAmplitude = Mathf.Lerp(_currentAmplitude, amplitude, smooth * Time.fixedDeltaTime);
        _currentFrequency = Mathf.Lerp(_currentFrequency, frequency, smooth * Time.fixedDeltaTime);
    }

    public void SetFOV(float targetFOV, float smooth)
    {
        _currentFOV = Mathf.Lerp(_currentFOV, targetFOV, smooth * Time.fixedDeltaTime);
    }
}