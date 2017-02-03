using UnityEngine;
using System.Collections;

public class FrameCounter : MonoBehaviour {

    public float frameRate;
    public int maxFrameRate = 60;
    public GUIStyle guiStyle;

    private void Start()
    {
        Application.targetFrameRate = maxFrameRate;
    }

    void Update () {

        frameRate = Time.frameCount / Time.time;
	}

    private void OnGUI()
    {
        GUILayout.Label("Framerate: " + frameRate.ToString("00"), guiStyle);
    }
}
