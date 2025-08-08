using System.Collections.Generic;
using UnityEngine;

public class RandomizeAssets : MonoBehaviour
{
    [SerializeField] private List<GameObject> smallObstacles;
    [SerializeField] private List<GameObject> largeObstacles;
    [SerializeField] private Transform assetsParent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (smallObstacles == null || smallObstacles.Count == 0 || largeObstacles == null || largeObstacles.Count == 0)
        {
            Debug.LogError("Obstacle lists are not set or empty");
            return;
        }

        int randomIndex = Random.Range(0, smallObstacles.Count);
        GameObject selectedObstacle = smallObstacles[randomIndex];
        selectedObstacle.SetActive(true);
        
        randomIndex = Random.Range(0, largeObstacles.Count);
        GameObject selectedLargeObstacle = largeObstacles[randomIndex];
        selectedLargeObstacle.SetActive(true);
        
        //1/2 chance to 180 transform on y axis
        if (Random.value > 0.5f)
        {
            assetsParent.Rotate(new Vector3(0, 180, 0));
        }
    }
}
