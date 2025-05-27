using UnityEngine;

public class Oscillator : MonoBehaviour
{
    public float amplitude = 2f; // The maximum distance from the starting position
    public float speed = 2f; // Speed of oscillation
    public Vector3 direction = Vector3.up; // Direction of oscillation (default: Up/Down)

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // Store the initial position
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * amplitude; // Calculate oscillation
        transform.position = startPos + direction * offset; // Apply oscillation
    }
}

