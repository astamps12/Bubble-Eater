using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlStCharacterAnim : MonoBehaviour {


    private Animator anim;

	void Start () {
        anim = GetComponent<Animator>();
	}
	
	
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("isSwimming", true);
        }

        else { anim.SetBool("isSwimmingRight", false);
        
        }	
	}
}
