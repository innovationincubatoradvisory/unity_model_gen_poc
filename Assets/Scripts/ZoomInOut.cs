using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float zoomSpeed = 5.0f; // Adjust the movement speed as needed
    // public float minZoomDistance = -10.0f; // Minimum Z position
    // public float maxZoomDistance = -8.0f;  // Maximum Z position


    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Calculate the new position based on the scroll input
        Vector3 newZPosition = transform.position + transform.forward * scroll * zoomSpeed;
        if (newZPosition.z >= -10.0f && newZPosition.z <= -8.4f)
        {
            // Update the camera's position
            transform.position = newZPosition;
        }
    }
}