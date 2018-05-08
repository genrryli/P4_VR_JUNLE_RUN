using UnityEngine;
using System.Collections;

public class gravity_suffer : MonoBehaviour {

    private float timer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;     
        if (transform.parent.parent.tag=="com"&&timer>=2)
        {
            transform.parent.parent.gameObject.GetComponent<NavMeshAgent>().speed = 2;
            if (timer >= 7) { timer = 0; gameObject.SetActive(false); }
        }	
        if (transform.parent.parent.tag == "Player" && timer >= 2)
        {
            transform.parent.parent.gameObject.GetComponent<Rigidbody>().mass = 120;
            Debug.Log("is adding");
            if (timer >= 7) { timer = 0; transform.parent.parent.gameObject.GetComponent<Rigidbody>().mass = 60; gameObject.SetActive(false); }
        }
	}
}
