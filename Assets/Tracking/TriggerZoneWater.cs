using UnityEngine;
using System.Collections;

public class TriggerZoneWater : TriggerZone {

    public Camera waterCamera;
    public Camera gtekCamera;
    public ParticleSystem rippleFX;
    public LayerMask waterLayer;

    public override void OnTriggerZone(Vector2 gtekTouchPosition)
    {
        if (Time.time > lastTriggered)
        {
            lastTriggered = Time.time + reTriggerDelay;
            RippleAtPosition(gtekCamera.WorldToScreenPoint(gtekTouchPosition));
        }
    }

    void RippleAtPosition(Vector3 gtekTouchPosition)
    {
        Ray r = waterCamera.ScreenPointToRay(gtekTouchPosition);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, Mathf.Infinity, waterLayer.value))
        {
            rippleFX.transform.position = hit.point;
            rippleFX.Play();
        }
    }
}
