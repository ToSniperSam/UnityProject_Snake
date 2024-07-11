using UnityEngine;
using System.Collections;
 
//Add this script to the platform you want to move.
//左右移动的平台
public class MovingPlatform : MonoBehaviour {
    private static MovingPlatform _instance;
    
    public static MovingPlatform Instance
    {
        get
        {
            return _instance;
        }
    }
    
    private void Awake()
    {
        _instance = this;
    }
    
    public float moveRange = 5f;    // Maximum distance the platform will move from its starting position
    public float moveSpeed = 1f;    // Speed of the platform

    private Vector3 startPosition;
    private Vector3 moveDirection;    // Random movement direction

    void Start()
    {
        // Initialize start position
        startPosition = transform.position;

        // Generate a random movement direction
        moveDirection = Random.insideUnitCircle.normalized;
    }

    void Update()
    {
        // Calculate the movement
        float pingPongValue = Mathf.PingPong(Time.time * moveSpeed, 1);
        float distance = Mathf.Lerp(0, moveRange, pingPongValue);
        Vector3 offset = moveDirection * distance;
        Vector3 newPosition = startPosition + offset;

        // Update the platform's position
        transform.position = newPosition;
    }
	
 
}