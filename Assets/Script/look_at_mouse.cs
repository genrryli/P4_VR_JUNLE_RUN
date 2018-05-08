using UnityEngine;
using System.Collections;

public class look_at_mouse : MonoBehaviour {

    public GameObject cube;

    private Vector3 point;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            point = hit.point;
            cube.transform.position = point;
        }

    }
}
