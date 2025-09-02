using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChangeMovementZoneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject changeMovementZonePrefab;
    [SerializeField] private float minSpawningIntervalInS = 5f;
    [SerializeField] private float maxSpawningIntervalInS = 10f;
    [SerializeField] private List<MovementEnumAndMaterialEntry> iconMaterials;
        
    private MovementEnum _current;

    private void Start()
    {
        StartCoroutine(SpawnChangeMovementZone(0));
    }

    private IEnumerator SpawnChangeMovementZone(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        
        InstantiateChangeMovementZone();
        float nextDelay = Random.Range(minSpawningIntervalInS, maxSpawningIntervalInS);
        StartCoroutine(SpawnChangeMovementZone(nextDelay));
    }
    
    private Material GetMaterialForIcon(MovementEnum type)
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
        
        MovementEnum newMovement = (MovementEnum) Random.Range(0, Enum.GetValues(typeof(MovementEnum)).Length);
        
        Material iconMaterial = GetMaterialForIcon(newMovement);
        changeMovementTypeZone.Initialize(iconMaterial, newMovement);
        _current = newMovement;
    }
}