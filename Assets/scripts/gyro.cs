/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
public class GyroControl: MonoBehaviour
{
private bool gyroEnabled;
private Gyroscope gyro;
private GameObject CameraContainer;
private Quaternion rot;

private void Start(){
    CameraContainer=new GameObject("Camera Container");
    CameraContainer.transform.position=transform.position; 
    transform.SetParent(CameraContainer.transform);
    gyroEnabled=EnableGyro();
}
private bool EnableGyro(){
 if(SystemInfo.supportsGyroscope){
 gyro=Input.gyro;
 gyro.enabled=true;
 CameraContainer.transform.rotation=Quaternion.Euler(90f,90f,0f);
 rot=new Quaternion(0,0,1,0);
return true;
 }
 return false;
}
private void Update(){
if(gyroEnabled){
    transform.localRotation=gyro.attitude*rot;
}
} 
}




using UnityEngine;
using System.Collections;

public class GyroControl : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private GameObject CameraContainer;
    private Quaternion rot;

    private void Start()
    {
        CameraContainer = new GameObject("Camera Container");
        CameraContainer.transform.position = transform.position;
        CameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
        
        transform.SetParent(CameraContainer.transform);
        
        if (Camera.main != null)
        {
            Camera.main.transform.SetParent(CameraContainer.transform);
        }

        StartCoroutine(EnableGyroWithDelay());
    }

    private IEnumerator EnableGyroWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // Small delay before enabling gyro
        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            rot = new Quaternion(0, 0, 1, 0);
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            transform.localRotation = new Quaternion(gyro.attitude.x, gyro.attitude.y, -gyro.attitude.z, -gyro.attitude.w) * rot;
        }
    }
}


using UnityEngine;
using System.Collections;

public class GyroControl : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private GameObject cameraContainer;
    private Quaternion rot;

    public Rigidbody playerRb; // Reference to player's Rigidbody
    public Transform parentObject; // Parent object to move when looking up
    public float moveSpeed = 5f; // Movement speed (used instead of move force for kinematic Rigidbody)
    public float upwardForce = 3f; // Force applied when looking up

    private void Start()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);

        transform.SetParent(cameraContainer.transform);

        if (Camera.main != null)
        {
            Camera.main.transform.SetParent(cameraContainer.transform);
        }

        StartCoroutine(EnableGyroWithDelay());
    }

    private IEnumerator EnableGyroWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // Small delay before enabling gyro
        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            rot = new Quaternion(0, 0, 1, 0);
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            transform.localRotation = new Quaternion(gyro.attitude.x, gyro.attitude.y, -gyro.attitude.z, -gyro.attitude.w) * rot;
            MovePlayer(); // Apply movement based on gyroscope direction
            MoveParentObject(); // Move parent when looking up
        }
    }

    private void MovePlayer()
    {
        if (playerRb == null) return; // Ensure Rigidbody is assigned

        Vector3 forward = transform.forward;
        float tiltX = forward.x; // Detect left/right tilt
        float tiltZ = forward.z; // Detect forward movement
        float tiltY = forward.y; // Detect upward/downward tilt

        // Debugging the tilt values
        Debug.Log($"TiltX: {tiltX}, TiltY: {tiltY}, TiltZ: {tiltZ}");

        // Movement logic
        if (tiltY < -0.5f) // If looking down, stop movement
        {
            // Stop movement when looking down, no need to set velocity for kinematic Rigidbody
        }
        else
        {
            // Directly move the player (using transform for kinematic Rigidbody)
            Vector3 movement = new Vector3(tiltX, 0, tiltZ) * moveSpeed * Time.deltaTime;

            // Apply movement by directly modifying position
            playerRb.MovePosition(playerRb.position + movement);
        }
    }

    private void MoveParentObject()
    {
        if (parentObject == null) return; // Ensure the parent object is assigned

        Vector3 forward = transform.forward;
        float tiltY = forward.y; // Detect upward movement

        if (tiltY > 0.5f) // If looking up, move the parent object upwards
        {
            parentObject.position += Vector3.up * upwardForce * Time.deltaTime;
        }
    }
}


using UnityEngine;
using System.Collections;

