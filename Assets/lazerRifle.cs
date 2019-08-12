using System.Collections;
using System.Collections.Generic;
using UnityEngine.VR;
using UnityEngine;

public class lazerRifle : MonoBehaviour {
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
		if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) == 1f){
			lazerBeam.SetActive (true);

		}
	}
}
