using UnityEngine;
using System.Collections;

public class bike_con : MonoBehaviour {

    public float turning_scale;
    public float riding_scale=200;
    public GameObject bike_head;
    public GameObject head_direction;
    public Transform r_center;
    public AudioClip hit_r;
    public AudioClip hit_m;
    public AudioClip hit_wl;
    public AudioClip hit_wr;

    private float real_speed;
    private float real_angle;
    private int _reversal = 1;
    private Rigidbody rigid;
    private Transform start_t;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        start_t = gameObject.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //龙头固定角度偏转
        Vector3 angle_dif = new Vector3(0, real_angle, 0) - bike_head.transform.localEulerAngles;
        bike_head.transform.RotateAround(r_center.position, r_center.up, angle_dif.y);
        Vector3 angle_dif_2 = new Vector3(0, real_angle, 0) - head_direction.transform.localEulerAngles;
        head_direction.transform.Rotate(0, angle_dif.y, 0);

        if (game_manager.gm.gamestate == game_manager.game_state.playing) { motion(); }
    }

    void Update()
    {
    }

    void motion()
    {
        //车体前进，以及前进速度
        //float foward_force = real_speed * riding_scale;
        //rigid.AddForce(head_direction.transform.forward * foward_force);
        //Vector3 direction = new Vector3(0,0,0)
        gameObject.transform.Translate(0,0,1 * real_speed * riding_scale / 50);

        //车体转向，及转向速度
        float rotate_speed = real_angle * turning_scale * real_speed;
        if (real_speed > 0) { transform.Rotate(Vector3.up * rotate_speed * _reversal); }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Water")
        {
            rigid.AddForce(transform.forward * 300000);
            rigid.AddForce(transform.up * 100000);
            AudioSource.PlayClipAtPoint(hit_wr, transform.position);
        }
        if (collision.gameObject.tag == "block")
        {
            AudioSource.PlayClipAtPoint(hit_r, transform.position);
        }
        if (collision.gameObject.tag == "com")
        {
            AudioSource.PlayClipAtPoint(hit_m, transform.position);
        }
        if (collision.gameObject.tag == "wall")
        {
            AudioSource.PlayClipAtPoint(hit_wl, transform.position, 0.2f);
        }
    }

    public float real_speed_
    {
        get { return real_speed; }
        set { real_speed = value; }
    }

    public int reversal
    {
        get { return _reversal; }
        set { _reversal = value; }
    }

    public float real_angle_
    {
        get { return real_angle; }
        set { real_angle = value; }
    }

}
