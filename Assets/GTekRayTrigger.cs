using UnityEngine;
using System.Collections;
using GTek;
using System.Collections.Generic;

public class GTekRayTrigger : MonoBehaviour {

    #region InitializeGTek

    public bool GTekActive = false;
    private Camera GTekCamera;
    private const System.UInt32 MAX_POINTS = 33 * 1024;
    private const System.UInt32 MAX_POLYS = 1024;
    private const System.UInt32 WAIT_TIME = 1; // 1ms
    private Vector3[] m_aTrackerPoints;
    private int[] m_aTrackerPolys;
    private System.UInt32 m_uNumTrackerPoints = 0;
    private System.UInt32 m_uNumTrackerPolys = 0;
    private Vector3 origin;

    public void Awake()
    {
        if (GTek.GFX2U3D.Connect())
        {
            GTekActive = true;

            // Allocate the arrays we will use to receive the tracker polygons
            m_aTrackerPoints = new Vector3[MAX_POINTS];
            m_aTrackerPolys = new int[MAX_POLYS];
        }
    }

    void Start()
    {
        GTekCamera = GetComponent<Camera>();
        float cameraSize = GTekCamera.orthographicSize;

        // get screen size aspect ratio
        float screenAspectRatio = (float)Screen.width / (float)Screen.height;
        Vector2 screenWorldSize = new Vector2(cameraSize * screenAspectRatio * 2f, cameraSize * 2f);
        Vector2 targetScreenSize = new Vector2(GTekCamera.pixelWidth, GTekCamera.pixelHeight);

        // position container at bottom left of camera
        Vector3 position = GTekCamera.transform.position;
        position.y -= cameraSize;
        position.x -= cameraSize * screenAspectRatio;

        // Setup the transformation for the incoming tracker points
        GTek.GFX2U3D.SetPointTransform
        (
          screenWorldSize.x / targetScreenSize.x, // Scale factor for the x axis
          screenWorldSize.y / targetScreenSize.y, // Scale factor for the y axis
          0f,                                     // Set the z values of the points to this
          (System.UInt32)targetScreenSize.y              // Height of the display so we can flip the points
        );
        
        // remapping GTek origin (upper left corner of screen) to unity Coordinates
        origin = GTekCamera.ScreenToWorldPoint(Vector3.zero);
    }

    void OnDestroy()
    {
        if (GTek.GFX2U3D.IsConnected())
        {
            GTekActive = false;
            GTek.GFX2U3D.Disconnect();
        }
    }

    #endregion

    #region Tracking

    public int maxSamples = 1000;
    public int checkEveryXFrames = 1;
    public bool enableTracking = true;
    private Color debugColor = Color.white;
    public LineRenderer lineRenderer;

    void Update()
    {
        // don't check every frame for an performance boost
        if (Time.frameCount % checkEveryXFrames == 0)
        {
            if (enableTracking)
            {
                OnTrack();
            }
        }
    }

    void OnTrack()
    {
        List<Vector3> triggeredSpots = new List<Vector3>();

        if (showDebug)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPositions(new Vector3[] { });
        }
        else
        {
            lineRenderer.enabled = false;
        }

        // if GTek is active, use it's data
        if (GTekActive)
        {
            if (GTek.GFX2U3D.GetPolygons(m_aTrackerPoints, MAX_POINTS, ref m_uNumTrackerPoints, m_aTrackerPolys, MAX_POLYS, ref m_uNumTrackerPolys, WAIT_TIME))
            {
                for (int i = 0; i < maxSamples; i++)
                {
                    trackerPoints = m_uNumTrackerPoints;
                    if (i < m_uNumTrackerPoints)
                    {
                        Vector3 spot = new Vector3(m_aTrackerPoints[i].x + origin.x, m_aTrackerPoints[i].y + origin.y, 0.1f);
                        triggeredSpots.Add(spot);
                    }
                }
                TriggerSpot(triggeredSpots);
                if (showDebug)
                {
                    lineRenderer.SetVertexCount(triggeredSpots.Count);
                    lineRenderer.SetPositions(triggeredSpots.ToArray());
                }
            }
        }
        // if GTek isn't active, emulate the effect in Unity
        else
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = GTekCamera.ScreenToWorldPoint(Input.mousePosition);
                float radius = 0.1f;
                int numSegments = 10;
                Vector2 circlePoint = Vector2.zero;
                float angle = 20f;

                for (int i = 0; i < (numSegments + 1); i++)
                {
                    circlePoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                    circlePoint.y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
                    triggeredSpots.Add(circlePoint + mousePos);
                    triggeredSpots[triggeredSpots.Count - 1] = new Vector3(triggeredSpots[triggeredSpots.Count - 1].x, triggeredSpots[triggeredSpots.Count - 1].y, 0.1f);
                    angle += (360f / numSegments);
                }

                TriggerSpot(triggeredSpots);
            }

            if (showDebug)
            {
                lineRenderer.SetVertexCount(triggeredSpots.Count);
                lineRenderer.SetPositions(triggeredSpots.ToArray());
            }
        }
    }

    public GameObject hitObject;
    public uint trackerPoints = 0;
    public bool showDebug = false;

    void OnGUI()
    {
        if (showDebug)
        {
            if (hitObject)
            {
                lineRenderer.SetColors(Color.blue, Color.blue);
                GUILayout.Label(" Hitting: " + hitObject.name);
            }
            else
            {
                lineRenderer.SetColors(Color.white, Color.white);
                GUILayout.Label("None");
            }

            if (GTekActive)
            {
                GUILayout.Label("Tracker points: " + trackerPoints);
            }
            else
            {
                GUILayout.Label("Fake (Emulated) points: " + trackerPoints);
            }
        }
    }

    void TriggerSpot(List<Vector3> spots)
    {
        foreach (Vector3 spot in spots)
        {
            Vector2 overlapPoint = new Vector2(spot.x, spot.y);
            Collider2D p = Physics2D.OverlapPoint(overlapPoint);
            if (p)
            {
                hitObject = p.gameObject;
                break;
            }
            else
            {
                hitObject = null;
            }
        }
    }

    #endregion
}