public class GyroControl : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private GameObject cameraContainer;
    private Quaternion rot;

    public Transform player; // Reference to player transform
    public Transform parentObject; // Parent object to move when looking up
    public float moveSpeed = 0.1f; // Speed of movement
    public float upwardSpeed = 0.05f; // Speed when looking up

    private void Start()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);

        transform.SetParent(cameraContainer.transform);

        if (Camera.main != null)
        {
            Camera.main.transform.SetParent(cameraContainer.transform);
        }

        StartCoroutine(EnableGyroWithDelay());
    }

    private IEnumerator EnableGyroWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // Small delay before enabling gyro
        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            rot = new Quaternion(0, 0, 1, 0);
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            // Apply rotation using gyroscope readings
            transform.localRotation = new Quaternion(gyro.attitude.x, gyro.attitude.y, -gyro.attitude.z, -gyro.attitude.w) * rot;

            // Apply movement using the same logic
            MovePlayer();
            MoveParentObject();
        }
    }

    private void MovePlayer()
    {
        if (player == null) return;

        Vector3 forward = transform.forward;
        float tiltX = forward.x; // Detect left/right tilt
        float tiltZ = forward.z; // Detect forward/backward tilt
        float tiltY = forward.y; // Detect up/down tilt

        // Move player based on tilt values
        if (tiltY < -0.5f) // If looking down, stop movement
        {
            return;
        }
        else
        {
            // Move along X and Z axis based on gyroscope tilt
            player.position += new Vector3(tiltX, 0, tiltZ) * moveSpeed;
        }
    }

  private void MoveParentObject()
{
    if (parentObject == null) return;

    float tiltY = transform.forward.y;

    if (tiltY > 0.5f) // If looking up
    {
        Vector3 targetPosition = parentObject.position + Vector3.up * upwardSpeed;
        parentObject.position = Vector3.Lerp(parentObject.position, targetPosition, Time.deltaTime * 5f);
    }
}

}


using UnityEngine;
using System.Collections;

public class GyroControl : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private GameObject cameraContainer;
    private Quaternion rot;

    public Transform player; // Reference to player transform
    public Transform parentObject; // Parent object to move when looking up
    public float moveSpeed = 0.1f; // Speed of movement
    public float upwardSpeed = 0.05f; // Speed when looking up

    private void Start()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;

        // ✅ Flip the scene upside down at start
        cameraContainer.transform.rotation = Quaternion.Euler(270f, 0f, 360f);

        transform.SetParent(cameraContainer.transform);

        if (Camera.main != null)
        {
            Camera.main.transform.SetParent(cameraContainer.transform);
        }

        StartCoroutine(EnableGyroWithDelay());
    }

    private IEnumerator EnableGyroWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // Small delay before enabling gyro
        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Debug.Log("✅ Gyroscope is supported!");
            gyro = Input.gyro;
            gyro.enabled = true;
            rot = new Quaternion(0, 0, 1, 0);
            return true;
        }
        Debug.LogError("❌ Gyroscope NOT supported on this device!");
        return false;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            Debug.Log($"📍 Gyro Rotation: {gyro.attitude}");
            
            // ✅ Reverse x & y to fix upside-down issue
            transform.localRotation = new Quaternion(-gyro.attitude.x, -gyro.attitude.y, gyro.attitude.z, gyro.attitude.w) * rot;

            MovePlayer();
            MoveParentObject();
        }
    }

    private void MovePlayer()
    {
        if (player == null) return;

        Vector3 forward = transform.forward;
        float tiltX = forward.x; // ✅ Reverse left/right movement
        float tiltZ = forward.z; // ✅ Reverse forward/backward movement
        float tiltY = forward.y;  // Keep upward detection same

        Debug.Log($"🔄 Reversed TiltX: {tiltX}, TiltZ: {tiltZ}, TiltY: {tiltY}");

        if (tiltY > 0.5f) // ✅ Fix stopping logic for looking down
        {
            Debug.Log("⏸ Looking down – stopping movement");
            return;
        }
        else
        {
            player.position += new Vector3(tiltX, 0, tiltZ) * moveSpeed;
            Debug.Log($"🚶 Moving Player (Reversed): {player.position}");
        }
    }

    private void MoveParentObject()
    {
        if (parentObject == null) return;

        float tiltY = -transform.forward.y; // ✅ Reverse up/down detection
        Debug.Log($"⬆ Checking reversed upward tilt: {tiltY}");

        if (tiltY > 0.5f) // ✅ Corrected condition for looking up
        {
            Debug.Log("🌲 Looking up – Moving Parent Object (Reversed)");
            Vector3 targetPosition = parentObject.position + Vector3.up * upwardSpeed;
            parentObject.position = Vector3.Lerp(parentObject.position, targetPosition, Time.deltaTime * 5f);
        }
    }
}




using UnityEngine;
using System.Collections;

