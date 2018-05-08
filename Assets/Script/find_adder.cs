using UnityEngine;
using System.Collections;

public class find_adder : MonoBehaviour {

    private int numb;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        numb = transform.GetChildCount();

    }

    void OnTriggerEnter(Collider com)
    {
        if (com.tag == "com")
        {
            com.gameObject.GetComponent<navi_test>().finding_a = true;
            GameObject adder = transform.GetChild(Random.Range(1,numb)).gameObject;
            com.gameObject.GetComponent<navi_test>().adder_fingding(adder);
        }
    }

    void OnTriggerExit(Collider com)
    {
        if (com.tag == "com")
        {
            com.gameObject.GetComponent<navi_test>().finding_a = false;
        }
    }
}
