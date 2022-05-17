using UnityEngine;
using UnityEngine.Animations.Rigging;

[System.Serializable]
public class IKHandPoints
{
    [SerializeField] private Transform _wrist;
    [SerializeField] private WeightedTransformArray _indexFinger;
    [SerializeField] private WeightedTransformArray _indexFinger1;
    [SerializeField] private WeightedTransformArray _indexFinger2;
    [SerializeField] private WeightedTransformArray _finger;
    [SerializeField] private WeightedTransformArray _finger1;
    [SerializeField] private WeightedTransformArray _finger2;
    [SerializeField] private WeightedTransformArray _thumb;
    [SerializeField] private WeightedTransformArray _thumb1;

    public Transform Wrist => _wrist;
    public WeightedTransformArray IndexFinger => _indexFinger;
    public WeightedTransformArray IndexFinger1 => _indexFinger1;
    public WeightedTransformArray IndexFinger2 => _indexFinger2;
    public WeightedTransformArray Finger => _finger;
    public WeightedTransformArray Finger1 => _finger1;
    public WeightedTransformArray Finger2 => _finger2;
    public WeightedTransformArray Thumb => _thumb;
    public WeightedTransformArray Thumb1 => _thumb1;
}

[System.Serializable]
public class RigHandConstraints
{
    [SerializeField] private TwoBoneIKConstraint _wrist;
    [SerializeField] private MultiRotationConstraint _indexFinger;
    [SerializeField] private MultiRotationConstraint _indexFinger1;
    [SerializeField] private MultiRotationConstraint _indexFinger2;

    [SerializeField] private MultiRotationConstraint _finger;
    [SerializeField] private MultiRotationConstraint _finger1;
    [SerializeField] private MultiRotationConstraint _finger2;

    [SerializeField] private MultiRotationConstraint _thumb;
    [SerializeField] private MultiRotationConstraint _thumb1;

    public void SetConstraints(IKHandPoints points)
    {
        _wrist.data.target = points.Wrist;

        _indexFinger.data.sourceObjects = points.IndexFinger;
        _indexFinger1.data.sourceObjects = points.IndexFinger1;
        _indexFinger2.data.sourceObjects = points.IndexFinger2;
        _finger.data.sourceObjects = points.Finger;
        _finger1.data.sourceObjects = points.Finger1;
        _finger2.data.sourceObjects = points.Finger2;
        _thumb.data.sourceObjects = points.Thumb;
        _thumb1.data.sourceObjects = points.Thumb1;
    }
}

public class PlayerRepresentationAnimator : MonoBehaviour
{
    [SerializeField] private PlayerDeterminant _determinant;
    [SerializeField] private Transform _reporesentation;
    [SerializeField] private Avatar _avatar;
    [SerializeField] private  Rig _rightArmRig;
    [SerializeField] private  Rig _leftArmRig;
    [SerializeField] private  Rig _rightLegRig;
    [SerializeField] private  Rig _leftLegRig;
    [SerializeField] private Rig _bodyRig;

    [Header("IK Constraints")]
    [SerializeField] RigHandConstraints _rightHandConstraint;
    [SerializeField] RigHandConstraints _leftHandConstraint;
    [Header("IK Default targets from unarmed state.")]
    [SerializeField] IKHandPoints _leftHandPoints;
    [SerializeField] IKHandPoints _rightHandPoints;

    private Animator _animator;
    private Rig[] _allRigs;
    public Transform Reporesentation => _reporesentation;
    public Rig RightArmRig => _rightArmRig;
    public Rig LeftArmRig => _leftArmRig;
    public Rig RightLegRig => _rightLegRig;
    public Rig LeftLegRig => _leftLegRig;

    private void Awake()
    {
        _allRigs = new Rig[5] {_rightArmRig,_leftArmRig,_rightLegRig,_leftLegRig, _bodyRig};
        _animator = _reporesentation.GetComponent<Animator>();
    }
    
    public void AnimateRunTree(float speedDelimiter)
    {
        float mag = _determinant.Rigidbody.velocity.magnitude;
        float z = _determinant.PlayerCamera.CameraPivot.InverseTransformDirection(_determinant.Rigidbody.velocity).z;
        float x = _determinant.PlayerCamera.CameraPivot.InverseTransformDirection(_determinant.Rigidbody.velocity).x;
        _animator.SetFloat("Magnitude", mag);
        _animator.SetFloat("VerticalAxis", z / speedDelimiter);
        _animator.SetFloat("HorizontalAxis", x / speedDelimiter);
    }

    public void AdjustRepresentationRotation(Quaternion rot, float smooth)
    {
        _reporesentation.rotation = Quaternion.Slerp(_reporesentation.rotation,
            rot, smooth * Time.fixedDeltaTime);
    }

    public void CrossFade(string stateName, float time, int[] layers) {

        foreach(int index in layers)
            _animator.CrossFadeInFixedTime(stateName, time, index);
    }

    public void BoundConstraintsToTargets()
    {
        //_animator.avatar = null;
        foreach (var rig in _allRigs)
            rig.weight = 1f;
    }

    public void BoundConstraintsToTargets(Rig[] rigs)
    {
        foreach (Rig rig in rigs)
            rig.weight = 1f;
    }

    public void UnBoundConstraints()
    {
        foreach (var rig in _allRigs)
            rig.weight = 0f;

        //_animator.avatar = _avatar;
    }

    public void UnBoundConstraints(Rig[] rigs)
    {
        foreach (var rig in rigs)
            rig.weight = 0f;
    }
}