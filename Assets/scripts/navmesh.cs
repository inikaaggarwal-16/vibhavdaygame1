/*using UnityEngine;
using TMPro;
using System.Collections;

public class EnemyChase : MonoBehaviour
{
    public GameObject enemy; // The main enemy that chases
    public string enemyTag = "Enemy";  // Tag for additional enemies
    public float speed = 5f;
    public float catchDistance = 3f;
    public TextMeshProUGUI messageText;
    public GameObject[] imagesToDeactivate;

    private bool isChasing = false;
    private float timer = 0f;
    private int imageIndex = 0;
    private bool isCoroutineRunning = false;

    void Start()
    {
        if (enemy == null)
        {
            Debug.LogError("Enemy not assigned! Please assign an enemy GameObject in the inspector.");
            return;
        }

        if (messageText != null) messageText.text = "";

        if (imagesToDeactivate != null)
        {
            foreach (GameObject image in imagesToDeactivate)
                if (image != null) image.SetActive(true);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 15f) isChasing = true;

        if (isChasing)
        {
            ChaseTarget();
        }

        CheckProximityToEnemies(); // New function to check other "Enemy" objects
    }

    // The main enemy chases the camera
    void ChaseTarget()
    {
        if (enemy == null) return;

        Transform cameraTransform = Camera.main.transform;
        Vector3 direction = cameraTransform.position - enemy.transform.position;
        direction.y = 0; // Keep movement on the XZ plane

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, Time.deltaTime * speed);
        }

        enemy.transform.position += direction.normalized * speed * Time.deltaTime;

        if ((enemy.transform.position - cameraTransform.position).sqrMagnitude <= catchDistance * catchDistance)
        {
            if (messageText != null) messageText.text = "Enemy is too close!";

            if (!isCoroutineRunning && imageIndex < imagesToDeactivate.Length)
            {
                StartCoroutine(DeactivateImageWithDelay());
            }
        }
        else
        {
            StopAllCoroutines();
            isCoroutineRunning = false;
        }
    }

    // New logic to check for other enemies that don’t chase but affect the UI
    void CheckProximityToEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject otherEnemy in enemies)
        {
            if (otherEnemy != enemy) // Ensure it doesn’t affect the main chaser enemy logic
            {
                if ((otherEnemy.transform.position - Camera.main.transform.position).sqrMagnitude <= catchDistance * catchDistance)
                {
                    if (messageText != null) messageText.text = "An enemy is too close!";

                    if (!isCoroutineRunning && imageIndex < imagesToDeactivate.Length)
                    {
                        StartCoroutine(DeactivateImageWithDelay());
                    }
                    return; // Stop checking after first detected
                }
            }
        }
    }

    IEnumerator DeactivateImageWithDelay()
    {
        isCoroutineRunning = true;
        while (imageIndex < imagesToDeactivate.Length)
        {
            if (!IsAnyEnemyTooClose())
            {
                isCoroutineRunning = false;
                yield break;
            }

            if (imagesToDeactivate[imageIndex] != null)
                imagesToDeactivate[imageIndex].SetActive(false);

            imageIndex++;
            yield return new WaitForSeconds(3f);
        }

        isCoroutineRunning = false;
    }

    bool IsAnyEnemyTooClose()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject otherEnemy in enemies)
        {
            if ((otherEnemy.transform.position - Camera.main.transform.position).sqrMagnitude <= catchDistance * catchDistance)
            {
                return true;
            }
        }
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obs")) 
        {
            DeactivateNextImageInstantly();
        }
    }

    void DeactivateNextImageInstantly()
    {
        if (imageIndex < imagesToDeactivate.Length && imagesToDeactivate[imageIndex] != null)
        {
            imagesToDeactivate[imageIndex].SetActive(false);
            imageIndex++;
        }
    }
}




using UnityEngine;
using System.Collections;

public class EnemyChase : MonoBehaviour
{
    public GameObject enemy; // The main enemy that chases
    public string enemyTag = "Enemy";  // Tag for additional enemies
    public float speed = 5f;
    public float catchDistance = 3f;
    
    public GameObject[] imagesToDeactivate;

    private bool isChasing = false;
    private float timer = 0f;
    private int imageIndex = 0;
    private bool isCoroutineRunning = false;

    void Start()
    {
        if (enemy == null)
        {
            Debug.LogError("Enemy not assigned! Please assign an enemy GameObject in the inspector.");
            return;
        }

        if (messageText != null) messageText.text = "";

        if (imagesToDeactivate != null)
        {
            foreach (GameObject image in imagesToDeactivate)
                if (image != null) image.SetActive(true);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 15f) isChasing = true;

        if (isChasing)
        {
            ChaseTarget();
        }

        CheckProximityToEnemies();
    }

    void ChaseTarget()
    {
        if (enemy == null) return;

        Transform cameraTransform = Camera.main.transform;
        Vector3 direction = cameraTransform.position - enemy.transform.position;
        direction.y = 0;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, Time.deltaTime * speed);
        }

        enemy.transform.position += direction.normalized * speed * Time.deltaTime;

        if ((enemy.transform.position - cameraTransform.position).sqrMagnitude <= catchDistance * catchDistance)
        {
            if (messageText != null) messageText.text = "Enemy is too close!";

            if (!isCoroutineRunning && imageIndex < imagesToDeactivate.Length)
            {
                StartCoroutine(DeactivateImageWithDelay());
            }
        }
        else
        {
            StopAllCoroutines();
            isCoroutineRunning = false;
        }
    }

    void CheckProximityToEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject otherEnemy in enemies)
        {
            if (otherEnemy != enemy)
            {
                if ((otherEnemy.transform.position - Camera.main.transform.position).sqrMagnitude <= catchDistance * catchDistance)
                {
                    if (messageText != null) messageText.text = "An enemy is too close!";

                    if (!isCoroutineRunning && imageIndex < imagesToDeactivate.Length)
                    {
                        StartCoroutine(DeactivateImageWithDelay());
                    }
                    return;
                }
            }
        }
    }

    IEnumerator DeactivateImageWithDelay()
    {
        isCoroutineRunning = true;
        while (imageIndex < imagesToDeactivate.Length)
        {
            if (!IsAnyEnemyTooClose())
            {
                isCoroutineRunning = false;
                yield break;
            }

            if (imagesToDeactivate[imageIndex] != null)
                imagesToDeactivate[imageIndex].SetActive(false);

            imageIndex++;
            
            if (imageIndex >= imagesToDeactivate.Length)
            {
                EndGame();
                yield break;
            }
            
            yield return new WaitForSeconds(3f);
        }

        isCoroutineRunning = false;
    }

    bool IsAnyEnemyTooClose()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject otherEnemy in enemies)
        {
            if ((otherEnemy.transform.position - Camera.main.transform.position).sqrMagnitude <= catchDistance * catchDistance)
            {
                return true;
            }
        }
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obs")) 
        {
            DeactivateNextImageInstantly();
        }
    }

    void DeactivateNextImageInstantly()
    {
        if (imageIndex < imagesToDeactivate.Length && imagesToDeactivate[imageIndex] != null)
        {
            imagesToDeactivate[imageIndex].SetActive(false);
            imageIndex++;

            if (imageIndex >= imagesToDeactivate.Length)
            {
                EndGame();
            }
        }
    }

    void EndGame()
    {
        if (messageText != null)
        {
            messageText.text = "Game Over! All images disappeared.";
        }
        Time.timeScale = 0f; // Freeze the game
    }
}





using UnityEngine;
using System.Collections;

public class EnemyChase : MonoBehaviour
{
    public GameObject enemy; // The main enemy that chases
    public string enemyTag = "Enemy";  // Tag for additional enemies
    public float speed = 5f;
    public float catchDistance = 3f;
    
    public GameObject[] imagesToDeactivate;

    private bool isChasing = false;
    private float timer = 0f;
    private int imageIndex = 0;
    private bool isCoroutineRunning = false;

    void Start()
    {
        if (enemy == null)
        {
            Debug.LogError("Enemy not assigned! Please assign an enemy GameObject in the inspector.");
            return;
        }

        if (imagesToDeactivate != null)
        {
            foreach (GameObject image in imagesToDeactivate)
                if (image != null) image.SetActive(true);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 15f) isChasing = true;

        if (isChasing)
        {
            ChaseTarget();
        }

        CheckProximityToEnemies();
    }

    void ChaseTarget()
    {
        if (enemy == null) return;

        Transform cameraTransform = Camera.main.transform;
        Vector3 direction = cameraTransform.position - enemy.transform.position;
        direction.y = 0;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, Time.deltaTime * speed);
        }

        enemy.transform.position += direction.normalized * speed * Time.deltaTime;

        if ((enemy.transform.position - cameraTransform.position).sqrMagnitude <= catchDistance * catchDistance)
        {
            if (!isCoroutineRunning && imageIndex < imagesToDeactivate.Length)
            {
                StartCoroutine(DeactivateImageWithDelay());
            }
        }
        else
        {
            StopAllCoroutines();
            isCoroutineRunning = false;
        }
    }

    void CheckProximityToEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject otherEnemy in enemies)
        {
            if (otherEnemy != enemy)
            {
                if ((otherEnemy.transform.position - Camera.main.transform.position).sqrMagnitude <= catchDistance * catchDistance)
                {
                    if (!isCoroutineRunning && imageIndex < imagesToDeactivate.Length)
                    {
                        StartCoroutine(DeactivateImageWithDelay());
                    }
                    return;
                }
            }
        }
    }

    IEnumerator DeactivateImageWithDelay()
    {
        isCoroutineRunning = true;
        while (imageIndex < imagesToDeactivate.Length)
        {
            if (!IsAnyEnemyTooClose())
            {
                isCoroutineRunning = false;
                yield break;
            }

            if (imagesToDeactivate[imageIndex] != null)
                imagesToDeactivate[imageIndex].SetActive(false);

            imageIndex++;
            
            if (imageIndex >= imagesToDeactivate.Length)
            {
                EndGame();
                yield break;
            }
            
            yield return new WaitForSeconds(3f);
        }

        isCoroutineRunning = false;
    }

    bool IsAnyEnemyTooClose()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject otherEnemy in enemies)
        {
            if ((otherEnemy.transform.position - Camera.main.transform.position).sqrMagnitude <= catchDistance * catchDistance)
            {
                return true;
            }
        }
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obs")) 
        {
            DeactivateNextImageInstantly();
        }
    }

    void DeactivateNextImageInstantly()
    {
        if (imageIndex < imagesToDeactivate.Length && imagesToDeactivate[imageIndex] != null)
        {
            imagesToDeactivate[imageIndex].SetActive(false);
            imageIndex++;

            if (imageIndex >= imagesToDeactivate.Length)
            {
                EndGame();
            }
        }
    }

    void EndGame()
    {
        Time.timeScale = 0f; // Freeze the game
    }
}



using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required for Text UI

public class EnemyChase : MonoBehaviour
{
    public GameObject enemy; // The main enemy that chases
    public string enemyTag = "Enemy";  // Tag for additional enemies
    public float speed = 5f;
    public float catchDistance = 3f;

    public GameObject[] imagesToDeactivate;
    public Text gameOverText; // ✅ Add this in the inspector

    private bool isChasing = false;
    private float timer = 0f;
    private int imageIndex = 0;
    private bool isCoroutineRunning = false;
    private Coroutine activeCoroutine = null;
    private Transform cameraTransform;

    void Start()
    {
        if (enemy == null)
        {
            Debug.LogError("Enemy not assigned! Please assign an enemy GameObject in the inspector.");
            return;
        }

        cameraTransform = Camera.main.transform;

        if (imagesToDeactivate != null)
        {
            foreach (GameObject image in imagesToDeactivate)
                if (image != null) image.SetActive(true);
        }

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false); // Hide at start
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 15f) isChasing = true;

        if (isChasing)
        {
            ChaseTarget();
        }

        CheckProximityToEnemies();
    }

    void ChaseTarget()
    {
        if (enemy == null) return;

        Vector3 direction = cameraTransform.position - enemy.transform.position;
        direction.y = 0;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, Time.deltaTime * speed);
        }

        enemy.transform.position += direction.normalized * speed * Time.deltaTime;

        if ((enemy.transform.position - cameraTransform.position).sqrMagnitude <= catchDistance * catchDistance)
        {
            TryStartImageDeactivation();
        }
        else
        {
            if (activeCoroutine != null)
            {
                StopCoroutine(activeCoroutine);
                isCoroutineRunning = false;
            }
        }
    }

    void CheckProximityToEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject otherEnemy in enemies)
        {
            if (otherEnemy != enemy)
            {
                if ((otherEnemy.transform.position - cameraTransform.position).sqrMagnitude <= catchDistance * catchDistance)
                {
                    TryStartImageDeactivation();
                    return;
                }
            }
        }
    }

    void TryStartImageDeactivation()
    {
        if (!isCoroutineRunning && imageIndex < imagesToDeactivate.Length)
        {
            activeCoroutine = StartCoroutine(DeactivateImageWithDelay());
        }
    }

    IEnumerator DeactivateImageWithDelay()
    {
        isCoroutineRunning = true;
        while (imageIndex < imagesToDeactivate.Length)
        {
            if (!IsAnyEnemyTooClose())
            {
                isCoroutineRunning = false;
                yield break;
            }

            if (imagesToDeactivate[imageIndex] != null)
                imagesToDeactivate[imageIndex].SetActive(false);

            imageIndex++;

            if (imageIndex >= imagesToDeactivate.Length)
            {
                EndGame();
                yield break;
            }

            yield return new WaitForSeconds(3f);
        }

        isCoroutineRunning = false;
    }

    bool IsAnyEnemyTooClose()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject otherEnemy in enemies)
        {
            if ((otherEnemy.transform.position - cameraTransform.position).sqrMagnitude <= catchDistance * catchDistance)
            {
                return true;
            }
        }
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obs")) 
        {
            DeactivateNextImageInstantly();
        }
    }

    void DeactivateNextImageInstantly()
    {
        if (imageIndex < imagesToDeactivate.Length && imagesToDeactivate[imageIndex] != null)
        {
            imagesToDeactivate[imageIndex].SetActive(false);
            imageIndex++;

            if (imageIndex >= imagesToDeactivate.Length)
            {
                EndGame();
            }
        }
    }

    void EndGame()
    {
        Time.timeScale = 0f;
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true); // ✅ Show Game Over
            gameOverText.text = "Game Over!";
        }
    }
}
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyChase : MonoBehaviour
{
    public GameObject enemy;
    public string enemyTag = "Enemy";
    public float speed = 5f;
    public float catchDistance = 3f;

    public GameObject[] imagesToDeactivate;
    public Text gameOverText;             // UI Text to show Game Over
    public Text survivalTimeText;         // ✅ UI Text to show survival time

    private bool isChasing = false;
    private float timer = 0f;
    private float totalSurvivalTime = 0f; // ✅ Track total time survived
    private int imageIndex = 0;
    private bool isCoroutineRunning = false;
    private Coroutine activeCoroutine = null;
    private Transform cameraTransform;

    void Start()
    {
        if (enemy == null)
        {
            Debug.LogError("Enemy not assigned!");
            return;
        }

        cameraTransform = Camera.main.transform;

        foreach (GameObject image in imagesToDeactivate)
            if (image != null) image.SetActive(true);

        if (gameOverText != null)
            gameOverText.gameObject.SetActive(false);

        if (survivalTimeText != null)
            survivalTimeText.gameObject.SetActive(false);
    }

    void Update()
    {
        totalSurvivalTime += Time.deltaTime; // ✅ Increment survival time

        timer += Time.deltaTime;
        if (timer >= 9f) isChasing = true;

        if (isChasing) ChaseTarget();

        CheckProximityToEnemies();
    }

    void ChaseTarget()
    {
        if (enemy == null) return;

        Vector3 direction = cameraTransform.position - enemy.transform.position;
        direction.y = 0;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, Time.deltaTime * speed);
        }

        enemy.transform.position += direction.normalized * speed * Time.deltaTime;

        if ((enemy.transform.position - cameraTransform.position).sqrMagnitude <= catchDistance * catchDistance)
        {
            TryStartImageDeactivation();
        }
        else if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
            isCoroutineRunning = false;
        }
    }

    void CheckProximityToEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject otherEnemy in enemies)
        {
            if (otherEnemy != enemy)
            {
                if ((otherEnemy.transform.position - cameraTransform.position).sqrMagnitude <= catchDistance * catchDistance)
                {
                    TryStartImageDeactivation();
                    return;
                }
            }
        }
    }

    void TryStartImageDeactivation()
    {
        if (!isCoroutineRunning && imageIndex < imagesToDeactivate.Length)
        {
            activeCoroutine = StartCoroutine(DeactivateImageWithDelay());
        }
    }

    IEnumerator DeactivateImageWithDelay()
    {
        isCoroutineRunning = true;
        while (imageIndex < imagesToDeactivate.Length)
        {
            if (!IsAnyEnemyTooClose())
            {
                isCoroutineRunning = false;
                yield break;
            }

            if (imagesToDeactivate[imageIndex] != null)
                imagesToDeactivate[imageIndex].SetActive(false);

            imageIndex++;

            if (imageIndex >= imagesToDeactivate.Length)
            {
                EndGame();
                yield break;
            }

            yield return new WaitForSeconds(3f);
        }

        isCoroutineRunning = false;
    }

    bool IsAnyEnemyTooClose()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject otherEnemy in enemies)
        {
            if ((otherEnemy.transform.position - cameraTransform.position).sqrMagnitude <= catchDistance * catchDistance)
            {
                return true;
            }
        }
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obs")) 
        {
            DeactivateNextImageInstantly();
        }
    }

    void DeactivateNextImageInstantly()
    {
        if (imageIndex < imagesToDeactivate.Length && imagesToDeactivate[imageIndex] != null)
        {
            imagesToDeactivate[imageIndex].SetActive(false);
            imageIndex++;

            if (imageIndex >= imagesToDeactivate.Length)
            {
                EndGame();
            }
        }
    }

    void EndGame()
    {
        Time.timeScale = 0f;

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "Game Over!";
        }

        if (survivalTimeText != null)
        {
            survivalTimeText.gameObject.SetActive(true);
            survivalTimeText.text = $"You survived for {totalSurvivalTime:F1} seconds!";
        }
    }
}
