using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed;

    private const float LeftBound = -15;

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.left);
        
        if (transform.position.x < LeftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
    
}