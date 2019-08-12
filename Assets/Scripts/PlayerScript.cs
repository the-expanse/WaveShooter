using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	Ray ray;
	RaycastHit hit;

	public static bool onTommyGun = true;

	public float points = 0f;
	public float minPoints = 0f;

	public float laserAmmo = 100f;
	public Image lazerAmmoCount;
	bool isShooting = false;
	bool audioOn = true;

	GameObject lazerBeam;
	LineRenderer lazer;
	Material beamMat;
	LineRenderer beamLine;
	public AudioSource beam;

	// Use this for initialization
	void Start () {
		beamLine = GameObject.Find ("beamMat").GetComponent<LineRenderer>();
		beamMat = beamLine.material;
		lazerBeam = GameObject.Find ("beam");
		lazer = lazerBeam.AddComponent<LineRenderer> ();
		lazer.SetWidth(.3f,.3f);
		lazer.material = beamMat;
		lazerBeam.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		lazerAmmoCount.fillAmount = laserAmmo / 100;
		RaycastPointer ();
		reload ();
	}

	public void RaycastPointer() {
		if (Input.GetMouseButtonDown (0) && laserAmmo >=1) {
			beam.Play ();
		}

		if (Input.GetMouseButton (0) && laserAmmo >= 1f) {
			isShooting = true;
			laserAmmo -= 10f * Time.deltaTime;
			lazerBeam.SetActive (true);
			lazer.SetPosition (0, lazerBeam.transform.position);
			ray = Camera.main.ViewportPointToRay (new Vector3 (0.5f, .5f, 0));
			if (Physics.Raycast (ray, out hit, 1000f)) {
				lazer.SetPosition (1, hit.point);
				//Debug.Log (hit.transform.name);
				enemy script = hit.transform.root.GetComponent<enemy> ();
				lazer.SetPosition (1, hit.point);
				//Debug.Log (hit.transform.name);
				if (hit.transform.tag == "Drone" && script.health > 0f) {
					points += 10 * Time.deltaTime;
					Debug.Log ("hit");
					script.health -= 50 * Time.deltaTime;
				} 
				if (hit.transform.tag == "Soldier" && script.health > 0) {
					Debug.Log ("SHOT");
					script.health -= 50 * Time.deltaTime;
				}
			} 
		}
		else if (Input.GetMouseButtonUp (0)) {
				beam.Pause ();
				lazerBeam.SetActive (false);
		}

		if (laserAmmo <= 1) {
			reload ();
		}
	}

	IEnumerator reloadTime(){
		yield return new WaitForSeconds (2f);
		laserAmmo = 100f;
	}

	void reload(){
		if (laserAmmo <= 1) {
			StartCoroutine (reloadTime ());
			beam.Pause ();
			lazerBeam.SetActive (false);
		}
		if (Input.GetKey (KeyCode.R)) {
			StartCoroutine (reloadTime ());
		}
	}

}
