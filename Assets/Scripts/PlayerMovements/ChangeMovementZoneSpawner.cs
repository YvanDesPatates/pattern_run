using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMovementZoneSpawner : MonoBehaviour, IGameOverSubscriber
{
    [SerializeField] private GameObject changeMovementZonePrefab;
    [SerializeField] private float minSpawningIntervalInS = 5f;
    [SerializeField] private float maxSpawningIntervalInS = 10f;
    [SerializeField] private List<MovementStrategyAndMaterialEntry> iconMaterials;
        
    private bool _isFirstSpawn = true;
    private MovementStrategyEnum _currentStrategy;
    private bool _isGameOver = false;

    public void OnGameOver()
    {
        _isGameOver = true;
    }

    private void Start()
    {
        GameOverPublisher.GetInstance().Subscribe(this);
        StartCoroutine(SpawnChangeMovementZone(0));
    }

    private IEnumerator SpawnChangeMovementZone(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        
        if (_isGameOver) yield break;
        
        InstantiateChangeMovementZone();
        float nextDelay = Random.Range(minSpawningIntervalInS, maxSpawningIntervalInS);
        StartCoroutine(SpawnChangeMovementZone(nextDelay));
    }
    
    private Material GetMaterialForIcon(MovementStrategyEnum type)
    {
        foreach (var entry in iconMaterials)
        {
            if (entry.strategy == type)
                return entry.material;
        }
        Debug.LogWarning($"No material found for strategy: {type}");
        return null;
    }

    private void InstantiateChangeMovementZone()
    {
        GameObject zone = Instantiate(changeMovementZonePrefab, transform.position, Quaternion.identity);
        ChangeMovementTypeZone changeMovementTypeZone = Util.GetComponentOrLogError<ChangeMovementTypeZone>(zone);   
        
        MovementStrategyEnum newMovementStrategy;
        if (_isFirstSpawn)
        {
            newMovementStrategy = MovementStrategyEnum.GravityInvertion;
            _isFirstSpawn = false;
        }
        else
        {
            newMovementStrategy = MovementStrategyEnumUtil.GetRandomMovementStrategyExceptOne(_currentStrategy);   
        }
        
        Material iconMaterial = GetMaterialForIcon(newMovementStrategy);
        changeMovementTypeZone.Initialize(iconMaterial, newMovementStrategy);
        _currentStrategy = newMovementStrategy;
    }
}
