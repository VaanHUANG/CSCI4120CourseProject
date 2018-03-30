using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {

	public float forwardRate = 3.0F;
	public float turnRate = 2.0F;
	public GameObject wall;
	public GameObject poison;
	public Transform poisonGroup;

	UnityEngine.AI.NavMeshObstacle obstacle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float forwardAmount = Input.GetAxis("Vertical") * forwardRate;
		float turnForce = Input.GetAxis("Horizontal") * turnRate;
		transform.Rotate(0, turnForce, 0);
		transform.position += transform.forward * forwardAmount * Time.deltaTime;

		if (Input.GetButtonDown ("Fire2")) {
			GameObject newPoison = Instantiate (poison, transform.position, transform.rotation);
			obstacle = newPoison.GetComponent<UnityEngine.AI.NavMeshObstacle> ();
			newPoison.transform.parent = poisonGroup;
			bool walkable = GetRandomValue ();
			obstacle.enabled = walkable;
			StartCoroutine (destroyPoison (newPoison));
		}

	}

	bool GetRandomValue() {
		float rand = Random.value;
		if (rand <= .5f)
			return false;
		if (rand <= .8f)
			return true;

		return true;
	}

	IEnumerator destroyPoison(GameObject poison){
		yield return new WaitForSeconds (5.0f);
		Destroy (poison);
	}
}
