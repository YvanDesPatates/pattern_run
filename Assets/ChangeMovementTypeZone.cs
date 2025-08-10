using System;
using UnityEngine;

public class ChangeMovementTypeZone : MonoBehaviour
{
    private Func<IMovementStrategy> _newMovementStrategy = () => new JumpStrategy();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = Util.GetComponentOrLogError<PlayerController>(other.gameObject);
            player.ChangeMovementStrategy(_newMovementStrategy);
        }
    }
}
