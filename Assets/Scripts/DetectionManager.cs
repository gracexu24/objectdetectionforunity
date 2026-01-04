using System.Collections.Generic;
using UnityEngine;

public class DetectionManager : MonoBehaviour
{
    public BoundingBoxSpawner spawner;
    public float confidenceThreshold = 0.5f;

    public void ProcessDetections(List<Detection> detections)
    {
        List<Detection> filtered = new List<Detection>();

        foreach (var d in detections)
        {
            if (d.confidence >= confidenceThreshold)
                filtered.Add(d);
        }

        spawner.SpawnDetections(filtered);
    }
}