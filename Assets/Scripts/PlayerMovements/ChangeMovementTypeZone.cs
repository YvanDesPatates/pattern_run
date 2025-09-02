using UnityEngine;

public class ChangeMovementTypeZone : MonoBehaviour
{
    [SerializeField] private GameObject icon;
    
    private MovementEnum _newMovement;

    public void Initialize(Material inconMaterial, MovementEnum newMovement)
    {
        _newMovement = newMovement;
        Renderer iconRenderer = icon.GetComponent<Renderer>();
        iconRenderer.material = inconMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = Util.GetComponentOrLogError<PlayerController>(other.gameObject);
        }
    }
}