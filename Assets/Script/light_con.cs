using UnityEngine;
using System.Collections;

public class light_con : MonoBehaviour {

    private Light mylight;

	// Use this for initialization
	void Start () {
        mylight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.T))
            mylight.enabled = false;
        else
            mylight.enabled = true;
    }
}
