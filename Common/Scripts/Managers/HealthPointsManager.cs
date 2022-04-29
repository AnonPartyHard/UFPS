using UnityEngine;

public class HealthPointsManager : MonoBehaviour
{
    [SerializeField] private float _defaultHP;
    private float _currentHP;
    [SerializeField] private AIUnitEvents _unitEvents;
    [SerializeField] private PlayerActionEvents _playerEvents;
    private Collider _instanceCollider;
    private bool _alive = true;

    private void Awake()
    {
        _currentHP = _defaultHP;
        _instanceCollider = GetComponent<ObjectRoot>().InstanceCollider;
    }

    public void RecieveDamage(float damage)
    {
        _currentHP -= damage;
        if (_currentHP <= 0 && _alive)
            Die();
    }

    private void Die()
    {
        _currentHP = 0;
        _alive = false;
        _unitEvents?.Died(_instanceCollider.gameObject);
        //_playerEvents?.Died();
    }
}