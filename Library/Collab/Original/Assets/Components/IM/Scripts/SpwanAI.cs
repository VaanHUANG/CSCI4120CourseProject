using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanAI : MonoBehaviour {

	Grid grid;
	int gridSizeX, gridSizeY;
	Vector2 gridWorldSize;
	float nodeRadius;
	float nodeDiameter;

	public float checkMapPower = 9.0f;
	public Transform pointObject;

	void Awake(){
		grid = GameObject.Find("Grid").GetComponent<Grid> ();
	}

	// Use this for initialization
	void Start () {
		gridWorldSize = grid.gridWorldSize;
		nodeRadius = grid.nodeRadius;
		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);

		StartCoroutine (checkMap ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void createPoint(){
		for (int i = 0; i < gridSizeX; i++) {
			for (int j = 0; j < gridSizeY; j++) {
				Transform newPoint = Instantiate (pointObject, grid.getWorldPosition (i, j), transform.rotation);
				newPoint.transform.parent = transform;
			}
		}
	}

	IEnumerator checkMap(){
		yield return new WaitForSeconds (1.0f);			
		createPoint ();
		while (true) {
			yield return new WaitForSeconds (5.0f);
			for (int i = 0; i < gridSizeX; i++) {
				for (int j = 0; j < gridSizeY; j++) {
					float power = grid.getNodePower (i, j);
					if (power <= checkMapPower && grid.nodeWalkable(i, j)) {
						Transform newPoint = Instantiate (pointObject, grid.getWorldPosition (i, j), transform.rotation);
						newPoint.transform.parent = transform;
					}
				}
			}
		}
	}
}
