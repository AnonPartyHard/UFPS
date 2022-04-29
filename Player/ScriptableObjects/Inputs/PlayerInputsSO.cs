using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum InputKeys
{
    JUMP,
    SPRINT,
    DUCK,
    ACTION,
    FIRE,
    ALTFIRE,
    DROP
}

[System.Serializable]
public class InputKey 
{
    [SerializeField] private InputKeys _keyName;
    [SerializeField] private KeyCode _keyCode;

    public InputKeys KeyName => _keyName;
    public KeyCode KeyCode => _keyCode;

    public InputKey(InputKeys keyName, KeyCode keyCode)
    {
        _keyName = keyName;
        _keyCode = keyCode;
    }
}

[CreateAssetMenu(fileName = "Inputs", menuName = "ScriptableObjects/Player/Inputs", order = 1)] 

public class PlayerInputsSO : ScriptableObject
{
    [SerializeField] private string horizontalMoveAxis = "Horizontal";
    [SerializeField] private string verticalMoveAxis = "Vertical";
    [SerializeField] private string horizontalMouseAxis = "Mouse X";
    [SerializeField] private string verticalMouseAxis = "Mouse Y";
    [SerializeField] private List<InputKey> InputsList;
    
    public Vector2 GetMouseVector()
    {
        return new Vector2(Input.GetAxis(horizontalMouseAxis), Input.GetAxis(verticalMouseAxis));
    }
    
    public Vector2 GetMovementVector()
    {
        return new Vector2(Input.GetAxisRaw(horizontalMoveAxis), Input.GetAxisRaw(verticalMoveAxis));
    }

    public bool IsKeyPressed(InputKeys keyName)
    {
        return Input.GetKeyDown(InputsList.First(i => i.KeyName == keyName).KeyCode);
    }
    
    public bool IsKeyDown(InputKeys keyName)
    {
        return Input.GetKey(InputsList.First(i => i.KeyName == keyName).KeyCode);
    }
    
    public bool IsKeyReleased(InputKeys keyName)
    {
        return Input.GetKeyUp(InputsList.First(i => i.KeyName == keyName).KeyCode);
    }
}
