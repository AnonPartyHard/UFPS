using System.Collections;
using Cinemachine;
using UnityEngine;
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private PlayerDeterminant _determinant;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Transform _cameraAnchor;
    [SerializeField] private bool _enabled;
    [SerializeField] private bool _smooth;

    private float _yAngle;
    private float _xAngle;
    private Vector3 _offsetPosition;
    private Quaternion _offsetRotation;
    private CinemachineVirtualCamera _vCam;
    private Vector3 _cameraOriginPosition;

    //DEBUG
    public Transform _lookAtTarget;

    public Transform CameraAnchor => _cameraAnchor;
    public Transform CameraPivot => _cameraPivot;

    public CinemachineVirtualCamera VirtualCamera => _vCam;

    public bool Enabled
    {
        get { return _enabled; }
        set { _enabled = value; }
    }

    private void Awake()
    {
        _vCam = _camera.GetComponent<CinemachineVirtualCamera>();
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

        _xAngle = Mathf.Clamp(_xAngle, -_determinant.CameraSetups.CameraAngleLimit, _determinant.CameraSetups.CameraAngleLimit);
    }

    private void RotateAnchor()
    {
        if (_smooth)
        {
            _cameraPivot.localRotation = Quaternion.Slerp(_cameraPivot.localRotation, Quaternion.Euler(0, _yAngle, 0),
                _determinant.CameraSetups.CameraSmooth * Time.fixedDeltaTime);

            _cameraAnchor.localRotation = Quaternion.Slerp(_cameraAnchor.localRotation, Quaternion.Euler(_xAngle, 0, 0),
                _determinant.CameraSetups.CameraSmooth * Time.fixedDeltaTime);
        } else
        {
            _cameraPivot.localRotation = Quaternion.Euler(0, _yAngle, 0);
            _cameraAnchor.localRotation = Quaternion.Euler(_xAngle, 0, 0);
        }

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