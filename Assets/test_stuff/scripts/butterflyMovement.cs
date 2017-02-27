using UnityEngine;
using System.Collections;

public class butterflyMovement : MonoBehaviour {

    public GameObject[] landingPads;
    public float maxSpeed = 3f;    //fastest possible speed
    public float maxForce = 0.25f;  //turning speed of the object

    Rigidbody rb;
    new Transform transform;
    Vector3 targetPos;
    bool goingHome = true;

    public float interactionTimeThreshold;

    float lastInteractionTime;

    void Start () {
        rb =GetComponent<Rigidbody>();
        transform = base.transform;
        selectLocationFromList();
	}

    private void selectLocationFromList()
    {
        if (landingPads.Length > 0)
        {
            targetPos = landingPads[Random.Range(0, landingPads.Length)].transform.position;
            goingHome = true;
        }
    }


    public void setLocationTarget(Vector3 location)
    {
            targetPos = location;
            goingHome = false;
            lastInteractionTime = Time.time;
    }

    void FixedUpdate()
    {
        if (Time.time > lastInteractionTime + interactionTimeThreshold) {
            selectLocationFromList();
        }

        if (goingHome)
        {
            if (Vector3.Distance(transform.position, targetPos) > 0.1f)
            {
                Seek(targetPos); // move towards target
            }
            else
            {
                transform.position = targetPos; // stop at target
            }
        }
        else { Seek(targetPos); }
           
    }

    void Seek(Vector3 target)
    {
        //Reynolds steering behaviour = desired - velocity

        //the direction that you need to go to reach the target
        Vector3 desired = target - transform.position;
        desired.Normalize();
        desired *= maxSpeed;
        
        Vector3 steer = desired - rb.velocity;
        
        if (steer.sqrMagnitude > maxForce * maxForce)
        {
            steer.Normalize();
            steer *= maxForce;
        }

        rb.velocity += steer;
        
        
    }
}
