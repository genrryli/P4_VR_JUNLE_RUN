using UnityEngine;
using System.Collections;

public class oil : MonoBehaviour {

    private float timer=0;
    public AudioClip shound;

    void Start()
    {
        AudioSource.PlayClipAtPoint(shound, transform.position);
    }

    void Update()
    {
        timer += Time.deltaTime;              
    }

    void OnTriggerEnter(Collider Trigger)
    {
        
        if (Trigger.tag == "Player"&&timer>=0.2)
        {
            Trigger.transform.FindChild("Weapon").FindChild("oil_suffer").gameObject.SetActive(true);         
        }
        if (Trigger.tag == "com" && timer >= 0.2)
        {
            Trigger.transform.FindChild("Weapon").FindChild("oil_suffer").gameObject.SetActive(true);
        }
    }
}
