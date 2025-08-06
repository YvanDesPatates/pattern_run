using UnityEngine;

public class MoveLeft : MonoBehaviour, IGameOverSubscriber
{
    public float speed;

    private const float LeftBound = -15;

    private void Start()
    {
        GameOverPublisher.GetInstance().Subscribe(this);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.left);
        
        if (transform.position.x < LeftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    public void OnGameOver()
    {
        speed = 0;
    }
}