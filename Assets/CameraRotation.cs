using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CameraRotation : MonoBehaviour
{
  public Transform target; // Reference to the target object.
    public float rotationSpeed = 1.0f; // Rotation speed.
    public float zoomSpeed = 2.0f; // Zoom speed.
    public float minDistance = 3.0f; // Minimum zoom distance.
    public float maxDistance = 15.0f; // Maximum zoom distance.
    public float initialYPosition = 0.0f; // Initial Y position of the camera.

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    public float distance;

    void Start()
    {
        /*distance = Vector3.Distance(transform.position, target.position);
        Vector3 initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - distance);
        transform.position = initialPosition;*/
    }
    
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Mouse is over a UI element, don't process camera input.
            return;
        }

        // Handle touch/mouse input for camera rotation.
        if (Input.GetMouseButton(0))
        {
            xRotation += Input.GetAxis("Mouse X") * rotationSpeed;
            yRotation -= Input.GetAxis("Mouse Y") * rotationSpeed;
        }

        // Clamp the yRotation to limit camera movement.
        yRotation = Mathf.Clamp(yRotation, -90.0f, 90.0f);

        // Handle zoom using the scroll wheel or finger touch.
        float zoomInput = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        // Check for touch input for zoom (for mobile devices).
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            float prevMagnitude = (touch0PrevPos - touch1PrevPos).magnitude;
            float currentMagnitude = (touch0.position - touch1.position).magnitude;

            zoomInput = (currentMagnitude - prevMagnitude) * zoomSpeed * 0.01f;
        }

        distance = Mathf.Clamp(distance - zoomInput, minDistance, maxDistance);

        // Calculate the new camera position and rotation.
        Quaternion rotation = Quaternion.Euler(yRotation, xRotation, 0);
        Vector3 position = rotation * new Vector3(0, initialYPosition, -distance) + target.position;

        // Apply the new position and rotation to the camera.
        transform.rotation = rotation;
        transform.position = position;
    }

    
}
