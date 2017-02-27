using UnityEngine;
using System.Collections;

public class FishManager : MonoBehaviour {

    public GameObject fishPrefab; 
    public static int amount = 15;
    public static GameObject[] allFish = new GameObject[amount];
    public static Vector3 goal = Vector3.zero;
    public static bool attraction = true; 
    public static Vector3 tankSize;
    public static Vector3 centreOfTank;

    public int targetChangeRate;
    public int minSize;
    public int maxSize;

    public float width;
    public float height;
    public float depth;

    public bool debug;

    GameObject debugGoal;

    // Use this for initialization
    void Start () {

        tankSize = new Vector3(width, height, depth);
        centreOfTank = transform.position;

        for (int i = 0; i < amount; i++) {
            Vector3 pos = new Vector3(
                Random.Range(-tankSize.x, tankSize.x),
                Random.Range(-tankSize.y, tankSize.y),
                Random.Range(-tankSize.z, tankSize.z));

           GameObject fish=(GameObject) Instantiate(fishPrefab);
            fish.transform.SetParent(transform, false);
            fish.transform.localPosition = pos; 
            fish.transform.localScale = Vector3.one * Random.Range(minSize, maxSize);
            fish.transform.rotation = Quaternion.Euler(0, 90, 0);
            allFish[i] = fish;
        }

            debugGoal = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            debugGoal.layer = LayerMask.NameToLayer("Environment");
            debugGoal.transform.SetParent(transform, false);
            debugGoal.SetActive(debug);
        

        InvokeRepeating("setGoalPos", 0, targetChangeRate);
        
	}
	



    void setGoalPos() {
        goal.x = Random.Range(-tankSize.x, tankSize.x);
        goal.y = Random.Range(-tankSize.y, tankSize.y);
        goal.z = Random.Range(-tankSize.z, tankSize.z);

        if (debug) {
           
            debugGoal.transform.localPosition = goal;
        }


        debugGoal.SetActive(debug);

    }



    public void setGoal(Vector3 pos, bool attract) {
        goal = pos;
        attraction = attract;



    }
}
