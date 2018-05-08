using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ui_finish : MonoBehaviour {

    public Text rank;
    public Text all_rank;
    public Text time;
    public Text best_time;
    public Text score;
    public Text all_score;
    public Text w_rank;
    public Text kaluli;
    public RawImage star_0;
    public Texture[] star;

    private float score_data;
    private float best_time_data;
    private int all_score_data;
    private Component data;
	// Use this for initialization
	void Start () {
        rank.text = GameObject.Find("managers").GetComponent<data_manager>().self_rank.text;
        float x = 0;
        if (rank.text == "1") { x = 30; star_0.texture = star[3]; }
        else if (rank.text == "2") { x = 20; star_0.texture = star[2]; }
        else if (rank.text == "3") { x = 10; star_0.texture = star[1]; }
        else { x = 0; star_0.texture = star[0]; }
        score_data = GameObject.FindWithTag("Player").GetComponent<CarController>().CalcTotalDis() * game_manager.gm.max_loop / (int)GameObject.Find("managers").GetComponent<data_manager>().time_log_now * 2 + x;
        all_rank.text = GameObject.Find("managers").GetComponent<data_manager>().all_rank.text + "名次";
        time.text = GameObject.Find("managers").GetComponent<data_manager>().time.text;
        if (PlayerPrefs.GetFloat("best_time")!= 0) { best_time.text = ((int)(PlayerPrefs.GetFloat("best_time") / 60)).ToString("D2") + ":" + ((int)(PlayerPrefs.GetFloat("best_time") % 60)).ToString("D2") + ":" + ((int)((PlayerPrefs.GetFloat("best_time") - (int)PlayerPrefs.GetFloat("best_time")) * 100)).ToString("D2"); }
        score.text = (int)score_data+"";
        if (PlayerPrefs.GetInt("all_score") != 0) { all_score.text = PlayerPrefs.GetInt("all_score").ToString(); }

        kaluli.text = GameObject.Find("managers").GetComponent<data_manager>().sport_data.text;

        all_score_data = (int)(score_data + PlayerPrefs.GetInt("all_score"));
        PlayerPrefs.SetInt("all_score", all_score_data);

        best_time_data = PlayerPrefs.GetFloat("best_time");
        if (best_time_data == 0 || best_time_data > GameObject.Find("managers").GetComponent<data_manager>().time_log_now)
        {
            best_time_data = GameObject.Find("managers").GetComponent<data_manager>().time_log_now;
            PlayerPrefs.SetFloat("best_time", best_time_data);
        }
    }
	
	// Update is called once per frame
	void Update () {
 
    }
}
