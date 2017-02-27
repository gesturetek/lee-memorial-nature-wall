using UnityEngine;
using System.Collections;

public class TriggerZoneButterfly : TriggerZone
{
    public Camera environmentCam;
    public Camera gtekCamera;
    public LayerMask layer;
    public butterflyMovement butterfly;
    

    public override void OnTriggerZone(Vector2 gtekTouchPosition)
    {
        if (Time.time > lastTriggered)
        {
            lastTriggered = Time.time + reTriggerDelay;
            hoverAtPoint(gtekCamera.WorldToScreenPoint(gtekTouchPosition));
        }
    }

    void hoverAtPoint(Vector3 gtekTouchPosition)
    {
        Ray r = environmentCam.ScreenPointToRay(gtekTouchPosition);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit,layer.value))
        {
            butterfly.setLocationTarget(hit.point);   
        }
    }
}
