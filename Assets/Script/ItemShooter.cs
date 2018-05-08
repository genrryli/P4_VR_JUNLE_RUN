using UnityEngine;
using System.Collections;

public class ItemShooter : MonoBehaviour
{
    public float ThrustForce = 5000f;
    public Rigidbody Item;
    public Transform StartLocation;
    public int set_item_num=1;

    private bool ready = false;
    private string big_button_data;
    private Rigidbody InstanceItem;
    private float timer=0;
    private int item_num;
    void Start()
    {
        item_num = set_item_num;
        if (StartLocation == null) { StartLocation = transform; }
    }

    void Update()
    {
        big_button_data = GameObject.Find("player").GetComponent<motion_data>().big_button;
        if (big_button_data == "on") { ready = true; }

            if (transform.parent.root.tag == "Player" &&(Input.GetKeyUp(KeyCode.F) || (big_button_data == "off" && ready)) && item_num > 0)
        {
            item_num -= 1;
            InstanceItem = Instantiate(Item, StartLocation.position, StartLocation.rotation) as Rigidbody;
            InstanceItem.AddForce(StartLocation.forward * ThrustForce);
            ready = false;
        }

        if (transform.parent.root.tag == "com" && item_num > 0)
        {
            item_num -= 1;
            InstanceItem = Instantiate(Item, StartLocation.position, StartLocation.rotation) as Rigidbody;
            InstanceItem.AddForce(StartLocation.forward * ThrustForce);
        }
        

        if (item_num <= 0)
        {
            timer += Time.deltaTime;
            if (timer >= 1.0f)
            {
                //GameObject.Find("managers").GetComponent<stuff_by_add>().add_gun(false);
                gameObject.SetActive(false);
                item_num = set_item_num;
            }           
        }
   
    }
}