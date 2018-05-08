using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class button_con : MonoBehaviour {

    public GameObject[] weapons;
    public AudioClip button_switch;
    public AudioClip button_comfirm;
    public Transform UI_location;
    public GameObject weapon_UI;
    public GameObject gvr_reticle;
    public GazeInputModule gaze_input_module;

    private bool weapons_ready;
    private bool UI_on=false;
    private string big_button_data;

    void Start () {     
    }
	
	void Update () {
        big_button_data = GameObject.Find("player").GetComponent<motion_data>().big_button;//获取按钮的数据
        weapons_on();//检测武器是否已经装备
        con_by_bigbutton(big_button_data);//硬件按钮控制的

        //按下按钮出现武器UI
        if ((Input.GetKeyDown(KeyCode.F)) && weapons_ready == false)
        {
            weapon_UI.GetComponent<Canvas>().enabled = true;
            weapon_UI.transform.position = UI_location.position;
            weapon_UI.transform.rotation = UI_location.rotation;
            GetComponent<AudioSource>().PlayOneShot(button_switch, 50);
            gvr_reticle.SetActive(true);
        }
        else if ((Input.GetKeyUp(KeyCode.F)))
        {
            weapon_UI.GetComponent<Canvas>().enabled = false;
            GetComponent<AudioSource>().PlayOneShot(button_comfirm, 2);
            gvr_reticle.SetActive(false);
        }

        //若已经装上武器，按下按钮会出现提示
        if ((Input.GetKeyDown(KeyCode.F) || Input.GetKey(KeyCode.F)) && weapons_ready == true)
        {
            if (weapons[2].active == true || weapons[3].active == true || weapons[4].active == true)
            {
                GameObject.Find("managers").GetComponent<data_manager>().message_on(true, "Weapons In Use.");
            }
        }
        else if (Input.GetKeyUp(KeyCode.F)) { GameObject.Find("managers").GetComponent<data_manager>().message_on(false, " "); }
    }

    void weapons_on()//检测武器是否装备
    {
        for(int i = 0; i <= weapons.Length - 1; i++)
        {
            if (weapons[i].active == true) { weapons_ready = true;break; }
            else { weapons_ready = false; }
        }
    }

    void con_by_bigbutton(string button_event)
    {
        //若已经装上武器，按下按钮会出现提示
        if (button_event == "on" && weapons_ready == true)
        {
            if (weapons[2].active == true || weapons[3].active == true || weapons[4].active == true)
            {
                GameObject.Find("managers").GetComponent<data_manager>().message_on(true, "Weapons In Use.");
            }
        }
        else if (button_event == "off" && weapons_ready == true) { GameObject.Find("managers").GetComponent<data_manager>().message_on(false, " "); }

        //按下按钮出现武器UI
        if (button_event=="on")
        {
            if (weapons_ready || UI_on) { return; }
            weapon_UI.GetComponent<Canvas>().enabled = true;//出现UI
            weapon_UI.transform.position = UI_location.position;//设置位置
            weapon_UI.transform.rotation = UI_location.rotation;//设置偏转
            GetComponent<AudioSource>().PlayOneShot(button_switch, 50);//播放音效
            gvr_reticle.SetActive(true);//出现视点
            UI_on = true;
        }
        else if (button_event=="off"&&UI_on)
        {
            gaze_input_module.comfirm(true);
            weapon_UI.GetComponent<Canvas>().enabled = false;
            GetComponent<AudioSource>().PlayOneShot(button_comfirm, 2);
            gvr_reticle.SetActive(false);
            UI_on = false;
        }
    }
}
