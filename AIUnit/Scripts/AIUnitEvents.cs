using UnityEngine;

[RequireComponent(typeof(AIUnitDeterminant))]
public class AIUnitEvents : MonoBehaviour
{
	public delegate void AIUnitObserveObjectAction(GameObject gameObject);

	public AIUnitObserveObjectAction onObjectNoticed;
	public AIUnitObserveObjectAction onEnemyNoticed;
	public AIUnitObserveObjectAction onObjectLost;
	public AIUnitObserveObjectAction onEnemyLost;

	public delegate void AIUnitWeaponAction(Weapon weapon);

	public AIUnitWeaponAction onWeaponShot;

	public delegate void AIUnitOtherAction(GameObject gameObject);

	public AIUnitOtherAction onDied;

	public void ObjectNoticed(GameObject gameObject)
	{
		onObjectNoticed?.Invoke(gameObject);
	}

	public void EnemyNoticed(GameObject gameObject)
	{
		onEnemyNoticed?.Invoke(gameObject);
	}

	public void ObjectLost(GameObject gameObject)
	{
		onObjectLost?.Invoke(gameObject);
	}

	public void EnemyLost(GameObject gameObject)
	{
		onEnemyLost?.Invoke(gameObject);
	}

	public void WeaponShot(Weapon weapon)
	{
		onWeaponShot?.Invoke(weapon);
	}

	public void Died(GameObject gameObject)
	{
		onDied?.Invoke(gameObject);
	}
}