public class GyroControl : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private GameObject cameraContainer;
    private Quaternion rot;

    public Transform player; // Reference to player transform
    public Transform parentObject; // Parent object to move when looking up
    public float moveSpeed = 0.1f; // Speed of movement
    public float upwardSpeed = 0.05f; // Speed when looking up

    private void Start()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;

        // ✅ Scene already correctly flipped
        cameraContainer.transform.rotation = Quaternion.Euler(270f, 0f, 360f);

        transform.SetParent(cameraContainer.transform);

        if (Camera.main != null)
        {
            Camera.main.transform.SetParent(cameraContainer.transform);
        }

        StartCoroutine(EnableGyroWithDelay());
    }

    private IEnumerator EnableGyroWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // Small delay before enabling gyro
        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Debug.Log("✅ Gyroscope is supported!");
            gyro = Input.gyro;
            gyro.enabled = true;
            rot = new Quaternion(0, 0, 1, 0);
            return true;
        }
        Debug.LogError("❌ Gyroscope NOT supported on this device!");
        return false;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            Debug.Log($"📍 Gyro Rotation: {gyro.attitude}");
            
            // ✅ Corrected inversion issue when turning phone left/right
            transform.localRotation = new Quaternion(gyro.attitude.x, gyro.attitude.y, -gyro.attitude.z, -gyro.attitude.w) * rot;

            MovePlayer();
            MoveParentObject();
        }
    }

    private void MovePlayer()
    {
        if (player == null) return;

        Vector3 forward = transform.forward;
        float tiltX = forward.x;  // ✅ Now correctly moves left when tilting left
        float tiltZ = forward.z;  // ✅ Now correctly moves forward/backward
        float tiltY = forward.y;  // Keep upward detection same

        Debug.Log($"🔄 Fixed TiltX: {tiltX}, TiltZ: {tiltZ}, TiltY: {tiltY}");

        if (tiltY > 0.5f) // ✅ Fix stopping logic for looking down
        {
            Debug.Log("⏸ Looking down – stopping movement");
            return;
        }
        else
        {
            player.position += new Vector3(tiltX, 0, tiltZ) * moveSpeed;
            Debug.Log($"🚶 Moving Player: {player.position}");
        }
    }

    private void MoveParentObject()
    {
        if (parentObject == null) return;

        float tiltY = -transform.forward.y; // ✅ Reverse up/down detection
        Debug.Log($"⬆ Checking reversed upward tilt: {tiltY}");

        if (tiltY > 0.5f) // ✅ Corrected condition for looking up
        {
            Debug.Log("🌲 Looking up – Moving Parent Object");
            Vector3 targetPosition = parentObject.position + Vector3.up * upwardSpeed;
            parentObject.position = Vector3.Lerp(parentObject.position, targetPosition, Time.deltaTime * 5f);
        }
    }
}




using UnityEngine;
using System.Collections;

public class GyroControl : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private GameObject cameraContainer;
    private Quaternion rot;

    [Header("Movement Settings")]
    public Transform targetObject; // Object to move based on gyro
    public float moveSpeed = 3f;  // Increased Speed
    public float upwardSpeed = 1f; // Increased Upward Speed

    private void Start()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        cameraContainer.transform.rotation = Quaternion.Euler(270f, 0f, 360f);

        transform.SetParent(cameraContainer.transform);

        if (Camera.main != null)
        {
            Camera.main.transform.SetParent(cameraContainer.transform);
        }

        StartCoroutine(EnableGyroWithDelay());
    }

    private IEnumerator EnableGyroWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Debug.Log("✅ Gyroscope is supported!");
            gyro = Input.gyro;
            gyro.enabled = true;
            rot = new Quaternion(0, 0, 1, 0);
            return true;
        }
        Debug.LogError("❌ Gyroscope NOT supported on this device!");
        return false;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            Debug.Log($"📍 Gyro Rotation: {gyro.attitude}");
            transform.localRotation = new Quaternion(gyro.attitude.x, gyro.attitude.y, -gyro.attitude.z, -gyro.attitude.w) * rot;

            MoveTargetObject();
        }
    }

    private void MoveTargetObject()
    {
        if (targetObject == null) return;

        Vector3 forward = transform.forward;
        float tiltX = forward.x;
        float tiltZ = forward.z;
        float tiltY = forward.y;

        Debug.Log($"🔄 TiltX: {tiltX}, TiltZ: {tiltZ}, TiltY: {tiltY}");

        // ✅ Apply movement directly using Time.deltaTime for smoothness
        Vector3 movement = new Vector3(tiltX, 0, tiltZ) * moveSpeed * Time.deltaTime;

        // Move only if tilt is significant
        if (movement.magnitude > 0.01f)  
        {
            targetObject.position += movement;
            Debug.Log($"🚶 Moving Target Object: {targetObject.position}");
        }

        // Move upward when looking up
        if (-tiltY > 0.3f) 
        {
            Debug.Log("🌲 Looking up – Moving Target Object Upward");
            targetObject.position += Vector3.up * upwardSpeed * Time.deltaTime;
        }
    }
}






using UnityEngine;
using System.Collections;

