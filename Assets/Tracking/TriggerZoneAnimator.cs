using UnityEngine;
using System.Collections;

public class TriggerZoneAnimator : TriggerZone {

    public Animator animator;
    public Transform testObject;
    public string triggerMessage = "Interact";
    
    public override void OnInteract()
    {
        if (animator)
        {
            animator.SetTrigger(triggerMessage);
        }
        else if (testObject)
        {
            StartCoroutine(Spin());
        }
    }

    IEnumerator Spin()
    {
        for (float i = 0; i < reTriggerDelay; i += Time.deltaTime)
        {
            testObject.Rotate(0, Time.deltaTime * 360, 0);
            yield return null;
        }
    }
}

