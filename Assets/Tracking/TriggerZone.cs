using UnityEngine;
using System.Collections;

public class TriggerZone : MonoBehaviour {

    public bool limitTriggerRate = true;
    public float reTriggerDelay = 1;
    public float lastTriggered = 0;

	public virtual void OnTriggerZone(Vector2 gtekTouchPosition)
    {
        if (limitTriggerRate)
        {
            if (Time.time > lastTriggered)
            {
                lastTriggered = Time.time + reTriggerDelay;
                OnInteract();
            }
        }
        else
        {
            OnInteract();
        }
    }

    public virtual void OnInteract()
    {
    }
}
