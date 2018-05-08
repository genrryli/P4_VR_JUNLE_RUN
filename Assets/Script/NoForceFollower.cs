using UnityEngine;
using System.Collections;

public class NoForceFollower : MonoBehaviour
{

    public GameObject player;
    public GameObject rotate_target;

    void Start()
    {
    
    }

    void LateUpdate()
    {
        //transform.position = player.transform.position ;
        //transform.rotation = rotate_target.transform.rotation;
    }
}