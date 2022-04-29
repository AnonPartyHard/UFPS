using System.Collections;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private PlayerDeterminant _determinant;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _cameraAnchor;
    [SerializeField] private bool _enabled;

    private float _yAngle;
    private float _xAngle;
    private Vector3 _offsetPosition;
    private Quaternion _offsetRotation;
    private CinemachineVirtualCamera _vCam;
    private Vector3 _cameraOriginPosition;

    // private bool _locked = false;

    // private float _lockedX;
    // private float _lockedY;

    // private Quaternion _lockedRotation;
    // private float _lockY;
    // private float _lockX;


    //DEBUG
    public Transform _lookAtTarget;

    public Transform CameraAnchor => _cameraAnchor;

    public CinemachineVirtualCamera VirtualCamera => _vCam;

    public bool Enabled
    {
        get { return _enabled; }
        set { _enabled = value; }
    }

    private void Awake()
    {
        _vCam = _camera.GetComponent<CinemachineVirtualCamera>();
        // _lockX = _determinant.PlayerSetups.CameraAngleLimit;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _vCam.m_Lens.FieldOfView = _determinant.CameraSetups.FieldOfView;
        _cameraOriginPosition = _cameraAnchor.localPosition;
    }

    private void Update()
    {
        if (_enabled)
        {
            RotateAnchor();
            UpdateAngles();
        }

        SynchCamera();

        //DEBUG
        // if (_determinant.PlayerInput.IsKeyPressed(InputKeys.TESTLOOKAT))
        //     if (!_locked)
        //         Lock(30f, 30f, Quaternion.LookRotation(_cameraAnchor.forward));
        //     else
        //         Unlock();

        // if (Input.GetKey(KeyCode.U))
        //     UpdateLockRotation(Quaternion.LookRotation(-_cameraAnchor.forward),
        //         _determinant.PlayerSetups.CameraTransitionsSmooth);
    }

    public void LookAt(Transform target, float duration, float smooth, bool disableCamera)
    {
        StartCoroutine(LookAtCoroutine(target, duration, smooth, disableCamera));
    }

    private void SynchCamera()
    {
        _camera.position = _cameraAnchor.position;
        _camera.rotation = _cameraAnchor.rotation;
    }

    private void UpdateAngles()
    {
        _yAngle += _determinant.PlayerInput.GetMouseVector().x * _determinant.CameraSetups.CameraSensitivity *
                   _determinant.CameraSetups.CameraSensitivityMultiplier * Time.fixedDeltaTime;

        _xAngle -= _determinant.PlayerInput.GetMouseVector().y * _determinant.CameraSetups.CameraSensitivity *
                   _determinant.CameraSetups.CameraSensitivityMultiplier * Time.fixedDeltaTime;

        _xAngle = Mathf.Clamp(_xAngle, -_determinant.CameraSetups.FieldOfView, _determinant.CameraSetups.FieldOfView);

        // if (_locked)
        //     _yAngle = Mathf.Clamp(_yAngle, _lockedY - _lockY, _lockedY + _lockY);

        // _xAngle = Mathf.Clamp(_xAngle, _lockedX - _lockX, _lockedX + _lockX);
    }

    private void RotateAnchor()
    {
        _determinant.Rigidbody.MoveRotation(Quaternion.Slerp(_determinant.Rigidbody.rotation,
            Quaternion.Euler(0, _yAngle, 0), _determinant.CameraSetups.CameraSmooth * Time.fixedDeltaTime));

        _cameraAnchor.localRotation = Quaternion.Slerp(_cameraAnchor.localRotation, Quaternion.Euler(_xAngle, 0, 0),
            _determinant.CameraSetups.CameraSmooth * Time.fixedDeltaTime);

        _cameraAnchor.localRotation *= _offsetRotation;
        _cameraAnchor.localPosition = _cameraOriginPosition + _offsetPosition;
    }

    public void RotationOffsetUpdate(Quaternion targetRotation, float smooth)
    {
        _offsetRotation = Quaternion.Slerp(_offsetRotation, targetRotation, smooth * Time.fixedDeltaTime);
    }

    public void PositionOffsetUpdate(Vector3 targetPosition, float smooth)
    {
        _offsetPosition = Vector3.Slerp(_offsetPosition, targetPosition, smooth * Time.fixedDeltaTime);
    }

    // public void Lock(float x, float y, Quaternion lockRotation)
    // {
    //     _lockedY = lockRotation.eulerAngles.y;
    //     _lockedX = lockRotation.eulerAngles.x;
    //     _lockX = x;
    //     _lockY = y;
    //     _locked = true;
    // }

    // public void UpdateLockRotation(Quaternion rotation, float smooth)
    // {
    //     _lockedX = rotation.eulerAngles.x;
    //     _lockedY = rotation.eulerAngles.y;
    //     // _lockedX = Mathf.Lerp(_lockedX, rotation.eulerAngles.x, smooth * Time.fixedDeltaTime);
    //     // _lockedY = Mathf.Lerp(_lockedY, rotation.eulerAngles.y, smooth * Time.fixedDeltaTime);
    // }

    // public void Unlock()
    // {
    //     _lockY = _determinant.PlayerSetups.CameraAngleLimit;
    //     _locked = false;
    // }

    private IEnumerator LookAtCoroutine(Transform target, float duration, float smooth, bool disableCamera)
    {
        _enabled = false;
        Quaternion originYRot = transform.localRotation;
        Quaternion originXRot = _cameraAnchor.localRotation;
        Vector3 dir = target.position - _cameraAnchor.position;
        Quaternion LookRot = Quaternion.LookRotation(dir);
        Quaternion yRot = LookRot;
        Quaternion xRot = LookRot;
        yRot.x = 0;
        yRot.z = 0;
        xRot.y = 0;
        xRot.z = 0;
        float t = 0f;
        while (t < 1f)
        {
            yield return null;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, yRot, t * smooth);
            _cameraAnchor.localRotation = Quaternion.Slerp(_cameraAnchor.localRotation, xRot, t * smooth);
            t += Time.deltaTime;
        }

        if (!disableCamera)
        {
            t = 0f;
            yield return new WaitForSeconds(duration);
            while (t < 1f)
            {
                yield return null;
                transform.localRotation = Quaternion.Slerp(transform.localRotation, originYRot, t * smooth);
                _cameraAnchor.localRotation = Quaternion.Slerp(_cameraAnchor.localRotation, originXRot, t * smooth);
                t += Time.deltaTime;
            }

            _enabled = true;
        }
    }
}