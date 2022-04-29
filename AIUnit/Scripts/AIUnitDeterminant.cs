using System;
using UnityEngine;
using UnityEngine.AI;

//An entry point to create AI Unit
//All components that AI is planning interact with
[RequireComponent(typeof(AiUnitStateManager))]
[RequireComponent(typeof(AIUnitTargetSystem))]
[RequireComponent(typeof(AIUnitEvents))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ObjectRoot))]
[RequireComponent(typeof(AIUnitWeapon))]
[RequireComponent(typeof(AIUnitIKPointsController))]

public class AIUnitDeterminant : MonoBehaviour
{
	[SerializeField] private AIUnitData _unitData;
	[SerializeField] private Animator _representationAnimator;
	private AIUnitEvents _events;
	private NavMeshAgent _agent;
	private ObjectRoot _root;
	private AIUnitTargetSystem _target;
	private AIUnitWeapon _weapon;
	private AiUnitStateManager _stateManager;
	private AIUnitIKPointsController _rigController;

	public AIUnitData UnitData => _unitData;
	public AIUnitEvents Events => _events;
	public NavMeshAgent Agent => _agent;
	public ObjectRoot Root => _root;
	public AIUnitTargetSystem Target => _target;
	public AIUnitWeapon Weapon => _weapon;
	public Animator Animator => _representationAnimator;
	public AIUnitIKPointsController RigController => _rigController;

	private void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
		_events = GetComponent<AIUnitEvents>();
		_root = GetComponent<ObjectRoot>();
		_target = GetComponent<AIUnitTargetSystem>();
		_weapon = GetComponent<AIUnitWeapon>();
		_stateManager = GetComponent<AiUnitStateManager>();
		_rigController = GetComponent<AIUnitIKPointsController>();
		_events.onDied += OnDied;
	}

	private void OnDestroy()
	{
		_events.onDied -= OnDied;
	}

	private void OnDied(GameObject gameObject)
	{
		DropWeapon();
		DeactivateInstance();
	}

	private void DropWeapon()
	{
		var dropped = Instantiate(_unitData.Weapon.PropModel, _weapon.GunHolder.transform.position,
			_weapon.GunHolder.transform.rotation);
		dropped.GetComponent<Rigidbody>()
		       .AddForce(_weapon.GunHolder.forward * 250f * Time.fixedDeltaTime, ForceMode.Impulse);
		Destroy(_weapon.WeaponController.gameObject);
	}

	private void DeactivateInstance()
	{
		_root.InstanceCollider.enabled = false;
		_root.InstanceCollider.gameObject.SetActive(false);
		_stateManager.StopAllCoroutines();
		_agent.enabled = false;
		_target.Sight.enabled = false;
		_stateManager.enabled = false;
		_representationAnimator.enabled = false;

		// _sight.SetActive(false);
		// gameObject.SetActive(false);

	}
}