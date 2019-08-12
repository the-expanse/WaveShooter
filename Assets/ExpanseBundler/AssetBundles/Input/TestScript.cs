using UnityEngine;
using UnityEngine.AI;

public class TestScript : ExpanseBehaviour {
	Transform enemy;
	GameObject enemySpawn;
	//GameObject[] navObjects = new GameObject[1];
	GameObject navObj;

	// Use this for initialization
	void Start () {
		navObj = GameObject.Find("Test/Area_01/floor");
		navObj.AddComponent<NavMeshSurface> ();
		navObj.AddComponent<NavMeshLink> ();
		/*navObjects[0] = GameObject.Find("Test/Area_01/floor");
		navObjects[0].AddComponent<NavMeshSurface> ();
		navObjects[0].AddComponent<NavMeshLink> ();
		for(int i = 0; i<navObjects.Length; i++){
			navObjects [i].AddComponent<NavMeshSurface> ();
			navObjects [i].AddComponent<NavMeshLink> ();
		}*/
		Player playerScript = GameObject.Find ("Cockpit").AddComponent<Player> ();
		spawnInterval spawnScript = GameObject.Find ("Test/spawn1").AddComponent<spawnInterval> ();
		meleeEnemy enemyScript = transform.Find ("Test/SoldierEnemy").gameObject.AddComponent<meleeEnemy> ();
		NavMeshBaker baker = GameObject.Find ("Test").AddComponent<NavMeshBaker> ();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
