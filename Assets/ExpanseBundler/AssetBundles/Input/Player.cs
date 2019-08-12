using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public int health = 100;
	AudioSource deathSound;
	bool alive;
	GameObject enemyhand;
	GameObject sword;
	GameObject playerHand;

	// Use this for initialization
	void Start () {
		deathSound = GameObject.Find ("deathSFX").GetComponent<AudioSource> ();
		//playerHand = GameObject.Find("RightHandAnchor");
		sword = GameObject.Find ("Sword");
	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0){
			health = 0;
			deathSound.enabled = true;
			alive = false;
		}
//		sword.transform.position = playerHand.transform.position;
//		sword.transform.rotation = playerHand.transform.rotation;
	}

	void OnTriggerEnter(Collider col){
		if(col.name == "enemyHand"){
			health -= 25;
			enemyhand = col.transform.gameObject;
			enemyhand.SetActive (false);
			StartCoroutine (enemyAttackCooldown ());
		}
	}

	IEnumerator enemyAttackCooldown(){
		yield return new WaitForSeconds (2f);
		enemyhand.SetActive (true);
	}
}
