using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Constant speed

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime; // Move the player forward
    }
}
