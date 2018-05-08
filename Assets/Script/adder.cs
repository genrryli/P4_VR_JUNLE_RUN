using UnityEngine;
using System.Collections;

public class adder : MonoBehaviour {

    public enum add { speed,speed_con_ui,fog_con_ui,rocket_con_ui, gun_con_ui, oil_con_ui,shield_con_ui, angel_con_ui, magnet_con_ui, gracity_con_ui, devil_con_ui,random }
    public add add_stuff;
    public bool self_rotate=true;
    public AudioClip collid_shound;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (self_rotate) transform.Rotate( Vector3.up,10);
    }

    void OnTriggerEnter(Collider col)
    {
        int stuff = (int)add_stuff;
        if (stuff == 11) { stuff = Random.Range(1, 11); }

        if (col.gameObject.tag == "com")
        {
            GameObject.Find("managers").GetComponent<stuff_by_add>().com_add(col.transform.name, stuff);
            col.gameObject.GetComponent<navi_test>().finding_a = false;
            //GetComponent<AudioSource>().PlayOneShot(collid_shound);
            AudioSource.PlayClipAtPoint(collid_shound, transform.position);

            //Destroy(gameObject, 2f);
        }

        if (col.gameObject.tag == "Player")
        {                   
            switch (stuff)
            {
                case 0:
                    speed(col);
                    break;
                case 1:
                    speed_con(col);
                    break;
                case 2:
                    fog_con(col);
                    break;
                case 3:
                    rocket_con(col);
                    break;
                case 4:
                    gun_con(col);
                    break;
                case 5:
                    oil_con(col);
                    break;
                case 6:
                    shield_con(col);
                    break;
                case 7:
                    angel_con(col);
                    break;
                case 8:
                    magnet_con(col);
                    break;
                case 9:
                    gravity_con(col);
                    break;
                case 10:
                    devil_con(col);
                    break;                 
            }         
        }
    }

    void speed(Collider col)
    {
        GameObject.Find("managers").GetComponent<speed_by_add>().SendMessage("add_speed") ;
        GetComponent<AudioSource>().PlayOneShot(collid_shound);
        Destroy(gameObject,1f);
    }

    void speed_con(Collider col)
    {       
        GameObject.Find("managers").GetComponent<stuff_by_add>().add_button(0);
        GetComponent<AudioSource>().PlayOneShot(collid_shound);
        Destroy(gameObject, 1f);
    }

    void fog_con(Collider col)
    {
        GameObject.Find("managers").GetComponent<stuff_by_add>().add_button(1);
        GetComponent<AudioSource>().PlayOneShot(collid_shound);
        Destroy(gameObject, 1f);
    }

    void rocket_con(Collider col)
    {
        GameObject.Find("managers").GetComponent<stuff_by_add>().add_button(2);
        GetComponent<AudioSource>().PlayOneShot(collid_shound);
        Destroy(gameObject, 1f);
    }

    void gun_con(Collider col)
    {
        GameObject.Find("managers").GetComponent<stuff_by_add>().add_button(3);
        GetComponent<AudioSource>().PlayOneShot(collid_shound);
        Destroy(gameObject, 1f);
    }


    void oil_con(Collider col)
    {
        GameObject.Find("managers").GetComponent<stuff_by_add>().add_button(4);
        GetComponent<AudioSource>().PlayOneShot(collid_shound);
        Destroy(gameObject, 1f);
    }

    void shield_con(Collider col)
    {
        GameObject.Find("managers").GetComponent<stuff_by_add>().add_button(5);
        GetComponent<AudioSource>().PlayOneShot(collid_shound);
        Destroy(gameObject, 1f);
    }

    void angel_con(Collider col)
    {
        GameObject.Find("managers").GetComponent<stuff_by_add>().add_button(6);
        GetComponent<AudioSource>().PlayOneShot(collid_shound);
        Destroy(gameObject, 1f);
    }

    void magnet_con(Collider col)
    {
        GameObject.Find("managers").GetComponent<stuff_by_add>().add_button(7);
        GetComponent<AudioSource>().PlayOneShot(collid_shound);
        Destroy(gameObject, 1f);
    }

    void gravity_con(Collider col)
    {
        GameObject.Find("managers").GetComponent<stuff_by_add>().add_button(8);
        GetComponent<AudioSource>().PlayOneShot(collid_shound);
        Destroy(gameObject, 1f);
    }

    void devil_con(Collider col)
    {
        GameObject.Find("managers").GetComponent<stuff_by_add>().add_button(9);
        GetComponent<AudioSource>().PlayOneShot(collid_shound);
        Destroy(gameObject, 1f);
    }
}
