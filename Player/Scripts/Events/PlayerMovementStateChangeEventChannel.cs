using UnityEngine;

[CreateAssetMenu(fileName = "CameraSetups", menuName = "ScriptableObjects/Player/MovementStateChangeEventChannel", order = 1)]

public class PlayerMovementStateChangeEventChannel : ScriptableObject
{
	public delegate void MovementStateChangeEvent(MovementBaseState newState);
	public MovementStateChangeEvent onStateChanged;
	
	public void StateChanged(MovementBaseState newState)
	{
		onStateChanged?.Invoke(newState);
	}
}
