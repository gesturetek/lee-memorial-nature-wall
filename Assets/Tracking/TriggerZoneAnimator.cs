using UnityEngine;
using System.Collections;

public class TriggerZoneAnimator : TriggerZone {

    public Animator animator;
    public string triggerMessage = "Interact";
    public float reTriggerDelay = 1;
    private float lastTriggered = 0;

    public override void OnTriggerZone()
    {
        if (Time.time > lastTriggered)
        {
            base.OnTriggerZone();
            animator.SetTrigger(triggerMessage);
            lastTriggered = Time.time + reTriggerDelay;
        }
    }
}
