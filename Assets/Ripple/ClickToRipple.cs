using UnityEngine;
using System.Collections;

public class ClickToRipple : MonoBehaviour {

    public Camera cam;
    public ParticleSystem fx;

    private void Start()
    {

        InvokeRepeating("ClickStuff", 0, 0.2f);

    }

	void ClickStuff() {
        Ray r = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit))
        {
            fx.transform.position = hit.point;
            fx.Play();
        }
	}
}
