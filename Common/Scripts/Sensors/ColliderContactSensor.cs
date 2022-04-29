using System;
using UnityEngine;

public class ColliderContactSensor : MonoBehaviour
{
	private bool _isOverLap;
	public bool IsOverlap => _isOverLap;
	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("col enter");
		_isOverLap = true;
	}

	private void OnCollisionExit(Collision other)
	{
		Debug.Log("col exit");
		_isOverLap = false;
	}
}
