using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class spawning : MonoBehaviour {
	GameObject[] spawns;
	GameObject[] enemies;
	int pointNo;
	public int enemyNo;

	// Use this for initialization
	void Start () {
		spawns = GameObject.FindGameObjectsWithTag ("spawnPoint");
		enemies = GameObject.FindGameObjectsWithTag ("enemy");
		StartCoroutine (waitToSpawn ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator waitToSpawn(){
		if(enemyNo >= 12){
			enemyNo = 1;
		}
		yield return new WaitForSeconds (3f);
		pointNo = (Random.Range (1, 6));
		spawn ();
		StartCoroutine (waitToSpawn ());

	}

	void spawn(){
		if (enemyNo <= 12) {
			enemies [enemyNo].transform.position = spawns [pointNo].transform.position;
			enemies [enemyNo].GetComponent<NavMeshAgent> ().enabled = true;
			enemyNo++;
			//pointNo++;
		}
	}
}
