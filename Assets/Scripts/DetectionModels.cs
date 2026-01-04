using System;
using System.Collections.Generic;

[Serializable]
public class DetectionResponse
{
    public List<Detection> detections;
}

[Serializable]
public class Detection
{
    public float x1;
    public float y1;
    public float x2;
    public float y2;
    public int @class;
    public float confidence;
}