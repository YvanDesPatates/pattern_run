using System;
using UnityEngine;

public class ChangeMovementTypeZone : MonoBehaviour
{
    private Func<IMovementStrategy> _newMovementStrategy = () => new GravityInvertionStrategy();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if (other.CompareTag("Player"))
        {
            Debug.Log("player trigger");
            PlayerController player = Util.GetComponentOrLogError<PlayerController>(other.gameObject);
            player.ChangeMovementStrategy(_newMovementStrategy);
        }
    }
}
