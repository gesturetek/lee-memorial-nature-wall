using UnityEngine;
public class RaycastTest : MonoBehaviour
{

#if UNITY_EDITOR

    // used to check what the camera has hit!
    private Camera cam;

    [Tooltip("Raycast from mouse position or from center of main camera")]
    public bool RaycastFromMouse;
    

    void Awake() {

       cam= Camera.main;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray =  cam.ScreenPointToRay(Input.mousePosition) ;

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                
                    Debug.Log("Name = " + hit.collider.name);
                    Debug.Log("Tag = " + hit.collider.tag);
                    Debug.Log("Hit Point = " + hit.point);
                    Debug.Log("Object position = " + hit.collider.gameObject.transform.position);
                    Debug.Log("--------------");



                    //Debug.Log(hit);

                
            }
        }
    }
#endif
}