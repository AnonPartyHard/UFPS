using System;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
	public Transform _player;
	public Transform _checkPoint;
	private void OnTriggerEnter(Collider other)
	{
		_player.GetComponent<Rigidbody>().velocity = Vector3.zero;
		_player.position = _checkPoint.position;
		_player.rotation = _checkPoint.rotation;
	}
}
