using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {
	public GameObject particles;
	public GameObject plane;
	Collider m_Collider;

	private Quaternion planeRotation=new Quaternion(90,0,90,180);
	// Use this for initialization
	void Start () {
		m_Collider = GetComponent<Collider> ();
	}
	
	// Update is called once per frame
	void OnCollisionEnter (Collision col) {
		if (col.gameObject.layer == 12) {
			Instantiate (particles, this.transform.position,col.transform.rotation);
			m_Collider.enabled = false;
			OnBecameVisible ();
			StartCoroutine(destroy());
			if (col.gameObject.tag == "Terrain") {
				if (this.gameObject.tag == "FireBullet") {
					Instantiate (plane, this.transform.position+new Vector3(5,0,0),planeRotation);
				}
				if (this.gameObject.tag == "WaterBullet") {
					Instantiate (plane, this.transform.position,planeRotation);
				}
				if (this.gameObject.tag == "EarthBullet") {
					Instantiate (plane, this.transform.position,planeRotation);
				}
			}

			//Slime Resizing
			if (col.gameObject.tag == "Slime") {
				col.transform.localScale += new Vector3 (1, 1, 1);
			}
		}

	}
	void OnBecameVisible(){
		enabled = false;
	}
	IEnumerator destroy(){
		yield return new WaitForSeconds(5);
		Destroy (this.gameObject);
	}
}
