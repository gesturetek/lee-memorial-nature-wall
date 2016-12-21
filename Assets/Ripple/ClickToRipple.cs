using UnityEngine;
using System.Collections;

public class ClickToRipple : MonoBehaviour {

    public Plane plane;
    public Camera cam;
    public ParticleSystem fx;

    private void Start()
    {
        plane = new Plane(Vector3.up, Vector3.zero);
    }

	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = cam.ScreenPointToRay(Input.mousePosition);
            float hit;
            if (plane.Raycast(r, out hit))
            {
                fx.transform.position = r.GetPoint(hit);
                fx.Play();
                //fx.Emit(1);
            }
        }
	}
}
