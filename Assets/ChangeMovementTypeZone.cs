using System;
using UnityEngine;

public class ChangeMovementTypeZone : MonoBehaviour
{
    private Func<IMovementStrategy> _newMovementStrategy = () => new GravityInvertionStrategy();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = Util.GetComponentOrLogError<PlayerController>(other.gameObject);
            player.ChangeMovementStrategy(_newMovementStrategy);
        }
    }
}
