using UnityEngine;
using System.Collections;

public class lookat : MonoBehaviour {

    public Transform target;
    void Update()
    {
        if (target == null) { target = GameObject.FindWithTag("target").GetComponent<Transform>(); }
        transform.LookAt(target);
    }
}
