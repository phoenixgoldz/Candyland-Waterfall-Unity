using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class RotateOnVisible : MonoBehaviour
{
    public Image ConfirmationImage; // Reference to the Confirmation_Image
    public float rotationSpeed = 100f; // Rotation speed in degrees per second

    void Update()
    {
        // Check if the Confirmation_Image is active and visible
        if (ConfirmationImage != null && ConfirmationImage.gameObject.activeInHierarchy)
        {
            // Rotate the object around the Z-axis
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }
}
