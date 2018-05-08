using UnityEngine;
using System.Collections;

public class idle : MonoBehaviour {

    private Animator ani;
	// Use this for initialization
	void Start () {
        ani = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (game_manager.gm.gamestate == game_manager.game_state.start)
        {
            ani.SetBool("walking", true);
        }
	}
}
