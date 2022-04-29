using UnityEngine;

public class WeaponPropIdentifier : MonoBehaviour
{
    [SerializeField] private Weapon _data;

    public Weapon Data => _data;
}