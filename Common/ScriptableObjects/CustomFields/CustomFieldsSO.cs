using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class FloatField
{
    [SerializeField] private string _name;
    [SerializeField] private float _value;
    public string Name => _name;
    public float Value => _value;
}
[System.Serializable]
public class IntField
{
    [SerializeField] private string _name;
    [SerializeField] private int _value;
    public string Name => _name;
    public int Value => _value;
}
[System.Serializable]
public class StringField
{
    [SerializeField] private string _name;
    [SerializeField] private string _value;
    public string Name => _name;
    public string Value => _value;
}
[System.Serializable]
public class GameObjectField
{
    [SerializeField] private string _name;
    [SerializeField] private GameObject _value;
    public string Name => _name;
    public GameObject Value => _value;
}

[CreateAssetMenu(fileName = "CustomFields", menuName = "ScriptableObjects/CustomFields/CustomFields", order = 1)]
public class CustomFieldsSO : ScriptableObject
{
    [SerializeField] private List<FloatField> _floatFields;
    [SerializeField] private List<StringField> _stringFields;
    [SerializeField] private List<IntField> _intFields;
    [SerializeField] private List<GameObjectField> _gameObjectFields;

    public float GetFloat(string floatName)
    {
        return _floatFields.Find(f => f.Name == floatName).Value;
    }
    public int GetInt(string floatName)
    {
        return _intFields.Find(f => f.Name == floatName).Value;
    }
    public string GetString(string stringName)
    {
        return _stringFields.Find(f => f.Name == stringName).Value;
    }
    
    public GameObject GetGameObject(string gameObjectName)
    {
        return _gameObjectFields.Find(f => f.Name == gameObjectName).Value;
    }
}
