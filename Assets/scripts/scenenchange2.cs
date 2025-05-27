using UnityEngine;
using UnityEngine.SceneManagement; // Import Scene Management

public class ChangeSceneOnProximity : MonoBehaviour
{
    public Transform targetObject; // The object to check proximity with
    public float activationDistance = 5f; // Distance threshold for scene change
    private bool hasSceneChanged = false; // Prevent multiple scene changes

    private void Update()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("Target object is not assigned!");
            return;
        }

        float distance = Vector3.Distance(transform.position, targetObject.position);

        // Check if distance is within activation range and scene has not changed yet
        if (distance <= activationDistance && !hasSceneChanged)
        {
            hasSceneChanged = true; // Ensure scene change happens only once
            Debug.Log("Proximity detected! Changing scene...");
            SceneManager.LoadScene("SampleScene"); // Change to your scene name
        }
    }
}
