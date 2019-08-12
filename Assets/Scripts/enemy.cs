using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class enemy : MonoBehaviour {
	public float health = 1;
	public float droneHealth = 50;
	public float soldierHealth = 100;
	public float tankHealth = 250;

	NavMeshAgent enemyAgent;
	Transform player;
	public float dist;
	GameObject[] coverSpots;
	GameObject coverSpot;
	int coverDest;

	GameObject lazer;
	Rigidbody lazers;
	private Transform thisBarrel;

	bool shooting = false;
	bool inCover = false;
	Animator enemyAnimator;

	Transform enemySpawn;


	// Use this for initialization
	void Start () {
		thisBarrel = transform.GetChild (0).GetChild (1).GetChild (2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0);

		lazer = GameObject.Find("Lazer");
		lazers = lazer.GetComponent<Rigidbody>();

		if (this.transform.tag == "Drone") {
			health = droneHealth;
		}else if(this.transform.tag == "Tank"){
			health = tankHealth;
		}else if(this.transform.name == "Soldier"){
			health = soldierHealth;
		}

		enemySpawn = GameObject.Find ("enemySpawn").transform;
		enemyAnimator = this.GetComponentInChildren<Animator> ();
		coverSpots = GameObject.FindGameObjectsWithTag ("empty");
		coverDest = (Random.Range (0, 5));
		player = GameObject.Find ("FPSController").transform;
		findCover ();
		StartCoroutine (setDestination ());
	}
	void findCover(){
		enemyAgent = gameObject.GetComponentInParent<NavMeshAgent> ();
		int randSpawnNo = (Random.Range(0,coverSpots.Length));
		coverSpots[randSpawnNo].tag = "full";
		enemyAgent.destination = coverSpots[randSpawnNo].transform.position;
	}

	// Update is called once per frame
	void Update () {
		//enemyAgent.destination = coverSpot.transform.position;//coverSpots[coverDest].transform.position;
		Die();
	}

	void Die(){
		if (health <= 0f) {
			health = 100;
			transform.position = enemySpawn.position;
			enemyAnimator.SetBool ("intoCover", false);
			enemyAnimator.SetBool ("coverIdle", false);
			enemyAnimator.SetBool ("outOfCover", false);
			enemyAnimator.SetBool ("run", true);
			enemyAgent.destination = enemySpawn.transform.position;
			//coverSpot.transform.tag = "empty";
		}
	}

	IEnumerator setDestination(){
		Animator anim = transform.GetComponent<Animator> ();
		yield return new WaitForSeconds (.2f);
		dist = Vector3.Distance (transform.position, player.position);
		if (dist <= 1.7f) {
			anim.SetBool ("punch", true);
			Debug.Log ("PUNCH");
		}
		//enemyAgent.destination = coverSpot.transform.position;//coverSpots[coverDest].transform.position;
		StartCoroutine (setDestination ());
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "full") {
			inCover = true;
			coverSpot = col.transform.gameObject;
			//enemyAgent.destination = enemyAgent.transform.position;
			enemyAnimator.SetBool ("intoCover", true);
			StartCoroutine (shootInterval ());

			enemyAnimator.SetBool ("shoot", true);
			enemyAnimator.SetBool ("coverIdle", false);
			enemyAnimator.SetBool ("outOfCover", false);
			enemyAnimator.SetBool ("run", false);
			enemyAgent.transform.localRotation = col.transform.localRotation;
		}
	}

	void shootlazer(){
		GameObject go = Instantiate (lazer, thisBarrel.position, thisBarrel.rotation);
		Rigidbody clone = go.GetComponent<Rigidbody>();
		clone.velocity = transform.TransformDirection (Vector3.forward * 50);
	}

	IEnumerator shootInterval(){
		yield return new WaitForSeconds (5f);
		shooting = false;
		shootlazer ();
		yield return new WaitForSeconds (.5f);
		shootlazer ();
		yield return new WaitForSeconds (.5f);
		shootlazer ();
		yield return new WaitForSeconds (.5f);
		StartCoroutine (shootInterval ());
		//clone = Instantiate(lazers, thisBarrel.position, thisBarrel.rotation);
	}
}
