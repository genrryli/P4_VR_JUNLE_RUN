using UnityEngine;
using System.Collections;

public class gravity_child : MonoBehaviour {

    public float speed;
    public AudioClip shound;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0,0, speed*Time.deltaTime);
	}

    void OnTriggerEnter(Collider Trigger)
    {
        if (Trigger.name + "_" == gameObject.name) { return; }
        if (Trigger.tag == "com" || Trigger.tag == "Player")
        {
            Trigger.transform.FindChild("Weapon").FindChild("gravity_suffer").gameObject.SetActive(true);
            AudioSource.PlayClipAtPoint(shound, transform.position);
        }
    }
}
