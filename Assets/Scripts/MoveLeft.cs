using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveLeft : MonoBehaviour
{
    public float speed;
    
    private PlayerController _playerControllerScript;
    private float _leftBound = -15; 
    private void Start()
    {
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( ! _playerControllerScript.IsGameOver() )
        {
            transform.Translate(speed * Time.deltaTime * Vector3.left);
        }

        if (transform.position.x < _leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
