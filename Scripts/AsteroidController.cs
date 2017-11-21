using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {

	public AudioClip destroy;
	public GameObject smallAsteroid;
	 
	private GameController gameController;

	//Custom Collision
	private GameObject[] bulletList;
	private GameObject[] yingList;
	private GameObject[] yangList;
	private float bulletRad;
	private float yingRad;
	private float yangRad;
	// Use this for initialization
	void Start () {
		 
		// Get a reference to the game controller object and the script
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		gameController = gameControllerObject.GetComponent <GameController>();
		 
		// Push the asteroid in the direction it is facing
		GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(-50.0f, 150.0f));
		// Give a random angular velocity/rotation
		GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-0.0f, 90.0f);

		//Custom Collision
		yangRad = .7f;
		yingRad = 1.3f;
		bulletRad = .4f;
	}

	void FixedUpdate() {
		bulletList = GameObject.FindGameObjectsWithTag ("Bullet");
		yingList = GameObject.FindGameObjectsWithTag ("LargeAsteroid");
		yangList = GameObject.FindGameObjectsWithTag ("SmallAsteroid");

		/*
		CircleCollisionYing (bulletList, yingList);
		CircleCollisionYang (bulletList, yangList);
		*/

	}

	/*
	void CircleCollisionYing(GameObject[] bullet, GameObject[] list) {
		foreach (GameObject o in list) {
			float o2x = o.transform.position.x;
			float o2y = o.transform.position.y;
			foreach (GameObject b in bullet) {
				float o1x = b.transform.position.x;
				float o1y = b.transform.position.y;

				float distance = Mathf.Sqrt (((o1x - o2x) * (o1x - o2x)) + ((o1y - o2y) * (o1y - o2y)));

				bool col = distance < bulletRad + yingRad;

				if (col == true) {
					
					col = false;
					Destroy (b);

					Instantiate (smallAsteroid, 
						new Vector3 (o.transform.position.x - .5f, 
							o.transform.position.y - .5f, 0), Quaternion.Euler (0, 0, 90));


					// Spawn small asteroids
					Instantiate (smallAsteroid, 
						new Vector3 (o.transform.position.x + .5f, 
							o.transform.position.y + .0f, 0), Quaternion.Euler (0, 0, 0));


					// Spawn small asteroids
					Instantiate (smallAsteroid,
						new Vector3 (o.transform.position.x + .5f,
							o.transform.position.y - .5f, 0), Quaternion.Euler (0, 0, 270));
					
					Destroy(o);
					// Add to the score
					gameController.IncrementScore20();

					gameController.SplitAsteroid (); // +2
					//Debug.Log(gameController.asteroidsRemaining);
					AudioSource.PlayClipAtPoint(destroy, Camera.main.transform.position);


				}
			}
		}
	}

	void CircleCollisionYang(GameObject[] bullet, GameObject[] list) {
		foreach (GameObject o in list) {
			float o2x = o.transform.position.x;
			float o2y = o.transform.position.y;
			foreach (GameObject b in bullet) {
				float o1x = b.transform.position.x;
				float o1y = b.transform.position.y;

				float distance = Mathf.Sqrt (((o1x - o2x) * (o1x - o2x)) + ((o1y - o2y) * (o1y - o2y)));
				bool col = distance < bulletRad + yangRad;

				if (col == true) {
					col = false;
					Destroy (b);
					Destroy(o);
					// Add to the score
					gameController.IncrementScore50();

					gameController.DecrementAsteroids (); // +2

					AudioSource.PlayClipAtPoint(destroy, Camera.main.transform.position);
				}
			}
		}
	}
	*/
	




    //This Collision uses unity's rigid body system
	void OnCollisionEnter2D(Collision2D c){        
		if (c.gameObject.tag.Equals("Bullet")) {
			            
			// Destroy the bullet       
			Destroy (c.gameObject);

			// If large asteroid spawn new ones
			if (tag.Equals ("LargeAsteroid")) {
				// Spawn small asteroids
				Instantiate (smallAsteroid, 
					new Vector3 (transform.position.x - .5f, 
						transform.position.y - .5f, 0), Quaternion.Euler (0, 0, 90));
				// Spawn small asteroids
				Instantiate (smallAsteroid, 
					new Vector3 (transform.position.x + .5f, 
						transform.position.y + .0f, 0), Quaternion.Euler (0, 0, 0));
				// Spawn small asteroids
				Instantiate (smallAsteroid,
					new Vector3 (transform.position.x + .5f,
						transform.position.y - .5f, 0), Quaternion.Euler (0, 0, 270));
								// Add to the score
				gameController.IncrementScore20();

				gameController.SplitAsteroid (); // +2
				 
			} else {
				// Just a small asteroid destroyed
				gameController.DecrementAsteroids();
				// Add to the score
				gameController.IncrementScore50();
			}
			// Play a sound
			AudioSource.PlayClipAtPoint(destroy, Camera.main.transform.position);

			// Destroy the current asteroid
			Destroy (gameObject);
			 
		}
		 
	}

}