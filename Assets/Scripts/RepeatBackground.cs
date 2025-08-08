using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    [Tooltip("more the offset is, more the background will move left before repeating")]
    [SerializeField] private float xOffset = 100f;
    private Vector3 _startPos;
    
    // Start is called before the first frame update
    private void Start()
    {
        _startPos = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.x < _startPos.x - xOffset)
        {
            transform.position = _startPos;
        }
    }
}
