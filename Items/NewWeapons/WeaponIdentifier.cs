using UnityEngine;

public class WeaponIdentifier : MonoBehaviour
{
    [SerializeField] private GameObject _weaponFPPrefab;
    public GameObject FP_Prefab => _weaponFPPrefab;
}
