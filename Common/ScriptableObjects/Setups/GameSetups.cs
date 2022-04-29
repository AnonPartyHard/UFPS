using UnityEngine;

[CreateAssetMenu(fileName = "GameSetups", menuName = "ScriptableObjects/Setups/GameSetups", order = 2)]
public class GameSetups : ScriptableObject
{
    [SerializeField] private Vector3 _gravity;

    public Vector3 Gravity => _gravity;
}