using UnityEngine;
using System.Collections;

public class ericDronnerAI : MonoBehaviour
{
	//CONSTANTS
	float SEARCH_RADIUS = 5.0f;
	public float VISION_LENGTH = 50.0f;
	public float FIRE_TIME = 1.0f;
	public float BULLET_LIFETIME = 10.0f;

	//GAMEOBJECTS
	public GameObject playerRef;
	public GameObject droneBullet;
	public GameObject droneBulletSpawn;

	//VECTORS
	Vector3 DRONE_FACING;
	Vector3 playerPosition;

	//NAVMESH
	public UnityEngine.AI.NavMeshAgent drone_agent;

	//STATE FLAGS
	bool startSearch;
	bool turret;

	/*
	*	Initializes global variables
	*	Tells the drone to shoot when it sees the player
	*/
	void Start(){
		drone_agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		startSearch = true;
		playerPosition = playerRef.transform.position;
		turret = false;
		InvokeRepeating("shoot", FIRE_TIME, FIRE_TIME);
	}

	/*
	*	Determines state of drone
	*	In turret mode, stops moving, clears path, looks at player and checks to see if it can still see him
	*	In search mode, tries to find the player based on some initial position, checks to see if it can see the player	
	*/
	void Update(){
		DRONE_FACING = transform.TransformDirection(Vector3.forward);

		if (turret){
			drone_agent.Stop(true);
			drone_agent.ResetPath();
			transform.LookAt(playerRef.transform.position); 
			turret = vision();
		}

		else{
			drone_agent.Resume();
			if (startSearch){
				playerPosition = playerRef.transform.position;
				drone_agent.SetDestination(playerPosition);
				startSearch = false;
			}
			else if (withinRange(playerPosition, SEARCH_RADIUS)){        
				startSearch = true;
			}else if(drone_agent.velocity.Equals(new Vector3(0,0,0))){
				startSearch = true;
			}

			turret = vision();
		}
	}

	/*
	*	detects if the drone is within radius distance from queryPosition
	*/
	bool withinRange(Vector3 queryPosition, float radius){
		Vector3 dronePosition = transform.position;
		float drone_x, drone_y, drone_z;
		float player_x, player_y, player_z;

		drone_x = dronePosition.x;
		drone_y = dronePosition.y;
		drone_z = dronePosition.z;

		player_x = queryPosition.x;
		player_y = queryPosition.y;
		player_z = queryPosition.z;

		bool x_range, y_range, z_range;
		x_range = player_x - radius <= drone_x && drone_x <= player_x + radius;
		y_range = player_y - radius <= drone_y && drone_y <= player_y + radius;
		z_range = player_z - radius <= drone_z && drone_z <= player_z + radius;


		if(x_range && y_range && z_range){
			return true;
		}else{
			return false;
		}

	}

	/*
	*	vision method returns true if drone detects player, false if it does not
	*	uses raycast to serve as drone's "sight"
	*	hitInfo receives data once the raycast collides with something
	*/
	bool vision(){
		RaycastHit hitInfo;
		if (Physics.Raycast(transform.position, DRONE_FACING, out hitInfo, VISION_LENGTH)){
			if (hitInfo.collider.CompareTag("Player")){
				return true;
			}
			else{
				return false;
			}
		}
		else{
			return false;
		}
	}

	/*
	*	create a bullet, fire it and destroy it after a time
	*	only done if drone is in turret mode
	*/
	void shoot()
	{
		if (turret){
			var droneBulletInstance = Instantiate(droneBullet, droneBulletSpawn.transform.position, droneBulletSpawn.transform.rotation) as GameObject;
			droneBulletInstance.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 500);
			Destroy(droneBulletInstance, BULLET_LIFETIME);
		}
	}
}
