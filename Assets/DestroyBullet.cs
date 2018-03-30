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
			Instantiate (particles, this.transform.position,col.transform.rotation);
			m_Collider.enabled = false;
			OnBecameVisible ();
			StartCoroutine(destroy());
			if (col.gameObject.tag == "Terrain") {
				if (this.gameObject.tag == "FireBullet") {
					Instantiate (plane, this.transform.position,Quaternion.Euler(new Vector3(-90, 0, 0)));
				}
				if (this.gameObject.tag == "WaterBullet") {
					Instantiate (plane, this.transform.position,Quaternion.Euler(new Vector3(-90, 0, 0)));
				}
				if (this.gameObject.tag == "EarthBullet") {
					Instantiate (plane, this.transform.position,Quaternion.Euler(new Vector3(-90, 0, 0)));
				}
			}
		}

		if (col.gameObject.layer == 8) {
			//相剋
			if (this.gameObject.tag == "WaterBullet" && col.gameObject.tag == "Fire") {
				col.gameObject.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
				if (col.gameObject.transform.localScale == new Vector3 (0, 0, 0)) {
					Destroy (col.gameObject);
				}
			}
			if (this.gameObject.tag == "FireBullet" && col.gameObject.tag == "Earth") {
				col.gameObject.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
				if (col.gameObject.transform.localScale == new Vector3 (0, 0, 0)) {
					Destroy (col.gameObject);
				}
			}
			if (this.gameObject.tag == "EarthBullet" && col.gameObject.tag == "Water") {
				col.gameObject.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
				if (col.gameObject.transform.localScale == new Vector3 (0, 0, 0)) {
					Destroy (col.gameObject);
				}
			}

			//同屬性
			if (this.gameObject.tag == "WaterBullet" && col.gameObject.tag == "Water") {
				col.gameObject.transform.localScale += new Vector3 (0.5f, 0.5f, 0.5f);
			}
			if (this.gameObject.tag == "FireBullet" && col.gameObject.tag == "Fire") {
				col.gameObject.transform.localScale += new Vector3 (0.5f, 0.5f, 0.5f);
			}
			if (this.gameObject.tag == "EarthBullet" && col.gameObject.tag == "Earth") {
				col.gameObject.transform.localScale += new Vector3 (0.5f, 0.5f, 0.5f);
			}

			//無反應
			if (this.gameObject.tag == "WaterBullet" && col.gameObject.tag == "Earth") {
				col.gameObject.transform.localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
				if (col.gameObject.transform.localScale == new Vector3 (0, 0, 0)) {
					Destroy (col.gameObject);
				}
			}
			if (this.gameObject.tag == "FireBullet" && col.gameObject.tag == "Water") {
				col.gameObject.transform.localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
				if (col.gameObject.transform.localScale == new Vector3 (0, 0, 0)) {
					Destroy (col.gameObject);
				}
			}
			if (this.gameObject.tag == "EarthBullet" && col.gameObject.tag == "Fire") {
				col.gameObject.transform.localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
				if (col.gameObject.transform.localScale == new Vector3 (0, 0, 0)) {
					Destroy (col.gameObject);
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
