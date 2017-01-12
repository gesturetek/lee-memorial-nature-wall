using UnityEngine;
using System.Collections;

public class FrameCounter : MonoBehaviour {

    public float frameRate;


    private void Start()
    {
        Application.targetFrameRate = 100;
    }

    void Update () {

        frameRate = Time.frameCount / Time.time;
	}

    private void OnGUI()
    {
        GUILayout.Label("Framerate: " + frameRate.ToString("00"));
    }
}
