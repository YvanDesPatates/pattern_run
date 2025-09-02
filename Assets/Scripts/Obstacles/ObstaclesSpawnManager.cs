using UnityEngine;
using Random = UnityEngine.Random;

public class ObstaclesSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float minRangeSpawnInSec;
    [SerializeField] private float maxRangeSpawnInSec;

    [Tooltip("Y position where the obstacles will be spawned. Default is 0 to spawn on the ground.")]
    [SerializeField] private float ySpawnPosition = 0f;

    private Vector3 _spawnPos;
    private float _lastSpawnTime;
    private float _spawnCoolDown = 0;
    private bool _isGameOver;
    
    private void Start()
    {
        _spawnPos = new Vector3(25, ySpawnPosition, 0);
        _lastSpawnTime = Time.time;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isGameOver) return;

        if (Time.time - _lastSpawnTime > _spawnCoolDown)
        {
            SpawnObstacle();
            _lastSpawnTime = Time.time;
            _spawnCoolDown = Random.Range(minRangeSpawnInSec, maxRangeSpawnInSec);
        }
    }

    private void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, _spawnPos, obstaclePrefab.transform.rotation);
    }
}