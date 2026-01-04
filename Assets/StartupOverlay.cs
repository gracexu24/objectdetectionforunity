using UnityEngine;

public class StartupOverlay : MonoBehaviour
{
    void Awake()  { Debug.Log("AWAKE: StartupOverlay"); }
    void Start()  { Debug.Log("START: StartupOverlay"); }

    void OnGUI()
    {
        GUI.Label(new Rect(30, 30, 1200, 80),
            "✅ Unity scene is running (StartupOverlay OnGUI)");
    }
}