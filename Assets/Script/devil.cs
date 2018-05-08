using UnityEngine;
using System.Collections;

public class devil : MonoBehaviour {

    public GameObject Item;
    public Transform StartLocation;

    private bool ready = false;
    private string big_button_data;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        big_button_data = GameObject.Find("player").GetComponent<motion_data>().big_button;
        if (big_button_data == "on") { ready = true; }


        if (Input.GetKeyDown(KeyCode.F) || transform.parent.root.tag == "com" || (big_button_data == "off" && ready))
        {
            ready = false;
            GameObject go = Instantiate(Item, StartLocation.position, StartLocation.rotation) as GameObject;
            go.name = transform.parent.root.name + "_";
            gameObject.SetActive(false);
        }
    }
}
