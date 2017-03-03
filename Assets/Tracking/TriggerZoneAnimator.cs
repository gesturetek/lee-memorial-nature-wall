using UnityEngine;
using System.Collections;

public class TriggerZoneAnimator : TriggerZone {

    public Animator animator;
    public string triggerMessage = "Activate";
    
    public override void OnInteract()
    {
        if (animator)
        {
            animator.SetTrigger(triggerMessage);
        }
    }
}

