using UnityEngine;
using System.Collections;

public class TriggerZoneFlowers : TriggerZone
{
    public Camera flowerCamera;
    public Camera gtekCamera;
    public ParticleSystem petals; 
    public LayerMask flowerLayer;

    public override void OnTriggerZone(Vector2 gtekTouchPosition)
    {
        if (Time.time > lastTriggered)
        {
            lastTriggered = Time.time + reTriggerDelay;
            FlowersAtPosition(gtekCamera.WorldToScreenPoint(gtekTouchPosition));
        }
    }

    void FlowersAtPosition(Vector3 gtekTouchPosition)
    {
        Ray r = flowerCamera.ScreenPointToRay(gtekTouchPosition);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, Mathf.Infinity, flowerLayer.value))
        {
          petals.transform.position = hit.point;
          petals.Play();
        }
    }
}
