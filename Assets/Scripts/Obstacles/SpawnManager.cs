using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float minRangeSpawnInSec;
    public float maxRangeSpawnInSec;
    
    private readonly Vector3 _spawnPos = new Vector3(25, 0, 0);
    private float _lastSpawnTime;
    private float _spawnCoolDown = 0;
    private PlayerController _playerControllerScript;

    private void Start()
    {
        _lastSpawnTime = Time.time;
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( ! _playerControllerScript.IsGameOver() )
        {
            if (Time.time - _lastSpawnTime > _spawnCoolDown)
            {
                SpawnObstacle();
                _lastSpawnTime = Time.time;
                _spawnCoolDown = Random.Range(minRangeSpawnInSec, maxRangeSpawnInSec);
            }
        }
    }

    private void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, _spawnPos, obstaclePrefab.transform.rotation);
    }
}
