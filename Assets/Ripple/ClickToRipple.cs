using UnityEngine;
using System.Collections;

public class ClickToRipple : MonoBehaviour {

    public Camera cam;
    public ParticleSystem fx;

	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit))
            {
                fx.transform.position = hit.point;
                fx.Play();
            }
        }
	}
}
