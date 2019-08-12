using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class spawnInterval : MonoBehaviour {
	float spawnIntervals = 2f;
	GameObject enemy;
	Transform enemySpawn;
	GameObject spawnPosition1;
	GameObject spawnPosition2;
	NavMeshAgent agent;

	bool notSpawned = true;
	public static bool navready = false;

	int numberOfEnemies = 0;

	// Use this for initialization
	void Start () {
		spawnPosition1 = GameObject.Find("spawn1");
		spawnPosition2 = GameObject.Find("spawn2");
		int rand = (Random.Range (0, 1));
		enemy = GameObject.Find ("SoldierEnemy");
		agent = enemy.GetComponent<NavMeshAgent>();
	}

	IEnumerator spawnEnemy(){
		if (numberOfEnemies <= 5) {
			yield return new WaitForSeconds (spawnIntervals);
			int rand = (Random.Range (0, 1));
			numberOfEnemies += 2;
			GameObject enemyClone = Instantiate (enemy, spawnPosition1.transform.position, spawnPosition1.transform.rotation);
			GameObject enemyClone2 = Instantiate (enemy, spawnPosition2.transform.position, spawnPosition2.transform.rotation);
			yield return new WaitForSeconds (spawnIntervals);
			StartCoroutine (spawnEnemy ());		}
	}

	// Update is called once per frame
	void Update () {
		if (navready & notSpawned) {
			notSpawned = false;
			agent.enabled = true;
			StartCoroutine (spawnEnemy ());
			GameObject enemyClone = Instantiate (enemy, spawnPosition1.transform.position, spawnPosition1.transform.rotation);
			GameObject enemyClone1 = Instantiate (enemy, spawnPosition2.transform.position, spawnPosition2.transform.rotation);
		}
	}
}
