using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    private float targetAspect = 9f/16f; // Desired aspect ratio (16:9)
    void Start()
    {
        // Calculate the current window's aspect ratio
        float windowAspect = (float)Screen.width / (float)Screen.height;

        // Calculate the scale height to maintain the target aspect ratio
        float scaleHeight = windowAspect / targetAspect;

        
        Camera cam = GetComponent<Camera>();

        if (scaleHeight < 1.0f)
        {
            cam.orthographicSize = cam.orthographicSize / scaleHeight;
        }

    }

}
