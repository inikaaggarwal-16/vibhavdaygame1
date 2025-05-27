using UnityEngine;

public class InfiniteForestLoop : MonoBehaviour
{
    public Transform player; // Assign in Inspector
    public float tileSize = 50f; // Adjust based on your forest's size

    private Vector3 lastPlayerPosition;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("❌ ERROR: Player reference is missing! Assign the player in the Inspector.");
            return;
        }

        lastPlayerPosition = player.position;
        Debug.Log("✅ Forest script started. Waiting for player movement...");
    }

    void Update()
    {
        if (player == null) return; // Safety check

        Debug.Log("🔄 Checking player movement...");

        Vector3 playerMovement = player.position - lastPlayerPosition;
        Debug.Log($"📍 Player Position: {player.position} | Last Position: {lastPlayerPosition}");

        // Move in X direction
        if (Mathf.Abs(playerMovement.x) >= tileSize)
        {
            float moveDirection = Mathf.Sign(playerMovement.x);
            transform.position += new Vector3(moveDirection * tileSize, 0, 0);
            lastPlayerPosition = player.position;

            Debug.Log($"🌲 Forest moved in X direction by {moveDirection * tileSize}");
        }

        // Move in Z direction
        if (Mathf.Abs(playerMovement.z) >= tileSize)
        {
            float moveDirection = Mathf.Sign(playerMovement.z);
            transform.position += new Vector3(0, 0, moveDirection * tileSize);
            lastPlayerPosition = player.position;

            Debug.Log($"🌲 Forest moved in Z direction by {moveDirection * tileSize}");
        }
    }
}
