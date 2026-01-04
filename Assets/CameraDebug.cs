using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CameraDebug : MonoBehaviour
{
    public ARCameraManager cam;

    void OnEnable()
    {
        cam.frameReceived += args =>
        {
            Debug.Log("Camera frame received");
        };
    }
}