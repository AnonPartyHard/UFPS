using System;
using UnityEngine;

public class PlayerDeterminant : MonoBehaviour
{
    [SerializeField] private PlayerInputsSO _inputs;
    [SerializeField] private PlayerSetups _playerSetups;
    [SerializeField] private CameraSetups _cameraSetups;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private SphereSensor _groundSensor;
    [SerializeField] private SphereSensor _ceilSensor;
    [SerializeField] private Collider _upperCollider;
    [SerializeField] private PlayerCamera _playerCamera;
    [SerializeField] private CameraShaker _cameraShaker;
    [SerializeField] private SphereSensor _forwardWallSensor;
    [SerializeField] private SphereSensor _rightWallSensor;
    [SerializeField] private SphereSensor _leftWallSensor;
    [SerializeField] private PlayerMovementStateChangeEventChannel _movementStateChangeEventChannel;
    [SerializeField] private PlayerRepresentationAnimator _playerRepresentationAnimator;
    public PlayerSetups PlayerSetups => _playerSetups;
    public CameraSetups CameraSetups => _cameraSetups;
    public PlayerInputsSO PlayerInput => _inputs;
    public Rigidbody Rigidbody => _rigidbody;
    public SphereSensor GroundSensor => _groundSensor;
    public SphereSensor CeilSensor => _ceilSensor;
    public Collider UpperCollider => _upperCollider;
    public PlayerCamera PlayerCamera => _playerCamera;
    public CameraShaker CameraShaker => _cameraShaker;
    public SphereSensor RightWallSensor => _rightWallSensor;
    public SphereSensor LeftWallSensor => _leftWallSensor;
    public SphereSensor ForwardWallSensor => _forwardWallSensor;
    public PlayerMovementStateChangeEventChannel MovementStateChangeEventChannel => _movementStateChangeEventChannel;
    public PlayerRepresentationAnimator PlayerRepresentationAnimator => _playerRepresentationAnimator;

}
