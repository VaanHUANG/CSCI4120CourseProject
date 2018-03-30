using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	Grid grid;
	GameObject player;
	LayerMask playerMask;
	LayerMask obstacle;
	int enemyCount;

	public Transform enemyGroup;
	public Transform enemyObject;
	public Transform spawnPts;
	public float playerCircle = 0.1f;
	public float enemyCircle = 5.0f;
	public float viewRadius = 20.0f;

	public int maxEnemies = 100;

	void Awake(){
		grid = GetComponent<Grid> ();
	}

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		playerMask = LayerMask.GetMask ("Player");
		obstacle = LayerMask.GetMask("Obstacle");
		foreach (Transform enemy in enemyGroup) {
			StartCoroutine (enemyAI (enemy));
		}
		StartCoroutine (spawnEnemy());
	}

	// Update is called once per frame
	void Update () {
		grid.clearPower ();
		enemyCount = enemyGroup.childCount;
		//Debug.Log (enemyCount);
		foreach (Transform enemy in enemyGroup) {
			UnityEngine.AI.NavMeshAgent agent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent> ();
			grid.calPower (enemy.position);

			// DEBUG
			for (int i = 0; i < agent.path.corners.Length - 1; i++) {
				Debug.DrawLine (agent.path.corners [i], agent.path.corners [i + 1], Color.red);
			}
		}
	}
		
	Vector3 randomAroundPlayer(){
		Vector2 newPosition = Random.insideUnitCircle * playerCircle;
		return player.transform.position + new Vector3(newPosition.x,0,newPosition.y);
	}

	Vector3 randomPosition(Vector3 position, float radius){
		Vector2 newPosition = Random.insideUnitSphere * radius;
		return position + new Vector3(newPosition.x,0,newPosition.y);
	}

    Vector3 balancePower(Vector3 enemyPosition) {
        Vector3 ranPosition = grid.NodeFromWorldPoint(randomPosition(enemyPosition, enemyCircle)).worldPosition;
        if (grid.getPositionPower (ranPosition) <= 4.0f) {
			return ranPosition;
		} else {
			float lenght = float.MaxValue;
			int i = 0;
			int j = 0;
			foreach (Transform point in spawnPts) {
				Vector3 vecToPlayer = enemyPosition - point.position;
				if (vecToPlayer.magnitude <= lenght) {
					lenght = vecToPlayer.magnitude;
					j = i;
				}
				i++;
			}
			return spawnPts.GetChild (j).position;
		}
    }
		
	bool playerFound(Transform enemy)
	{
		Collider[] playerInViewRadius = Physics.OverlapSphere(enemy.position, viewRadius, playerMask);
		for (int i =0; i<playerInViewRadius.Length; i++)
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
	void stateOne(Transform self) {
		self.GetComponent<UnityEngine.AI.NavMeshAgent> ().destination = balancePower(self.position);
	}

	// Attrack
	void stateTwo(Transform self) {
		self.GetComponent<UnityEngine.AI.NavMeshAgent> ().destination = player.transform.position;
	}
		
	IEnumerator enemyAI(Transform enemy){
		while (enemy != null) {
			
			if (playerFound(enemy))
			{
                stateTwo(enemy);
			}
			else
			{
				stateOne (enemy);

			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	IEnumerator spawnEnemy() {
		yield return new WaitForSeconds(2.0f);
		while (true) {
			yield return new WaitForSeconds (0.01f);
			if (enemyCount != maxEnemies) {
				List<int> xy = grid.ramdonNode ();
				if (xy [0] != -1 && xy [1] != -1) {
					int val = Mathf.RoundToInt (Random.Range (0.0f, spawnPts.childCount-1));
					Vector3 newPosition = spawnPts.GetChild (val).position;
                    Debug.Log(newPosition);
					Transform newEnemy = Instantiate (enemyObject, newPosition, Quaternion.Euler(new Vector3(0, 0 , 0)));
					newEnemy.transform.parent = enemyGroup;
					StartCoroutine (enemyAI (newEnemy));
				}
			}
		}
	}
}
