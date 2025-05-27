using System.Collections;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public float maxFogDensity = 1.0f; // Set the maximum fog density
    public float fogIncreaseAmount = 0.5f; // Small increments for a smoother transition
    public float updateInterval = 3f; // Time between fog updates

    void Start() 
    {
        RenderSettings.fog = true;
        RenderSettings.fogDensity = 0.1f; // Start with a lower density
        StartCoroutine(UpdateTheFog());
    }

    IEnumerator UpdateTheFog() 
    {
        while (RenderSettings.fogDensity < maxFogDensity) 
        { 
            yield return new WaitForSeconds(updateInterval);
            RenderSettings.fogDensity = Mathf.Min(RenderSettings.fogDensity + fogIncreaseAmount, maxFogDensity); // Ensure it doesn't exceed maxFogDensity
        }
    }
}
