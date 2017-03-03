using UnityEngine;
using System.Collections;

public class TriggerZoneAnimatorOffset : TriggerZoneAnimator {

	public Animator[] animators;
	public float offsetForEach = 0.1f;

	private void Start () {

		foreach (Animator anim in animators) {
			transform.localScale = Random.Range (0.8f, 1.2f) * Vector3.one;
			anim.speed = Random.Range (0.8f, 1.2f);
		}

	}

	public override void OnInteract () {
		StartCoroutine (GoWithOffset ());
	}

	IEnumerator GoWithOffset () {
		foreach (Animator anim in animators) {
			anim.SetTrigger (triggerMessage);
			yield return new WaitForSeconds (offsetForEach);
		}
	}
}
