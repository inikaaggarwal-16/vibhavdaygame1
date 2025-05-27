/*using System.Collections;
using UnityEngine;

public class DelayedObjectAppear : MonoBehaviour
{
    public GameObject objectToAppear;  // The object (spider) to appear after delay
    public float delayTime = 20f;      // Delay time in seconds (when the object will appear)
    public float verticalOffset = 2f;  // Vertical offset to make the object appear a little upwards
    public float objectLifetime = 10f; // Time in seconds before the object disappears after appearing
    public float followDistance = 5f;  // Distance in front of the camera the object should follow
    public float followSpeed = 2f;     // Speed at which the object follows the camera

    private bool isFollowing = false;  // Flag to control if the object should follow the camera

    void Start()
    {
        // Start the coroutine to handle the object's delayed appearance and behavior
        StartCoroutine(ShowObjectOnce());
    }

    void Update()
    {
        // If the object is following the camera, update its position to follow the camera
        if (isFollowing && objectToAppear != null)
        {
            // Get the camera's position and forward direction
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 cameraForward = Camera.main.transform.forward;

            // Calculate the new position in front of the camera, considering vertical offset
            Vector3 targetPosition = cameraPosition + cameraForward * followDistance;
            targetPosition.y += verticalOffset; // Apply the vertical offset

            // Move the object towards the calculated position
            objectToAppear.transform.position = Vector3.Lerp(objectToAppear.transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    IEnumerator ShowObjectOnce()
    {
        // Start by making the object inactive
        objectToAppear.SetActive(false);

        // Wait for the specified delay time before making the object appear
        yield return new WaitForSeconds(delayTime);

        // After 20 seconds, make the object visible
        if (objectToAppear != null)
        {
            objectToAppear.SetActive(true);

            // Start following the camera
            isFollowing = true;

            // Wait for the object to be visible for the specified lifetime (10 seconds)
            yield return new WaitForSeconds(objectLifetime);

            // Stop following the camera and disable the object after the specified time (10 seconds after appearing)
            isFollowing = false;
            objectToAppear.SetActive(false);
        }
    }
}
*/

using System.Collections;
using UnityEngine;

public class DelayedObjectAppear : MonoBehaviour
{
    public GameObject objectToAppear;  // The object (spider) to appear after delay
    public float delayTime = 20f;      // Delay time in seconds (when the object will appear)
    public float verticalOffset = 2f;  // Vertical offset to make the object appear a little upwards
    public float objectLifetime = 10f; // Time in seconds before the object disappears after appearing
    public float followDistance = 5f;  // Distance in front of the camera the object should follow
    public float followSpeed = 2f;     // Speed at which the object follows the camera
    public float rotationSpeed = 5f;   // Speed at which the object rotates to face the camera

    private bool isFollowing = false;  // Flag to control if the object should follow the camera

    void Start()
    {
        // Start the coroutine to handle the object's delayed appearance and behavior
        StartCoroutine(ShowObjectOnce());
    }

    void Update()
    {
        // If the object is following the camera, update its position to follow the camera
        if (isFollowing && objectToAppear != null)
        {
            // Get the camera's position and forward direction
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 cameraForward = Camera.main.transform.forward;

            // Calculate the new position in front of the camera, considering vertical offset
            Vector3 targetPosition = cameraPosition + cameraForward * followDistance;
            targetPosition.y += verticalOffset; // Apply the vertical offset

            // Move the object towards the calculated position
            objectToAppear.transform.position = Vector3.Lerp(objectToAppear.transform.position, targetPosition, followSpeed * Time.deltaTime);

            // Rotate the object to face the opposite direction of the camera
            Vector3 directionAwayFromCamera = objectToAppear.transform.position - Camera.main.transform.position;
            directionAwayFromCamera.y = 0; // Keep the object from rotating up and down (optional)
            Quaternion targetRotation = Quaternion.LookRotation(directionAwayFromCamera);
            objectToAppear.transform.rotation = Quaternion.Slerp(objectToAppear.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator ShowObjectOnce()
    {
        // Start by making the object inactive
        objectToAppear.SetActive(false);

        // Wait for the specified delay time before making the object appear
        yield return new WaitForSeconds(delayTime);

        // After the delay, make the object visible
        if (objectToAppear != null)
        {
            objectToAppear.SetActive(true);

            // Start following the camera
            isFollowing = true;

            // Wait for the object to be visible for the specified lifetime (10 seconds)
            yield return new WaitForSeconds(objectLifetime);

            // Stop following the camera and disable the object after the specified time
            isFollowing = false;
            objectToAppear.SetActive(false);
        }
    }
}
