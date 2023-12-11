using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public float startRotationSpeed = 90.0f; // Starting speed
    public float minRotationSpeed = 5.0f;   // Minimum speed
    public float deceleration = 35.0f;       // Deceleration rate

    void Start()
    {
        StartCoroutine(RotateObjectWithDeceleration());
    }

    IEnumerator RotateObjectWithDeceleration()
    {
        float rotationSpeed = startRotationSpeed;

        while (rotationSpeed > minRotationSpeed)
        {
            // Rotate the object around the Z-axis (backward)
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);

            // Gradually decrease the rotation speed
            rotationSpeed -= deceleration * Time.deltaTime;

            yield return null; // Yielding null in a Coroutine makes it wait for the next frame.
        }

        // Ensure the object stops rotating
        // transform.rotation = Quaternion.identity;
    }



    // float rotSpeed = 100;
    // void OnMouseDrag()
    // {
    //     float rotX = Input.GetAxis("Mouse X")*rotSpeed*Mathf.Deg2Rad;
    //     float rotY = Input.GetAxis("Mouse Y")*rotSpeed*Mathf.Deg2Rad;

    //     transform.Rotate(Vector3.forward, -rotX);
    //     transform.Rotate(Vector3.down, rotY);
    // }

    private Vector3 mouseStart;
    private Vector3 rotationStart;
    private bool isRotating = false;

    void OnMouseDown()
    {
        // Capture the starting mouse position and the current rotation of the object.
        mouseStart = Input.mousePosition;
        rotationStart = transform.rotation.eulerAngles;
        isRotating = true;
    }

    void OnMouseDrag()
    {
        if (isRotating)
        {
            // Calculate the rotation based on the mouse movement.
            Vector3 mouseDelta = Input.mousePosition - mouseStart;
            Vector3 rotationDelta = new Vector3(mouseDelta.y, -mouseDelta.x, 0);

            // Apply the rotation to the object.
            transform.rotation = Quaternion.Euler(rotationStart + rotationDelta);
        }
    }

    void OnMouseUp()
    {
        // Stop rotating when the mouse button is released.
        isRotating = false;
    }
}
