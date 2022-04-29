using System;
using UnityEngine;

public class GameSetupsIdentifier : MonoBehaviour
{
    [SerializeField] private GameSetups _gameSetups;

    private void Awake()
    {
        Physics.gravity = _gameSetups.Gravity;
        QualitySettings.vSyncCount = 0;
    }
}