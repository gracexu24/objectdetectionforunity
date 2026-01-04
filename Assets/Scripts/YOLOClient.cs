using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System;

public class YOLOClient : MonoBehaviour
{
    public string serverUrl;
    public DetectionManager detectionManager;

    public void SendFrame(Texture2D tex)
    {
        byte[] jpg = tex.EncodeToJPG(60);
        string base64 = Convert.ToBase64String(jpg);

        Payload payload = new Payload
        {
            image = base64,
            width = tex.width,
            height = tex.height
        };

        string json = JsonUtility.ToJson(payload);
        StartCoroutine(Post(json));
    }

    IEnumerator Post(string json)
    {
        UnityWebRequest req = new UnityWebRequest(serverUrl, "POST");
        req.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            DetectionResponse response =
                JsonUtility.FromJson<DetectionResponse>(req.downloadHandler.text);

            detectionManager.ProcessDetections(response.detections);
        }
        else
        {
            Debug.LogError(req.error);
        }
    }

    [Serializable]
    class Payload
    {
        public string image;
        public int width;
        public int height;
    }
}
