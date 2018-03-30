using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_slime_animation : MonoBehaviour {

	private Animator anim;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("up")) {
			anim.SetBool ("isJump", true);
		} else {
			anim.SetBool ("isJump", false);
		}
	}
}
