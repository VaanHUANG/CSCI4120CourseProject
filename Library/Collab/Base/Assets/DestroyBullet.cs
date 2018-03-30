using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {
	public GameObject particles;
	public GameObject plane;
	Collider m_Collider;
	// Use this for initialization
	void Start () {
		m_Collider = GetComponent<Collider> ();
	}
	
	// Update is called once per frame
	void OnCollisionEnter (Collision col) {
		if (col.gameObject.layer == 12) {
			Instantiate (particles, col.transform.position,col.transform.rotation);
			m_Collider.enabled = false;
			OnBecameVisible ();
			StartCoroutine(destroy());
			if (col.gameObject.tag == "Terrain") {
				if (this.gameObject.tag == "FireBullet") {
					Instantiate (plane, col.transform.position,col.transform.rotation);
				}
				if (this.gameObject.tag == "WaterBullet") {
					Instantiate (plane, col.transform.position,col.transform.rotation);
				}
				if (this.gameObject.tag == "EarthBullet") {
					Instantiate (plane, col.transform.position,col.transform.rotation);
				}
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
