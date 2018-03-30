using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNew : MonoBehaviour
{

    public Transform player;

    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    NodeGrid[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Start()
    {
        // Setup of the grid
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    /***** Helper Functions *****/
    public void updatePower(Vector3 worldPosition)
    {
        NodeGrid NodeGrid = NodeFromWorldPoint(worldPosition);
        NodeGrid.power += 1.0f;
        NodeGrid.hasEnemies = true;
    }

    public NodeGrid NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x - transform.position.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z - transform.position.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        Debug.Log(percentX +", "+ percentY);
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    // INIT GRID	
    void CreateGrid()
    {
        grid = new NodeGrid[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new NodeGrid(walkable, worldPoint);
            }
        }
    }

    // DRAW GRID
    void OnDrawGizmos()
    {
        Debug.DrawLine(player.position, player.position + player.forward * 20.0f, Color.green);
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (grid != null)
        {
            foreach (NodeGrid n in grid)
            {
                NodeGrid playerNode = NodeFromWorldPoint(player.position);
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                if (n.hasEnemies)
                {
                    Gizmos.color = Color.green;
                }
                else if (n.power > 0)
                {
                    Gizmos.color = Color.blue;
                }
                Gizmos.DrawCube(n.worldPosition, new Vector3(nodeDiameter, 0.1f, nodeDiameter));
            }
        }
    }
}

public class NodeGrid
{

    public bool walkable;
    public Vector3 worldPosition;
    public float power;
    public bool hasEnemies;

    public NodeGrid(bool _walkable, Vector3 _worldPos)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        power = 0.0f;
        hasEnemies = false;
    }
}

