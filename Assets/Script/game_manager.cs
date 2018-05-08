using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public  class game_manager : MonoBehaviour {

    static public game_manager gm;
    public enum game_state { start, ready, playing, finish}
    public game_state gamestate;
    public GameObject start_time_count;
    public int finished_loop = 0;
    public int max_loop;

    private int rank;
    private float g_dis;
    // Use this for initialization
    void Start () {
        if (gm == null) { gm = GetComponent<game_manager>(); }
        //gm.gamestate = game_state.ready;
    }

    // Update is called once per frame
    void Update () {
        start_running();
        read_check_point();
    }

    void read_check_point()
    {
        if (finished_loop >= max_loop) { gm.gamestate = game_state.finish; }
    }

    void start_running()
    {
        if (start_time_count == null) { return; }
        if (start_time_count.active == false&&gm.gamestate==game_state.ready) { gm.gamestate = game_state.playing; }
    }

    public int read_rank(GameObject g)
    {
        if (g.tag == "Player")
        {
            g_dis = g.GetComponent<CarController>().finished_dis() + g.GetComponent<CarController>().loop * g.GetComponent<CarController>().CalcTotalDis();
        }
        else if (g.tag == "com")
        {
            g_dis = g.GetComponent<carcon_for_com>().finished_dis() + g.GetComponent<carcon_for_com>().loop * g.GetComponent<carcon_for_com>().CalcTotalDis();
        }

        GameObject player = GameObject.FindWithTag("Player");
        GameObject[] com = GameObject.FindGameObjectsWithTag("com");
        float player_dis = player.GetComponent<CarController>().finished_dis() + player.GetComponent<CarController>().loop * player.GetComponent<CarController>().CalcTotalDis();

        List<float> con_dis = new List<float>();
        con_dis.Add(player_dis);
        foreach (GameObject c in com)
        {
            float dis = c.GetComponent<carcon_for_com>().finished_dis() + c.GetComponent<carcon_for_com>().loop * c.GetComponent<carcon_for_com>().CalcTotalDis(); ;
            con_dis.Add(dis);
        }

        con_dis.Sort((x, y) => -x.CompareTo(y));
        for (int i = 0; i <= con_dis.Count - 1; i++)
        {
            if (con_dis[i] == g_dis) { rank = i+1; }
        }

        return rank;
    }

   
}
