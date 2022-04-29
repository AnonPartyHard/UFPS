using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefabToSpawn;
    [SerializeField] private float _step = 2f;

    private float _lastSpawnedTime = 0f;

    private void Update()
    {
        if (_prefabToSpawn != null)
            if (Time.time > _lastSpawnedTime + _step)
            {
                _lastSpawnedTime = Time.time;
                Instantiate(_prefabToSpawn, transform.position, Quaternion.identity);
            }
    }
}