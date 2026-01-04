using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Experimental.Rendering;

public class CameraStreamer : MonoBehaviour
{
    public ARCameraManager cameraManager;
    public YOLOClient yoloClient;


    void OnEnable()
    {
        cameraManager.frameReceived += OnFrameReceived;
    }

    void OnDisable()
    {
        cameraManager.framxeReceived -= OnFrameReceived;
    }

    void OnFrameReceived(ARCameraFrameEventArgs args)
    {
        Debug.Log("📷 Camera frame received");

        if (!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
            return;

        var conversionParams = new XRCpuImage.ConversionParams
        {
            inputRect = new RectInt(0, 0, image.width, image.height),
            outputDimensions = new Vector2Int(image.width, image.height),
            outputFormat = TextureFormat.RGB24,
            transformation = XRCpuImage.Transformation.None
        };

        Texture2D tex = new Texture2D(
            image.width,
            image.height,
            GraphicsFormat.R8G8B8_UNorm,
            TextureCreationFlags.None
        );

        image.Convert(
            conversionParams,
            tex.GetRawTextureData<byte>()
        );

        tex.Apply();
        image.Dispose();

        yoloClient.SendFrame(tex);
    }
}