namespace StarterAssets
{
    public class GyroscopeMovement : MonoBehaviour
    {
        [Header("Gyroscope Settings")]
        [Tooltip("Sensitivity of the gyroscope movement")]
        public float MoveSpeed = 4.0f;
        public float RotationSpeed = 1.0f;
        public float UpwardSpeed = 1.0f;

        private CharacterController _controller;
        private bool _gyroEnabled;
        private Gyroscope _gyroscope;
        private Quaternion _rot;
        private GameObject _cameraContainer;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _cameraContainer = new GameObject("Camera Container");
            _cameraContainer.transform.position = transform.position;
            _cameraContainer.transform.rotation = Quaternion.Euler(270f, 0f, 360f);
            transform.SetParent(_cameraContainer.transform);
            if (Camera.main != null)
            {
                Camera.main.transform.SetParent(_cameraContainer.transform);
            }
            StartCoroutine(EnableGyroWithDelay());
        }

        private IEnumerator EnableGyroWithDelay()
        {
            yield return new WaitForSeconds(0.5f);
            _gyroEnabled = EnableGyroscope();
        }

        private bool EnableGyroscope()
        {
            if (SystemInfo.supportsGyroscope)
            {
                _gyroscope = Input.gyro;
                _gyroscope.enabled = true;
                _rot = new Quaternion(0, 0, 1, 0);
                return true;
            }
            return false;
        }

        private void Update()
        {
            if (_gyroEnabled)
            {
                transform.localRotation = new Quaternion(
                    _gyroscope.attitude.x,
                    _gyroscope.attitude.y, // Reversed left-right direction
                    -_gyroscope.attitude.z,
                    -_gyroscope.attitude.w) * _rot;
                Move();
            }
        }

        private void Move()
        {
            Vector3 forward = transform.forward;
            float tiltX = -forward.x; // Reversed left-right direction
            float tiltZ = forward.z;
            float tiltY = forward.y;

            Vector3 movement = new Vector3(tiltX, 0, tiltZ) * MoveSpeed * Time.deltaTime;

            if (movement.magnitude > 0.01f)
            {
                _controller.Move(movement);
            }

            if (-tiltY > 0.3f)
            {
                _controller.Move(Vector3.up * UpwardSpeed * Time.deltaTime);
            }
        }
    }
}

*/

using UnityEngine;
using System.Collections;

namespace StarterAssets
{
    public class GyroscopeMovement : MonoBehaviour
    {
        [Header("Gyroscope Settings")]
        [Tooltip("Sensitivity of the gyroscope movement")]
        public float MoveSpeed = 4.0f;
        public float RotationSpeed = 1.0f;
        public float UpwardSpeed = 1.0f;

        private CharacterController _controller;
        private bool _gyroEnabled;
        private Gyroscope _gyroscope;
        private Quaternion _rot;
        private GameObject _cameraContainer;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _cameraContainer = new GameObject("Camera Container");
            _cameraContainer.transform.position = transform.position;
            _cameraContainer.transform.rotation = Quaternion.Euler(270f, 0f, 360f);
            transform.SetParent(_cameraContainer.transform);
            if (Camera.main != null)
            {
                Camera.main.transform.SetParent(_cameraContainer.transform);
            }
            StartCoroutine(EnableGyroWithDelay());
        }

        private IEnumerator EnableGyroWithDelay()
        {
            yield return new WaitForSeconds(0.5f);
            _gyroEnabled = EnableGyroscope();
        }

        private bool EnableGyroscope()
        {
            if (SystemInfo.supportsGyroscope)
            {
                _gyroscope = Input.gyro;
                _gyroscope.enabled = true;
                _rot = new Quaternion(0, 0, 1, 0);
                return true;
            }
            return false;
        }

        private void Update()
        {
            if (_gyroEnabled)
            {
                transform.localRotation = new Quaternion(
                    _gyroscope.attitude.x,
                    _gyroscope.attitude.y,  
                    -_gyroscope.attitude.z,
                    -_gyroscope.attitude.w) * _rot;  // No orientation changes!

                Move();
            }
        }

        private void Move()
        {
            Vector3 forward = transform.forward;
            float tiltX = forward.x;   // Moving right when looking right
            float tiltZ = forward.z;   // Moving forward when looking forward
            float tiltY = forward.y;   // Moving up when looking up

            Vector3 movement = new Vector3(tiltX, 0, tiltZ) * MoveSpeed * Time.deltaTime;

            if (movement.magnitude > 0.01f)
            {
                _controller.Move(movement);
            }

            if (tiltZ < -0.3f)
            {
                _controller.Move(Vector3.up * UpwardSpeed * Time.deltaTime);
            }
            else if (tiltZ > 0.3f)
            {
                _controller.Move(Vector3.down * UpwardSpeed * Time.deltaTime);
            }
        }
    }
}




