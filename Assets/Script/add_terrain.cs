using UnityEngine;
using System.Collections;

public class add_terrain : MonoBehaviour {
    
    public GameObject[] late_terrain;

    private GameObject humen;
    private float dis;
    private int x;

	// Use this for initialization
	void Start () {
        humen = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        x = Random.Range(0,4);
        dis = Vector3.Distance(humen.transform.position, transform.position);
        if (dis > 400)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {     
            GameObject new_terrain = Instantiate(late_terrain[x], transform.position + new Vector3(0, 0, 200), late_terrain[x].transform.rotation) as GameObject;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
