using UnityEngine;

public class PersistentCanvas : MonoBehaviour
{
    public static PersistentCanvas instance;

    void Awake()
    {
        // Ensure only one instance of the Canvas exists across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevents destruction when changing scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate Canvas instances
        }
    }
}
