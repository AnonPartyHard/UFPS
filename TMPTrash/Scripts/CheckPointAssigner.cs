using UnityEngine;

public class CheckPointAssigner : MonoBehaviour
{
    public Transform point;
    public DeathTrigger deathTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Body")
            deathTrigger._checkPoint = point;
    }
}
