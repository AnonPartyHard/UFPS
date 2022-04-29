using System;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<DamageReciever>() != null)
            collision.gameObject.GetComponent<DamageReciever>().RecieveDamage(20f);
    }
}