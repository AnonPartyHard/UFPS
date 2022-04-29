using UnityEngine;

public class PMSCETest : MonoBehaviour
{
    [SerializeField] private PlayerMovementStateChangeEventChannel _channel;

    void Start()
    {
        _channel.onStateChanged += MovementStateChanged;

    }

    private void MovementStateChanged(MovementBaseState newState)
    {
        Debug.Log(newState);
    }
}
