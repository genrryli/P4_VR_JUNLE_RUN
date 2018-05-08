using UnityEngine;
using System.Collections;

public class road_check : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider Trigger)
    {
        int check_points = GameObject.FindWithTag("Player").GetComponent<CarController>().CheckPoints.Count;
        int all_check_points= GameObject.FindWithTag("Player").GetComponent<CarController>().WaypointsModelAll.Count;
        if (Trigger.tag == "final_check"&&check_points>=all_check_points/3*2)
        {
            game_manager.gm.finished_loop ++;
        }
    }
}
