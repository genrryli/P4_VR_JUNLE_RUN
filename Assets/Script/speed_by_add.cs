using UnityEngine;
using System.Collections;

public class speed_by_add : MonoBehaviour
{
    public AudioClip speed_;
    public GameObject speed;

    private Rigidbody rigid;
    private bool add;
    private float timer;
    private float n = 0;
    // Use this for initialization
    void Start()
    {
        rigid = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float a = 2 * (1 - Mathf.Pow(0.5f, n));
        if (add && timer <= 1) { rigid.AddForce(rigid.transform.forward * 20000*a); speed.SetActive(true); }
        if (add && timer > 1) { add = false;n = 0;speed.SetActive(false); }
    }

    void add_speed()
    {
        GetComponent<AudioSource>().PlayOneShot(speed_,3);
        add = true;
        timer = 0;
        n += 1;
    }

}