using UnityEngine;
using System.Collections;
using System.Reflection;

public class rocket_child : MonoBehaviour {

    public float F;
    public float stop_s;
    public GameObject boom;

    private float timer = 0;
    public Transform target;
    private Rigidbody ri;
	// Use this for initialization
	void Start () {
        ri = gameObject.GetComponent<Rigidbody>();      
    }
	
	// Update is called once per frame
	void Update () {
        target = gameObject.GetComponent<find_rival>().target;
        timer += Time.deltaTime;
        if (timer <= 0.5) { ri.AddForce(transform.forward * 20000); }
        else
        {
            transform.LookAt(target);
            transform.Translate(0, 0, 80 * Time.deltaTime);
            gameObject.GetComponent<hover>().enabled = false;
        }
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "com")
        {
            col.gameObject.GetComponent<NavMeshAgent>().speed = stop_s;
            Rigidbody ri = col.gameObject.GetComponent<Rigidbody>();
            ri.AddForce(transform.forward * -F);
            ri.AddForce(transform.up * F);
            GameObject InstanceItem = Instantiate(boom, transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Player")
        {
            //col.gameObject.GetComponent<GoAndTurn>().foward_force = 0;
            Rigidbody ri = col.gameObject.GetComponent<Rigidbody>();
            ri.AddForce(transform.forward * -F);
            ri.AddForce(transform.up * F);
            GameObject InstanceItem = Instantiate(boom, transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);
        }
    }
}
