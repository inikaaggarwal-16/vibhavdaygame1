using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Time in seconds after which the scene will change
    private float timeToWait = 40f;

    void Start()
    {
        // Start the timer when the game starts
        Invoke("ChangeScene", timeToWait);
    }

    // This method will change the scene to the one named "next"
    void ChangeScene()
    {
        SceneManager.LoadScene("next");
    }
}
