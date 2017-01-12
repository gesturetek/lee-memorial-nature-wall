using UnityEngine;
using System.Collections;

public class TriggerZone : MonoBehaviour {

    public float reTriggerDelay = 1;
    public float lastTriggered = 0;

	public virtual void OnTriggerZone(Vector2 gtekTouchPosition)
    {
        if (Time.time > lastTriggered)
        {
            lastTriggered = Time.time + reTriggerDelay;
            OnInteract();
        }
    }

    public virtual void OnInteract()
    {
    }
}
