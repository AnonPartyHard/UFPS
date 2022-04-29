using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolableObjectList
{
    [SerializeField] private string _id;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _count;
    private List<GameObject> _instantiatedObjects;

    public string Id => _id;

    public GameObject Prefab => _prefab;

    public int Count => _count;

    public List<GameObject> InstantiatedObjects => _instantiatedObjects;

    public PoolableObjectList(string id, GameObject prefab)
    {
        _id = id;
        _prefab = prefab;
        _instantiatedObjects = new List<GameObject>();
    }
}

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private List<PoolableObjectList> _poolsToAdd;
    private List<PoolableObjectList> _poolableObjectsLists;
    public static ObjectsPool instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        _poolableObjectsLists = new List<PoolableObjectList>();

        foreach (var poolableObject in _poolsToAdd)
            AddObjectsPool(poolableObject.Id, poolableObject.Prefab, poolableObject.Count);
    }

    private int GetPoolIndex(string id)
    {
        return _poolableObjectsLists.FindIndex(o => o.Id == id);
    }

    public void AddObjectsPool(string id, GameObject prefab, int count)
    {
        int foundIndex = GetPoolIndex(id);
        if (foundIndex < 0)
        {
            var poolableObjectList = new PoolableObjectList(id, prefab);
            for (var i = 0; i < count; i++)
            {
                poolableObjectList.InstantiatedObjects.Add(CreateGameObject(prefab));
            }

            _poolableObjectsLists.Add(poolableObjectList);
        }
        else
        {
            for (var i = 0; i < count; i++)
            {
                _poolableObjectsLists[foundIndex].InstantiatedObjects.Add(CreateGameObject(prefab));
            }
        }
    }

    private GameObject CreateGameObject(GameObject prefab)
    {
        GameObject tmp = Instantiate(prefab, transform);
        tmp.transform.position = Vector3.zero;
        tmp.SetActive(false);
        return tmp;
    }

    public GameObject GetObject(string id, Vector3 position, Quaternion rotation)
    {
        int foundIndex = GetPoolIndex(id);
        if (foundIndex > -1)
        {
            var foundList = _poolableObjectsLists[foundIndex];
            GameObject go = GetInactiveObject(foundList.InstantiatedObjects);
            if (go != null)
            {
                go.transform.position = position;
                go.transform.rotation = rotation;
                go.SetActive(true);
            }
            else
            {
                go = Instantiate(foundList.Prefab, position, rotation, transform);
                foundList.InstantiatedObjects.Add(go);
            }
            return go;
        }
        return null;
    }

    private GameObject GetInactiveObject(List<GameObject> list)
    {
        GameObject res = null;
        foreach (GameObject go in list)
        {
            if (!go.activeInHierarchy)
                res = go;
        }

        return res;
    }

    public void ReturnObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.position = Vector3.zero;
        gameObject.transform.rotation = Quaternion.identity;
    }

    public void RemoveObjectsFromPool(string id, int count)
    {
        int foundIndex = GetPoolIndex(id);
        if (foundIndex > -1)
        {
            List<GameObject> objects = _poolableObjectsLists[foundIndex].InstantiatedObjects;
            for (int i = count - 1; i >= 0; i--)
            {
                if (objects[i] != null && !objects[i].activeInHierarchy)
                {
                    Destroy(objects[i]);
                    objects.RemoveAt(i);
                }
            }
        }
    }
}