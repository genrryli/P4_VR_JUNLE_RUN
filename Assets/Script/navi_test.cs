using UnityEngine;
using System.Collections;

public class navi_test : MonoBehaviour {

    public float reduce_speed;
    public bool finding_a=false;

    private GameObject[] distination;
    private NavMeshAgent agen;
    private Animator ani;
    private Vector3 dis;
    private int n=0;
    private float start_y;
    private float late_y;
    private float _y;
    private float max_speed;
    private float timer;
    private float change;
    private float start_reduce_speed;
    private float start_acceleration;

	// Use this for initialization
	void Start () {
        distination = GameObject.Find("managers").GetComponent<distance_manager>().distination;
        agen = GetComponent<NavMeshAgent>();
        ani = gameObject.GetComponent<Animator>();
        max_speed = agen.speed;
        agen.speed = 0;
        start_y = transform.localEulerAngles.y;
        start_acceleration = agen.acceleration;
        start_reduce_speed = reduce_speed;
    }

    // Update is called once per frame
    void Update()
    {
        //agen.stoppingDistance = stop_dis;
        if (game_manager.gm.gamestate == game_manager.game_state.playing)
        {
            agen.acceleration = Random.Range(start_acceleration - 50, start_acceleration + 100);
            reduce_speed = Random.Range(start_reduce_speed, start_reduce_speed + 0.5f);

            if (!finding_a) { dis = distination[n].transform.position; agen.stoppingDistance = 10; }
            agen.SetDestination(dis);
            if (agen.speed >= 0)
            {
                ani.SetBool("trace", true);
                ani.SetBool("run", true);
            }
            else { ani.SetBool("run", false); ani.SetBool("trace", true); }

            change = Random.Range(0f, 1f);
            change_distance();
            change_speed();
            catch_up();
        }
    }

    void change_distance()
    {
        if (Vector3.Distance(transform.position, distination[n].transform.position) <= 20)
        {            
            if (n < (distination.Length - 1)){ n += 1; }
            else if (n == distination.Length - 1) { n = 0; }
            else { ani.SetBool("trace", false); return; }
            
        }
    }

    void change_speed()
    {
        if (agen.speed <= max_speed) { agen.speed += change; }
        if (_y >= 1&&_y<=180) { agen.speed -= reduce_speed; agen.speed = Mathf.Clamp(agen.speed, max_speed/2, max_speed); }

        timer += Time.deltaTime;
        if (timer >= 0.01f)
        {
            late_y = transform.localEulerAngles.y;
            _y =Mathf.Abs(late_y - start_y);
            start_y = late_y;
            timer = 0;
        }
    }

    void catch_up()
    {
        int p_rank = game_manager.gm.read_rank(GameObject.FindWithTag("Player"));
        int c_rank= game_manager.gm.read_rank(GameObject.Find(gameObject.name));
        if (p_rank <= c_rank&&p_rank<=4)
        {
            float dis = Vector3.Distance(GameObject.FindWithTag("Player").transform.position, transform.position);
            gameObject.GetComponent<NavMeshAgent>().speed = 48;
            if (dis > 50)
            {
                gameObject.GetComponent<NavMeshAgent>().speed = 58;
            }         
        }
    }

    public void adder_fingding(GameObject adder)
    {
        dis = adder.transform.position ;
        agen.stoppingDistance = 1;
        Debug.Log(adder.name);
    }
}
