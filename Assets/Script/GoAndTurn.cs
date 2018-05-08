using UnityEngine;
using System.Collections;

public class GoAndTurn : MonoBehaviour {

    //public Transform direction;
    public float foward_force=1000;
    public float turn_speed=1;
    public float foward_max_force=10000;
    public float jump_force = 30000;
    public float float_force = 5000;

    public int reversal=1;
    public AudioClip hit_r;
    public AudioClip hit_m;
    public AudioClip hit_wl;
    public AudioClip hit_wr;
    public AudioClip jump_;

    private Animator humen_an;
    private Rigidbody rigid;
    private Vector3 start_location;
    private float timer2 = 0;
    private float start_force;
    private float turn_scale = 0.01f;

    private float groundedRaycastDistance=0.1f; //在判定玩家是否在地面上时，向地面发射射线的射线长度
    private bool isGrounded;//玩家是否在地面上
    private float running_speed;

    private RaycastHit hit;
    private Ray down_ray;
    private float height_distance;
    void Start ()
    {
        humen_an = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        start_force = foward_force;
        start_location = transform.position;

        down_ray = new Ray(transform.position, -Vector3.up);
        if (Physics.Raycast(down_ray, out hit))
        {
            height_distance = hit.distance;
        }
    }

    void FixedUpdate()
    {
        //从玩家的位置垂直向下发出长度为groundedRaycastDistance的射线，返回值表示玩家是否该射线是否碰撞到物体，该句代码用于检测玩家是否在地面上
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, groundedRaycastDistance);

        //if (Physics.Raycast(down_ray, out hit))
        //{
        //    float ground_height = hit.point.y;
        //    float move_down = -transform.position.y + ground_height;
        //    transform.Translate(0, -hit.distance + height_distance, 0);
        //}

        float x = Input.GetAxisRaw("Horizontal")*reversal;
        float y = Input.GetAxisRaw("Vertical");
        float r = Input.GetAxisRaw("Mouse X");
        float u = Input.GetAxisRaw("Mouse Y");
        bool j = Input.GetKeyDown(KeyCode.Space);
        bool jj = Input.GetKey(KeyCode.Space);
        bool back = Input.GetKey(KeyCode.Z);

        if (game_manager.gm.gamestate == game_manager.game_state.playing)
        {
            motion_manager(x, y, r, u, j, jj, back);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (foward_force >= foward_max_force) foward_force -= 100;

        speed_reading();
    }

    //动作管理函数
    void motion_manager(float x,float y,float r,float u,bool j,bool jj,bool back)
    {
        if (isGrounded) { Walking(y, back); }
        //Walking(y, back);
        turning(x);  
        jumping(j,jj); 
    }

    //行走函数
    void Walking(float y,bool back)
    {
        if (back) { rigid.AddForce(transform.forward * y * -foward_force); }
        rigid.AddForce(transform.forward * y * foward_force);
        humen_an.SetFloat("run_speed", running_speed);        
        if (y != 0)
        {
            foward_force = foward_force + 10;
            if (running_speed == 0) { foward_force = start_force; }
            foward_force = Mathf.Clamp(foward_force, 0, foward_max_force);
        }
        else
        {
            foward_force = start_force;
        }
    }

    //旋转函数
    void turning(float r)
    {
        if (r == 0) { turn_scale = 0.01f; }
        turn_scale = turn_scale + 0.1f;
        transform.Rotate(Vector3.up * (turn_speed+turn_scale) * r);
        if (r == 1 && running_speed >= 70) { rigid.AddForce(transform.right * 1000 * turn_scale); }
        if (r == -1 && running_speed >= 70) { rigid.AddForce(transform.right * -1000 * turn_scale); }
    }

    void jumping( bool j,bool jj)
    {      
        if (isGrounded&&j)
        {
            humen_an.SetTrigger("jump");
            rigid.AddForce(transform.up * jump_force);
            AudioSource.PlayClipAtPoint(jump_, transform.position);
        }
        if (!isGrounded)
        {
            humen_an.SetBool("air",true);
            rigid.AddForce(-transform.up * (float_force-50));
            if (jj) { rigid.AddForce(transform.up * float_force); }
        }
        else { humen_an.SetBool("air", false); }
    }

    void speed_reading()
    {
        Vector3 late_location = transform.position;//更新位置
        timer2 += Time.deltaTime;

        if (timer2 >= 0.1f)
        {
            float S = Vector3.Distance(start_location, late_location);//计算距离
            start_location = late_location;
            running_speed = S / timer2;//计算场景中的速度
            timer2 = 0;
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag != ("ground"))
    //    {
    //        foward_force -= 50f * running_speed;
    //    }
    //}

    //void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.tag != ("ground"))
    //    {
    //        foward_force -= 5;
    //    }
    //}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name=="Water")
        {
            rigid.AddForce(transform.forward * 300000);
            rigid.AddForce(transform.up * 100000);
            AudioSource.PlayClipAtPoint(hit_wr, transform.position);
        }
        if (collision.gameObject.tag== "block")
        {          
            AudioSource.PlayClipAtPoint(hit_r, transform.position);
        }
        if (collision.gameObject.tag == "com")
        {
            AudioSource.PlayClipAtPoint(hit_m, transform.position);
        }
        if (collision.gameObject.tag == "wall")
        {
            AudioSource.PlayClipAtPoint(hit_wl, transform.position,0.2f);
        }
    }
}
