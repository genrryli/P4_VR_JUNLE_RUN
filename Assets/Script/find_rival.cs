using UnityEngine;
using System.Collections;

public class find_rival : MonoBehaviour {

    private GameObject player;
    private GameObject[] com;
    public Transform target;
    private int player_rank;
    // Use this for initialization
    void Start()
    {
        com = GameObject.FindGameObjectsWithTag("com");
        string[] parent = transform.name.Split('_');
        player = GameObject.Find(parent[0]);
        player_rank = game_manager.gm.read_rank(player);
        find_target();
    }

    void find_target()
    {
        if (target != null) { return; }
        for (int i = 0; i <= com.Length - 1; i++)
        {
            if (game_manager.gm.read_rank(com[i]) == player_rank - 1)
            {
                target = com[i].transform;
                Debug.Log("kkkkkkkkkkkk" + game_manager.gm.read_rank(com[i]));
                break;
            }
            else if (game_manager.gm.read_rank(GameObject.Find("player")) == player_rank - 1)
            {
                target = GameObject.Find("player").transform;
                Debug.Log("find_player");
                break;
            }
        }
    }
}
