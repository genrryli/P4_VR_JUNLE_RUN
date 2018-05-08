using UnityEngine;
using System.Collections;

public class draw_line : MonoBehaviour {

   
    public GameObject boom;
    public float width=0.1f;

    private Transform location_0;
    private Transform location_1;
    private GameObject boo;
    private LineRenderer line;
	// Use this for initialization
	void Start () {
        //location_1 = gameObject.GetComponent<magnent>().target;
        location_0 = transform;
        line = gameObject.GetComponent<LineRenderer>();
        
    }
	
	// Update is called once per frame
	void Update () {
        location_1 = gameObject.GetComponent<magnent>().target;
        line.SetPosition(0, location_0.position);
        
        line.SetWidth(width, width);
        if (location_1 != null&&boo==null)
        {
            boo = Instantiate(boom, location_1.position + new Vector3(0, 0.5f, 0), location_1.rotation) as GameObject;
            
        }
        if (boo != null)
        {
            boo.transform.position = location_1.position + new Vector3(0, 0.5f, 0);
            line.SetPosition(1, location_1.position + new Vector3(0, 0.5f, 0));
        }        
    }
}
