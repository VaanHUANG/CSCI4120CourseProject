using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAINew : MonoBehaviour {

    GridNew grid;
    LayerMask playerMask;
    LayerMask obstacle;
    GameObject player;
    int state;

    public float viewRadius = 20.0f;

    void Awake()
    {
        grid = GameObject.Find("Grid").GetComponent<GridNew>();
    }

    // Use this for initialization
    void Start () {
        state = 0;
        player = GameObject.FindWithTag("Player");
        playerMask = LayerMask.GetMask("Player");
        obstacle = LayerMask.GetMask("Obstacle");
        StartCoroutine(enemyAI());
	}
	
	// Update is called once per frame
	void Update () {
        if (playerFound(transform))
        {
            state = 1;
        }
        else
        {
            state = 0;
        }
        grid.updatePower(transform.position);
    }

    bool playerFound(Transform enemy)
    {
        Collider[] playerInViewRadius = Physics.OverlapSphere(enemy.position, viewRadius, playerMask);
        for (int i = 0; i < playerInViewRadius.Length; i++)
        {
            Transform playerObject = playerInViewRadius[i].transform;
            Vector3 vecToPlayer = (playerObject.position - transform.position).normalized;
            if (!Physics.Raycast(transform.position, vecToPlayer, Vector3.Distance(transform.position, playerObject.position), obstacle))
            {
                return true;
            }
        }
        return false;
    }

    // Wandering
    void stateOne()
    {
        //transform.GetComponent<UnityEngine.AI.NavMeshAgent> ().destination = balancePower(self.position);
    }

    // Attrack
    void stateTwo()
    {
        transform.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = player.transform.position;
    }

    IEnumerator enemyAI()
    {
        while (true)
        {
            if (state == 1)
            {
                stateTwo();
            }
            else if (state == 0)
            {
                stateOne();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
