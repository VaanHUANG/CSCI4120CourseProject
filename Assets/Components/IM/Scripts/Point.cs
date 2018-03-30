using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (kill());	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator kill(){
		yield return new WaitForSeconds (5.0f);
		Destroy (gameObject);
	}
}
