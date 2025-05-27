using UnityEngine;

public class ActivateOnProximity : MonoBehaviour
{
    public GameObject objectToActivate; // The GameObject you want to activate
    public Transform targetObject; // The object you want to check proximity with
    public float activationDistance = 5f; // The distance at which activation happens

    private void Update()
    {
        // Calculate the distance between the parent object and the target object
        float distance = Vector3.Distance(transform.position, targetObject.position);

        // Debug log to show the current distance
        Debug.Log("Current distance to target: " + distance);

        // If the distance is less than or equal to the specified activation distance
        if (distance <= activationDistance)
        {
            // Activate the specified GameObject if it is not already active
            if (objectToActivate != null && !objectToActivate.activeSelf)
            {
                objectToActivate.SetActive(true);
                Debug.Log("Object activated because parent is within range.");
            }
        }
        else
        {
            // Deactivate the specified GameObject if it is active and distance exceeds the threshold
            if (objectToActivate != null && objectToActivate.activeSelf)
            {
                objectToActivate.SetActive(false);
                Debug.Log("Object deactivated because parent is out of range.");
            }
        }
    }
}
