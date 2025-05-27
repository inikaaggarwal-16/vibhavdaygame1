using UnityEngine;
using System.Collections;

public class ImageDeactivator : MonoBehaviour
{
    public GameObject[] imagesToDeactivate; // Images that will deactivate
    public GameObject[] triggerObjects; // Objects that, when activated, will trigger image deactivation

    private bool isCoroutineRunning = false;
    private int imageIndex = 0;

    void Update()
    {
        CheckTriggerObjects();
    }

    void CheckTriggerObjects()
    {
        foreach (GameObject obj in triggerObjects)
        {
            if (obj.activeSelf) // If any trigger object is active
            {
                if (!isCoroutineRunning)
                {
                    StartCoroutine(DeactivateImagesOneByOne());
                }
                break;
            }
        }
    }

    IEnumerator DeactivateImagesOneByOne()
    {
        isCoroutineRunning = true;
        while (imageIndex < imagesToDeactivate.Length)
        {
            if (imagesToDeactivate[imageIndex] != null && imagesToDeactivate[imageIndex].activeSelf)
            {
                imagesToDeactivate[imageIndex].SetActive(false);
                yield return new WaitForSeconds(3f); // Same delay logic as your script
            }
            imageIndex++;
        }
        isCoroutineRunning = false;
    }
}
