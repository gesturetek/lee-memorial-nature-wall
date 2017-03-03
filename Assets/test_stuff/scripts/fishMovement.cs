using UnityEngine;
using System.Collections;

public class fishMovement : MonoBehaviour {

    public float speed;
    float rotationSpeed = 0.75f;
    Vector3 averageHeading;
    Vector3 averagePos;
    float neighbourDistance = 3f;
    new Transform transform;

    Animator fishAnim;
    
    bool turning = false;
    // Use this for initialization
    void Start () {
        transform = base.transform;      
        speed += Random.Range(0.05f, 0.5f);
        fishAnim=this.GetComponent<Animator>();
        fishAnim.speed = speed;
    }

    
    // Update is called once per frame
	void FixedUpdate () {

        if (transform.localPosition.x >=  FishManager.tankSize.x||
            transform.localPosition.x <= -FishManager.tankSize.x||
            transform.localPosition.y >= FishManager.tankSize.y ||
            transform.localPosition.y <= -FishManager.tankSize.y||
            transform.localPosition.z >= FishManager.tankSize.z ||
            transform.localPosition.z <= -FishManager.tankSize.z) {
            turning = true; 
        }
        else {
            turning = false;
        }

        if (turning) {
            Vector3 dir = FishManager.goal - transform.localPosition;
            dir.y += Random.Range(0, 0.25f);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (Random.Range(0f, 5f) < 1f)
                ApplyRules();
        }
        transform.Translate(0,0, Time.deltaTime * speed);
	}

    void ApplyRules() {

        GameObject[] gos;
        gos = FishManager.allFish;

        Vector3 Centre= Vector3.zero;
        Vector3 Avoid = Vector3.zero;
        float gSpeed = 0.1f;
        Vector3 goalPos = FishManager.goal;

        float dist;
        int groupSize=0;
        for (int i = 0; i < gos.Length; i++) {
            if (gos[i] == this) { continue; } 

            dist = Vector3.Distance(gos[i].transform.position, transform.position);

            if (dist <= neighbourDistance)
            {
                Centre += gos[i].transform.position;
                groupSize++;

                if (dist < 1f)
                {
                    Avoid = Avoid + (transform.position - gos[i].transform.position);
                }
                fishMovement anotherFish = gos[i].GetComponent<fishMovement>();
                gSpeed = gSpeed + anotherFish.speed;
            }
        }

        if (groupSize > 0) {

            Centre = Centre / groupSize + (goalPos - transform.position);
            speed = gSpeed / groupSize;
            Vector3 dir = (Centre + Avoid) - transform.position;

            if (dir != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                     Quaternion.LookRotation(dir),
                                     rotationSpeed*Time.deltaTime);
            }
        }
    }
}
