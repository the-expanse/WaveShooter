using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Ui : MonoBehaviour {
	Text upgradeOne;
	Text upgradeTwo;
	Text upgradeThree;

	public Button select1;
	Button select2;
	Button select3;

	// Pistol Upgrades //
	string pistolUpg1 = "Extended Mag";
	string pistolUpg2 = "High Velocity Rounds";
	string pistolUpg3 = "Explosive Rounds";

	// Shotgun Upgrades //
	string shotgunUpg1 = "Extended Mag";
	string shotgunUpg2 = "Drum Mag";
	string shotgunUpg3 = "Flame Rounds";

	// AR Upgrades //
	string arUpg1 = "Extended Mag";
	string arUpg2 = "Drum Mag";
	string arUpg3 = "Grenade Launcher";


	// Use this for initialization
	void Start () {
		upgradeOne = GameObject.Find ("upgradeOne").GetComponent<Text>();
		upgradeTwo = GameObject.Find ("upgradeTwo").GetComponent<Text>();
		upgradeThree = GameObject.Find ("upgradeThree").GetComponent<Text>();

		select1 = GameObject.Find ("select1").GetComponent<Button> ();
		select2 = GameObject.Find ("select2").GetComponent<Button> ();
		select3 = GameObject.Find ("select2").GetComponent<Button> ();

	}
	
	// Update is called once per frame
	void Update () {
	}

	public void selectPistol(){
		Debug.Log ("PISTOL");
		upgradeOne.text = pistolUpg1.ToString ();
		upgradeTwo.text = pistolUpg2.ToString ();
		upgradeThree.text = pistolUpg3.ToString ();
	}

	void selectShotgun(){
		upgradeOne.text = shotgunUpg1.ToString ();
		upgradeTwo.text = shotgunUpg2.ToString ();
		upgradeThree.text = shotgunUpg3.ToString ();
	}

	void selectAR(){
		upgradeOne.text = arUpg1.ToString ();
		upgradeTwo.text = arUpg2.ToString ();
		upgradeThree.text = arUpg3.ToString ();
	}
}
