using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class data_manager : MonoBehaviour
{
    public GameObject sporter;
    public GameObject rotater_1;
    public GameObject rotater_2;
    public GameObject rotater_3;
    public GameObject rotater_4;
    public Text distance_data;
    public Text speed_data;
    public Text sport_data;
    public Text angle_data;
    public Text time;
    public Text loop;
    public Text self_rank;
    public Text all_rank;
    public Slider distance_percentage;
    public GameObject message_panel;
    public Text message;

    private GameObject player;
    private GameObject[] com;
    private float Wn = 0;
    private float W;
    private float timer2 = 0;
    private float S = 0;
    private float Sn = 0;
    private float v_virtual;
    private float true_v0 = 0;
    private float M = 60;
    private float N = 200;
    private int rank = 1;
    private bool is_wrong_way = true;
    private Vector3 start_location;

    public Text point_data;
    private int collect_point;
    private float time_loger = 0;
    public float time_log_now;

    // Use this for initialization
    void Start()
    {
        start_location = sporter.transform.position;
        collect_point = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bool is_re = GameObject.FindWithTag("Player").GetComponent<CarController>().is_reverse;

        cam_motion_blur();
        motion_data();
        time_data();
        loop_data();
        distance_left();
        rank_data();
        message_wrong_way(is_re);
    }

    //镜头模糊控制函数
    void cam_motion_blur()
    {
        MotionBlur mb = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UnityStandardAssets.ImageEffects.MotionBlur>();
        if (v_virtual >= 70) {
            mb.enabled = true;
            double aa = v_virtual - 70;
            mb.blurAmount = (float)(aa * 0.03);
        }
        else { mb.enabled = false; }
    }

    //运动数据计算函数
    void motion_data()
    {
        Vector3 late_location = sporter.transform.position;//更新位置
        timer2 += Time.deltaTime;

        if (timer2 >= 0.1f)
        {
            S = Vector3.Distance(start_location, late_location);//计算距离
            Sn = Sn + S;
            float true_v1 = GameObject.FindWithTag("Player").GetComponent<bike_con>().real_speed_;//输入的速度
            //float true_v1 = 0;
            if (true_v1 > true_v0) { W = M * (true_v1 - true_v0) * S / timer2 + N * S; } else { W = N * S; }//根据速度判断是否有做功，分段计算功
            Wn = Wn + W;
            start_location = late_location;
            true_v0 = true_v1;
            v_virtual = S / timer2;//计算场景中的速度
            timer2 = 0;
        }
        //获取角度数据
        //float rx = sporter_rotation.transform.localEulerAngles.x;
        //float rz = sporter_rotation.transform.localEulerAngles.z;
        //if (rx > 180) { rx = rx - 360; }
        //if (rz > 180) { rz = rz - 360; }
        float forward_height_difference = rotater_1.transform.position.y - rotater_2.transform.position.y;
        float forward_dis = Vector3.Distance(rotater_1.transform.position, rotater_2.transform.position);
        float cross_height_difference = rotater_3.transform.position.y - rotater_4.transform.position.y;
        float cross_dis = Vector3.Distance(rotater_3.transform.position,rotater_4.transform.position);
        float rz = Mathf.Asin(forward_height_difference / forward_dis)/Mathf.PI*180;
        float rx = Mathf.Asin(cross_height_difference / cross_dis) / Mathf.PI * 180;

        //计算运动数据
        speed_data.text = "+ " + (int)v_virtual;
        sport_data.text = "- " + (int)Wn / 4184;
        angle_data.text = (int)(rx) + "° , " + (int)(rz)+"°";
        
    }

    void point_collecting()
    {
        collect_point += 1;
        point_data.text = "" + collect_point;
    }

    public void time_data()
    {
        time_loger += Time.deltaTime;
        if (game_manager.gm.gamestate == game_manager.game_state.playing)
        {
            time_log_now = time_loger;
        }
        else { time_log_now = time_log_now; time_loger = 0; }

        time.text = ((int)(time_log_now / 60)).ToString("D2") + ":" + ((int)(time_log_now % 60)).ToString("D2") + ":" + ((int)((time_log_now - (int)time_log_now) * 100)).ToString("D2");
    }

    void loop_data()
    {
        int max_loop = game_manager.gm.max_loop;
        int current_loop = game_manager.gm.finished_loop + 1;
        loop.text = current_loop + "/" + max_loop;
    }

    void distance_left()
    {
        float one_loop_dis = GameObject.FindWithTag("Player").GetComponent<CarController>().CalcTotalDis();
        float all_loop_dis = one_loop_dis * game_manager.gm.max_loop;
        float current_loop_dis = GameObject.FindWithTag("Player").GetComponent<CarController>().finished_dis() + game_manager.gm.finished_loop * one_loop_dis;
        float left_dis = all_loop_dis - Mathf.Min(current_loop_dis, Sn);
        distance_data.text = ((int)left_dis).ToString("###,###") + "m";
        distance_percentage.value = Mathf.Min(current_loop_dis, Sn) / all_loop_dis;
    }

    void rank_data()
    {
        int all_players = GameObject.FindGameObjectsWithTag("Player").Length + GameObject.FindGameObjectsWithTag("com").Length;
        all_rank.text = "/ " + all_players;

        player = GameObject.FindWithTag("Player");
        //com = GameObject.FindGameObjectsWithTag("com");
        //float player_dis= player.GetComponent<CarController>().finished_dis() + player.GetComponent<CarController>().loop * player.GetComponent<CarController>().CalcTotalDis();

        //List<float> con_dis = new List<float>();
        //con_dis.Add(player_dis);
        //foreach (GameObject c in com)
        //{
        //    float dis = c.GetComponent<carcon_for_com>().finished_dis()+ c.GetComponent<carcon_for_com>().loop * c.GetComponent<carcon_for_com>().CalcTotalDis(); ;
        //    con_dis.Add(dis);
        //}

        //con_dis.Sort((x, y) => -x.CompareTo(y));
        //for(int i = 0; i <= con_dis.Count-1; i++)
        //{
        //    if (con_dis[i] == player_dis) { rank = i; }
        //}

        rank = game_manager.gm.read_rank(player);
        self_rank.text = "" + (rank);
    }

    void close_message_display()
    {
        message_panel.SetActive(false);
        message.text = null;
    }

    void message_wrong_way(bool x)
    {
        if (x&&is_wrong_way)
        {
            message_panel.SetActive(x);
            message.text = "WRONG WAY";
            is_wrong_way = false;
        }
        if (!x && !is_wrong_way)
        {
            close_message_display();
            is_wrong_way=true;
        }
    }

    public void message_on(bool x,string m)
    {
        message_panel.SetActive(x);
        message.text = m;
        message_panel.GetComponent<Animation>().enabled = !x;
        message.GetComponent<Animation>().enabled = !x;
    }
}