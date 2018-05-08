using UnityEngine;
using System.Collections;

public class devil_suffer : MonoBehaviour {

    private float timer;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (transform.parent.parent.tag == "com" && timer >= 1)
        {
            transform.parent.parent.gameObject.GetComponent<NavMeshAgent>().acceleration=0;
            if (timer >= 6) { timer = 0; gameObject.SetActive(false); }
        }
        if (transform.parent.parent.tag == "Player" && timer >= 1)
        {
            transform.parent.root.gameObject.GetComponent<bike_con>().reversal = -1;
            if (timer >= 6) { timer = 0; transform.parent.root.gameObject.GetComponent<bike_con>().reversal = 1; gameObject.SetActive(false); }
        }
    }
}
