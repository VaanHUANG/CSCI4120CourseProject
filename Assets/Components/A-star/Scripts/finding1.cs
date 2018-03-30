using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finding1 : MonoBehaviour {

	public Transform goal;
	private UnityEngine.AI.NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		agent.destination = goal.position;
		for (int i = 0; i < agent.path.corners.Length - 1; i++) {
			Debug.DrawLine (agent.path.corners [i], agent.path.corners [i + 1], Color.red);
		}
	}
}
