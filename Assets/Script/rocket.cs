using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour {

    public Rigidbody Item;
    public Transform StartLocation;
    public AudioClip rocket_s;

    private bool ready=false;
    private string big_button_data;
    private Rigidbody InstanceItem;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        big_button_data = GameObject.Find("player").GetComponent<motion_data>().big_button;
        if (big_button_data == "on") { ready = true; }


        if (Input.GetKeyDown(KeyCode.F)||transform.parent.root.tag=="com"|| (big_button_data == "off"&&ready))
        {
            ready = false;
            InstanceItem = Instantiate(Item, StartLocation.position, StartLocation.rotation) as Rigidbody;
            InstanceItem.name = transform.parent.root.name+"_";
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(rocket_s, StartLocation.position, 1);
        }

    }
}
