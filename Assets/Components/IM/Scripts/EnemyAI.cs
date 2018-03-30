using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public int lifePoint = 20;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision otherObj)
	{
		if (otherObj.collider.tag == "Poison")
		{
			lifePoint -= 10;
			if (lifePoint <= 0)
			{
				Destroy(gameObject, 0.5F);
			}
		}
	}
}
