using System.Collections.Generic;
using UnityEngine;

public class BoundingBoxSpawner : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject boundingBoxPrefab;

    public void SpawnDetections(List<Detection> detections)
    {
        foreach (var d in detections)
        {
            float cx = (d.x1 + d.x2) / 2f;
            float cy = (d.y1 + d.y2) / 2f;

            Vector3 screenPos = new Vector3(cx, Screen.height - cy, 0);
            Ray ray = mainCamera.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out RaycastHit hit, 10f))
            {
                Instantiate(
                    boundingBoxPrefab,
                    hit.point,
                    Quaternion.identity
                );
            }
        }
    }
}