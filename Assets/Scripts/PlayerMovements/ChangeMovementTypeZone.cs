using UnityEngine;

public class ChangeMovementTypeZone : MonoBehaviour
{
    [SerializeField] private GameObject icon;
    
    private MovementStrategyEnum _newMovementStrategy;

    public void Initialize(Material inconMaterial, MovementStrategyEnum newMovementStrategy)
    {
        _newMovementStrategy = newMovementStrategy;
        Renderer iconRenderer = icon.GetComponent<Renderer>();
        iconRenderer.material = inconMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = Util.GetComponentOrLogError<PlayerController>(other.gameObject);
            player.ChangeMovementStrategy(_newMovementStrategy);
        }
    }
}
