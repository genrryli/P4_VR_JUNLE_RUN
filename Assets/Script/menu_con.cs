using UnityEngine;
using System.Collections;

public class menu_con : MonoBehaviour {

    public GazeInputModule gaze_input_module;
    private string big_button_data;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        big_button_data = GameObject.Find("player").GetComponent<motion_data>().big_button;
        if (big_button_data == "on") { gaze_input_module.comfirm(true); }
    }
}
