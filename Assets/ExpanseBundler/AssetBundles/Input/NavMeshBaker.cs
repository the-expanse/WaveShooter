using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class NavMeshBaker : MonoBehaviour {
	public NavMeshSurface[] surfaces = new NavMeshSurface[10];

	// Use this for initialization
	void Start () {
		surfaces = GameObject.FindObjectsOfType<NavMeshSurface> ();
		/*surfaces[0] = surfaceObjects[0].GetComponent<NavMeshSurface>();
		surfaces[1] = surfaceObjects[1].GetComponent<NavMeshSurface>();
		surfaces[2] = surfaceObjects[2].GetComponent<NavMeshSurface>();
		surfaces[3] = surfaceObjects[3].GetComponent<NavMeshSurface>();
		surfaces[4] = surfaceObjects[4].GetComponent<NavMeshSurface>();
		surfaces[5] = surfaceObjects[5].GetComponent<NavMeshSurface>();
		surfaces[6] = surfaceObjects[6].GetComponent<NavMeshSurface>();
		surfaces[7] = surfaceObjects[7].GetComponent<NavMeshSurface>();
		surfaces[8] = surfaceObjects[8].GetComponent<NavMeshSurface>();
		surfaces[9] = surfaceObjects[9].GetComponent<NavMeshSurface>();*/


		/*for(int i = 0; i<surfaceObjects.Length; i++){
		}*/
		StartCoroutine (waitToBake ());

	}

	IEnumerator waitToBake(){
		yield return new WaitForSeconds (3);
		for(int i = 0; i<surfaces.Length; i++){
		surfaces [i].BuildNavMesh();
		}
		yield return new WaitForSeconds (.5f);
		spawnInterval.navready = true;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
