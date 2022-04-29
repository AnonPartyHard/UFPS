using UnityEngine;
public class SyncTransform : MonoBehaviour
{
	[SerializeField] private Transform _target;
	private Transform origin;

	private void Awake()
	{
		origin = transform;
	}

	private void Update()
	{
		origin.position = _target.position;
		origin.rotation = _target.rotation;
	}
}