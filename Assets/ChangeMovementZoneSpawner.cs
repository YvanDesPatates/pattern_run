using System.Collections;
using UnityEngine;

public class ChangeMovementZoneSpawner : MonoBehaviour, IGameOverSubscriber
{
    [SerializeField] private GameObject changeMovementZonePrefab;
    [SerializeField] private float minSpawningIntervalInS = 5f;
    [SerializeField] private float maxSpawningIntervalInS = 30f;
    
    private bool _isGameOver = false;

    public void OnGameOver()
    {
        _isGameOver = true;
    }

    private void Start()
    {
        GameOverPublisher.GetInstance().Subscribe(this);
        StartCoroutine(SpawnChangeMovementZone(8));
    }

    private IEnumerator SpawnChangeMovementZone(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        
        if (_isGameOver) yield break;
        
        Instantiate(changeMovementZonePrefab, transform.position, Quaternion.identity);
        float nextDelay = Random.Range(minSpawningIntervalInS, maxSpawningIntervalInS);
        StartCoroutine(SpawnChangeMovementZone(nextDelay));
    }
}
