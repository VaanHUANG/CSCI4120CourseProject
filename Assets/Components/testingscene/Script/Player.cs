using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	[SerializeField]
	private Stat health;
	[SerializeField]
	private Stat ammo;
	[SerializeField]
	private Image water;
	[SerializeField]
	private Image fire;
	[SerializeField]
	private Image earth;

	public GameObject particles;

	Bar healthBar;
			// Use this for initialization
	private void Awake(){
		health.Initialize();
		ammo.Initialize();
	}
	void Start(){
		water.color = new Color(0.392f,0.392f,0.392f,1);
        fire.color = new Color(1,1,1,1);
		earth.color = new Color(0.392f,0.392f,0.392f,1);
		ammo.CurrentVal =5;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Z)){
			health.CurrentVal -= 10;
		}
		if (Input.GetKeyDown(KeyCode.X)){
			health.CurrentVal += 10;
		}
		GameObject flaregun = GameObject.Find("flaregun");
        flaregun gunscript = flaregun.GetComponent<flaregun>();
        ammo.CurrentVal = gunscript.currentRound;
		if (Input.GetKeyDown(KeyCode.B)){
			ammo.CurrentVal -= 1;
		}
		if (Input.GetKeyDown(KeyCode.R)){
			ammo.CurrentVal =5;
		}
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			water.color = new Color(0.392f,0.392f,0.392f,1);
        	fire.color = new Color(1,1,1,1);
			earth.color = new Color(0.392f,0.392f,0.392f,1);
			ammo.CurrentVal =5;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)){
			water.color = new Color(1,1,1,1);
        	fire.color = new Color(0.392f,0.392f,0.392f,1);
			earth.color = new Color(0.392f,0.392f,0.392f,1);
			ammo.CurrentVal =5;
		

		}
		if (Input.GetKeyDown(KeyCode.Alpha3)){
			water.color = new Color(0.392f,0.392f,0.392f,1);
        	fire.color = new Color(0.392f,0.392f,0.392f,1);
			earth.color = new Color(1,1,1,1);
			ammo.CurrentVal =5;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		Debug.Log ("FUCK");
		if (col.gameObject.tag=="Fire"||col.gameObject.tag=="Water"||col.gameObject.tag=="Earth") {
			Instantiate (particles, this.gameObject.transform);
			health.CurrentVal -= 2;
		}
	}
}
