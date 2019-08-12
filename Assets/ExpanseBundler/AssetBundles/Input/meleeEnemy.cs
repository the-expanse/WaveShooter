using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class meleeEnemy : MonoBehaviour {
	GameObject player;
	NavMeshAgent enemy;
	Animator anim;
	GameObject hand;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Cockpit");
		enemy = gameObject.GetComponent<NavMeshAgent>();
		anim = enemy.transform.GetChild(0).GetComponent<Animator> ();
		StartCoroutine (waitToAttack ());
		Debug.Log(enemy.agentTypeID);
	}
	
	// Update is called once per frame
	void Update () {
		punch ();
	}

	void punch(){
		float Dist = Vector3.Distance (player.transform.position, transform.position);
		if (Dist <= 1.5f) {
			enemy.destination = enemy.transform.position;
			anim.SetBool ("punch", true);
		} else {
			
			anim.SetBool ("punch", false);
			anim.SetBool ("run", true);
		}
	}

	IEnumerator waitToAttack(){
		yield return new WaitForSeconds (1f);
		if (enemy.enabled) {
			StartCoroutine (updateDest ());
		} else {
			StartCoroutine (waitToAttack ());
		}
	}
	IEnumerator updateDest(){
		enemy.enabled = true;
		yield return new WaitForSeconds (.5f);
		Debug.Log ("TARGET CHANGE");
		enemy.destination = player.transform.position;
		yield return new WaitForSeconds (.5f);
		StartCoroutine (updateDest ());
	}

	void OnTriggerEnter(Collider col){
		if (col.name == "mine") {
			Destroy (gameObject);
			Destroy (col.gameObject);
		}
	}
}
