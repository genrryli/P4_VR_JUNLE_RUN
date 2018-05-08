using UnityEngine;
using System.Collections;

public class stuff_by_add : MonoBehaviour {

    [Header("CON_UI")]
    public GameObject[] CON_UI;
    public GameObject[] CON_UI2;
    public GameObject[] _CON_UI;
    public GameObject[] _CON_UI2;

    [Header("ITEMS")]
    public GameObject fog;
    public GameObject rocket;
    public GameObject gun;
    public GameObject oil;
    //public GameObject oil_father;
    public GameObject shield;
    public GameObject angel;
    public GameObject magnet;
    public GameObject gravity;
    public GameObject devil;
    public GameObject gravity_suffer;
    public GameObject devil_suffer;

    private Rigidbody instantiate_item;
    private bool button1_come_up;
    private bool button2_come_up;
    private int button1_up_num=0;
    private int button2_up_num = 0;
    // Use this for initialization
    void Start () {       
	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i <= 9; i++)
        {         
            if (CON_UI[i].active == true) { button1_up_num += 1;}
        }
        if (button1_up_num == 0) { button1_come_up = false; }
        else if (button1_up_num != 0) { button1_come_up = true; button1_up_num = 0; }

        for (int i = 0; i <= 9; i++)
        {
            if (CON_UI2[i].active == true) { button2_up_num += 1; }
        }
        if (button2_up_num == 0) { button2_come_up = false; }
        else if (button2_up_num != 0) { button2_come_up = true; button2_up_num = 0; }

        for(int i=0; i <= 9; i++)
        {
            _CON_UI[i].SetActive(CON_UI[i].active);
            _CON_UI2[i].SetActive(CON_UI2[i].active);
        }
    }

    public void add_s() { GameObject.Find("managers").GetComponent<speed_by_add>().SendMessage("add_speed"); }
    public void drop_fog() { Instantiate(fog, GameObject.FindWithTag("Player").transform.position, GameObject.FindWithTag("Player").transform.rotation) ;}
    public void add_rocket(bool x) { rocket.SetActive(x); }
    public void add_gun(bool x) { gun.SetActive(x); }
    public void drop_oil()
    {
        Instantiate(oil, GameObject.FindWithTag("Player").transform.position, GameObject.FindWithTag("Player").transform.rotation);
        //Instantiate(oil_father, GameObject.FindWithTag("Player").transform.position, GameObject.FindWithTag("Player").transform.rotation);
    }
    public void add_shield(bool x) { shield.SetActive(x); }
    public void add_angel(bool x) { angel.SetActive(x); }
    public void add_magnet(bool x) { magnet.SetActive(x); }
    public void add_gravity(bool x) { gravity.SetActive(x); }
    public void add_devil(bool x) { devil.SetActive(x); }
    public void add_gravity_suffer(bool x) { gravity_suffer.SetActive(x); }
    public void add_devil_suffer(bool x) { devil_suffer.SetActive(x); }


    public void add_button(int i)
    {
        if (button1_come_up)
        {
            if (button2_come_up) { return; }
            CON_UI2[i].SetActive(true); return;
        }       
        CON_UI[i].SetActive(true);
    }

    public void com_add(string com_name,int stuff)
    {
        GameObject com = GameObject.Find(com_name);
        switch (stuff)
        {
            case 0:                
                break;
            case 1:
                break;
            case 2:
                Instantiate(fog, com.transform.position, com.transform.rotation);
                break;
            case 3:
                com.transform.FindChild("Weapon").FindChild("Rocket").gameObject.SetActive(true);
                break;
            case 4:
                com.transform.FindChild("Weapon").FindChild("gun").gameObject.SetActive(true);
                break;
            case 5:
                Instantiate(oil, com.transform.position,com.transform.rotation);
                break;
            case 6:
                com.transform.FindChild("Weapon").FindChild("shield").gameObject.SetActive(true);
                break;
            case 7:
                com.transform.FindChild("Weapon").FindChild("angel").gameObject.SetActive(true);
                break;
            case 8:
                com.transform.FindChild("Weapon").FindChild("magnet").gameObject.SetActive(true);
                break;
            case 9:
                com.transform.FindChild("Weapon").FindChild("gravity").gameObject.SetActive(true);
                break;
            case 10:
                com.transform.FindChild("Weapon").FindChild("devil").gameObject.SetActive(true);
                break;
        }
     }
}
