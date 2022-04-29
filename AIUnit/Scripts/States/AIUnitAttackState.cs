using UnityEngine;

public class AIUnitAttackState : AIUnitBaseState
{
    private Transform _currentTarget;
    private float _smooth = 25f;
    private bool _needToRotate = false;
    private float _originRigWeight;
    private Animator _animator;
    // private float _delay = 1.5f;

    public override void EnterState(AiUnitStateManager unit)
    {
        _currentTarget = unit.Determinant.Target.GetClosestEnemy().transform;
        _originRigWeight = unit.Determinant.RigController.WeaponHolderRig.weight;
        _animator = unit.Determinant.Animator;
        _animator.SetTrigger("Attack");
        unit.Determinant.RigController.WeaponHolderRig.weight = 1;
        unit.Determinant.Agent.ResetPath();
        unit.Determinant.Events.onWeaponShot += FireAnimation;
    }

    private void FireAnimation(Weapon weapon)
    {
        _animator.Play("Fire", -1, 0f);
        _animator.Play("WHFire", -1, 0f);
    }

    private void LookAtTarget(AiUnitStateManager unit)
    {
        Vector3 targetDirection = _currentTarget.position - unit.Determinant.Weapon.GunHolder.parent.position;
        Quaternion gunTargetRotation = Quaternion.Slerp(unit.Determinant.Weapon.GunHolder.parent.rotation,
            Quaternion.LookRotation(targetDirection),
            _smooth * Time.deltaTime);

        unit.Determinant.Target.Sight.transform.rotation = gunTargetRotation;
    }

    private void RotateBody(AiUnitStateManager unit)
    {
        Vector3 targetDirection = _currentTarget.position - unit.transform.position;
        targetDirection.y = 0;

        if (Vector3.Distance(unit.transform.position, _currentTarget.transform.position) > 2f)
        {
            float angle = Vector3.Angle(unit.transform.forward, targetDirection);

            if (angle > unit.Determinant.UnitData.SightAngle - 10f)
                _needToRotate = true;

            if (angle < 3f)
                _needToRotate = false;

            if (_needToRotate)
                unit.transform.rotation = Quaternion.Slerp(unit.transform.rotation,
                    Quaternion.LookRotation(targetDirection),
                    _smooth * Time.deltaTime);
        }
        else
        {
            unit.transform.rotation = Quaternion.Slerp(unit.transform.rotation,
                Quaternion.LookRotation(targetDirection),
                _smooth * Time.deltaTime);
        }
    }

    public override void UpdateState(AiUnitStateManager unit)
    {
        LookAtTarget(unit);
        RotateBody(unit);
        // if (Time.time > _delay)
            unit.Determinant.Weapon.Fire();
    }

    public override void ExitState(AiUnitStateManager unit)
    {
        _currentTarget = null;
        unit.Determinant.RigController.WeaponHolderRig.weight = _originRigWeight;
        unit.Determinant.Animator.ResetTrigger("Attack");
        unit.Determinant.Events.onWeaponShot -= FireAnimation;
    }
}