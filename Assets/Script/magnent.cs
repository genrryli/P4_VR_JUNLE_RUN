using UnityEngine;
using System.Collections;

public class magnent : MonoBehaviour {

    public float force;   
    public Transform target;

    private GameObject player;
    private GameObject[] com;
    private int player_rank;
    private float timer;
    // Use this for initialization
    void Start () {
        com = GameObject.FindGameObjectsWithTag("com");
        player = GameObject.Find(transform.parent.parent.name);    
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 2) { timer = 0; gameObject.SetActive(false); }
        find_target();
        if (target != null)
        {
            Vector3 force_direction = (target.position - player.transform.position)/Vector3.Distance(target.position, player.transform.position);
            player.gameObject.GetComponent<Rigidbody>().AddForce(force_direction * force);
            target.gameObject.GetComponent<Rigidbody>().AddForce(force_direction * -force);
        }
    }

    void find_target()
    {
        player_rank = game_manager.gm.read_rank(player);
        for (int i = 0; i <= com.Length - 1; i++)
        {
            if (game_manager.gm.read_rank(com[i]) == player_rank - 1)
            {
                target = com[i].transform;
                Debug.Log("dddd" + game_manager.gm.read_rank(com[i]));
                
                break;
            }
        }
    }
}
