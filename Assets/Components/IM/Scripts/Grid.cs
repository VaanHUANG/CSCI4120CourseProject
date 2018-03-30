using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	public Transform player;

	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	Node[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

    void Awake()
    {

    }

	void Start () {
		// Setup of the grid
		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
		CreateGrid();
	}

	/***** Helper Functions *****/
	public void calPower(Vector3 worldPosition) {
		Node node = NodeFromWorldPoint (worldPosition);
		node.power += 1.0f;
		node.hasEnemies = true;
	}

	public Node predictPlayer(Vector3 worldPosition) {
		Node node = NodeFromWorldPoint (worldPosition);
		node.power -= 10;
		return node;
	}

	public void clearPower(){
		foreach (Node n in grid) {
			n.power = 0.0f;
			n.hasEnemies = false;
		}
	}

	public Vector3 getWorldPosition(int x, int y){
		return grid [x, y].worldPosition;
	}

    public Vector3 ramdonNode1(Vector3 worldPosition)
    {
        int x = Mathf.RoundToInt(Random.Range(-1, 1));
        int y = Mathf.RoundToInt(Random.Range(-1, 1));
        List<int> temp = XYFromWorldPoint(worldPosition);
        int i = temp[0];
        int j = temp[1];
        //if (x + i >= 0 && x + i < gridSizeX && y + j >= 0 && y + j < gridSizeY)
            //return getWorldPosition(i+x, j+y);
        return worldPosition;
    }


    public List<int> ramdonNode(){
		int x = -1;
		int y = -1;
		int trial = 2;
		while (trial > 0) {
			x = Mathf.RoundToInt (Random.Range (0.0f, gridSizeX - 1.0f));
			y = Mathf.RoundToInt (Random.Range (0.0f, gridSizeY - 1.0f));
			float power = 0.0f;

			for (int i = -2; i < 3; i++) {
				for (int j = -2; j < 3; j++) {
					if (x + i >= 0 && x + i < gridSizeX && y + j >= 0 && y + j < gridSizeY) {
						power += grid [x + i, y + j].power;
					}
				}
			}
			if (power <= 4.0f) {
				break;
			} else {
				x = -1;
				y = -1;
			}
			trial -= 1;
		}
		List<int> xy = new List<int> ();
		xy.Add (x);
		xy.Add (y);
		return xy;
	}

	public float getNodePower(int x, int y){
		float power = 0.0f;
		for (int i = -2; i < 3; i++) {
			for (int j = -2; j < 3; j++) {
				if (x + i > 0 && x + i < gridSizeX && y + j > 0 && y + j < gridSizeY) {
					power += grid [x + i, y + j].power;
				}
			}
		}
		return power;
	}

	public float getPositionPower(Vector3 position) {
		List<int> xy = XYFromWorldPoint (position);
		float power = 0.0f;
		int x = xy [0];
		int y = xy [1];
		for (int i = -2; i < 3; i++) {
			for (int j = -2; j < 3; j++) {
				if (x + i > 0 && x + i < gridSizeX && y + j > 0 && y + j < gridSizeY) {
					power += grid [x + i, y + j].power;
				}
			}
		}
		return power;
	}

	public Node NodeFromWorldPoint(Vector3 worldPosition) {
		float percentX = (worldPosition.x - transform.position.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.z - transform.position.z + gridWorldSize.y / 2) / gridWorldSize.y;
		percentX = Mathf.Clamp01 (percentX);
		percentY = Mathf.Clamp01 (percentY);
		int x = Mathf.RoundToInt ((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt ((gridSizeY - 1) * percentY);
		return grid [x, y];
	}

	public List<int> XYFromWorldPoint(Vector3 worldPosition) {
		float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
		percentX = Mathf.Clamp01 (percentX);
		percentY = Mathf.Clamp01 (percentY);
		int x = Mathf.RoundToInt ((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt ((gridSizeY - 1) * percentY);
		List<int> xy = new List<int> ();
		xy.Add (x);
		xy.Add (y);
		return xy;
	}

    public bool nodeWalkable(int x, int y)
    {
        return grid[x,y].walkable;
    }
		
	// INIT GRID	
	void CreateGrid() {
		grid = new Node[gridSizeX,gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2;

		for (int x = 0; x < gridSizeX; x ++) {
			for (int y = 0; y < gridSizeY; y ++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint,nodeRadius,unwalkableMask));
                grid[x,y] = new Node(walkable,worldPoint);
			}
		}
	}

	// DRAW GRID
	void OnDrawGizmos() {
		Debug.DrawLine(player.position, player.position + player.forward * 20.0f, Color.green);
		Gizmos.DrawWireCube(transform.position,new Vector3(gridWorldSize.x,1,gridWorldSize.y));
		if (grid != null) {
			foreach (Node n in grid) {
				Node playerNode = NodeFromWorldPoint (player.position);
				Gizmos.color = (n.walkable)?Color.white:Color.red;
				if (n.hasEnemies) {
                    Gizmos.color = Color.green;
				} else if (n.power <= -5.0f) {
					Gizmos.color = Color.blue;
				}
				Gizmos.DrawCube(n.worldPosition, new Vector3(nodeDiameter,0.1f,nodeDiameter));
			}
		}
	}
}

public class Node {

	public bool walkable;
	public Vector3 worldPosition;
	public float power;
	public bool hasEnemies;

	public Node(bool _walkable, Vector3 _worldPos) {
		walkable = _walkable;
		worldPosition = _worldPos;
		power = 0.0f;
		hasEnemies = false;
	}
}
