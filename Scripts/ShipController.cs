using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

	//float rotationSpeed = 100.0f;
	//float thrustForce = 3f;


	public AudioClip crash;
	public AudioClip shoot;
	public GameObject bullet;
	//public GameObject asteroid;
	private GameController gameController;

	// Custom Collision
	//public GameObject ying;
	//public GameObject yang;
	private GameObject[] yingList;
	private GameObject[] yangList;
	private float reimuRad;
	private float yingRad;
	private float yangRad;



	//CustomMovement
	public Vector3 vehiclePosition;
	public Vector3 velocity;
	public Vector3 direction;
	public Vector3 acceleration;		
	public float accelRate;
	public float maxSpeed;
	public float anglePerFrame;
	public float totalRotation;


	void Start(){
		// Get a reference to the game controller object and the script
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		gameController = gameControllerObject.GetComponent <GameController>();
		reimuRad = gameObject.GetComponent<SpriteRenderer> ().bounds.extents.x;
		yangRad = .7f;
		yingRad = 1.3f;
		//Movement
		vehiclePosition = transform.position;

	}

	void Update() {
		GenMovement ();
		yingList = GameObject.FindGameObjectsWithTag ("LargeAsteroid");
		yangList = GameObject.FindGameObjectsWithTag ("SmallAsteroid");
        //Using circle collision
		CircleCollisionYing (yingList);
		CircleCollisionYang (yangList);
	}

	/*
	void FixedUpdate () {
		 // Rotate the ship if necessary
		transform.Rotate(0, 0, -Input.GetAxis("Horizontal")*
		rotationSpeed * Time.deltaTime);
		 
		// Thrust the ship if necessary
		GetComponent<Rigidbody2D>().AddForce(transform.up * thrustForce * Input.GetAxis("Vertical"));
		 
		// Has a bullet been fired
		if (Input.GetKeyDown ("space")) {
			ShootBullet ();
		}
	}
	*/
	void FixedUpdate() {
        //Shoots a bullet
		if (Input.GetKeyDown ("space")) {
			ShootBullet ();
		}
	}

    // Generate the movement for Reimu
	void GenMovement() {
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			// rotate left, positive rotation
			totalRotation += anglePerFrame;
			direction = Quaternion.Euler (0, 0, anglePerFrame) * direction;
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			// rotate right, negative rotation
			totalRotation -= anglePerFrame;
			direction = Quaternion.Euler (0, 0, -anglePerFrame) * direction;
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			acceleration = accelRate * direction;
			velocity += acceleration;
			vehiclePosition += velocity;
			velocity = Vector3.ClampMagnitude (velocity, maxSpeed);


			// Set the transform component to the vehicle's position vector
			transform.position = vehiclePosition;
			transform.rotation = Quaternion.Euler (0, 0, totalRotation);
		}
		//Deceleration
		else if (!Input.GetKey (KeyCode.UpArrow)) { //&& Input.GetKeyUp (KeyCode.I)  
			acceleration = acceleration /2f;
			velocity *= .7f;
			vehiclePosition -= velocity;
			//velocity = Vector3.ClampMagnitude (velocity, maxSpeed);

			// Set the transform component to the vehicle's position vector
			transform.position = vehiclePosition;
			transform.rotation = Quaternion.Euler (0, 0, totalRotation);
		}
        //Needs to use custom screen wrapping
		if (transform.position.x > 7) {
			vehiclePosition.x = -7;
		} else if (transform.position.x < -7) {
			vehiclePosition.x = 7;
		} else if (transform.position.y > 5) {
			vehiclePosition.y = -5;
		} else if (transform.position.y < -5) {
			vehiclePosition.y = 5;
		}

	}
	/*
	void OnTriggerEnter2D(Collider2D c){
		// Anything except a bullet is an asteroid
		if (c.gameObject.tag != "Bullet") {
		AudioSource.PlayClipAtPoint (crash, Camera.main.transform.position);

		// Move the ship to the centre of the screen
			vehiclePosition = new Vector3 (0, 0, 0); 
			// Remove all velocity from the ship
			GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
			gameController.DecrementLives ();
		}
	}
	*/

    //Collisions with the larger asteroid
	void CircleCollisionYing(GameObject[] list) {
		foreach (GameObject o in list) {
			float o2x = o.transform.position.x;
			float o2y = o.transform.position.y;
			float o1x = gameObject.transform.position.x;
			float o1y = gameObject.transform.position.y;

			float distance = Mathf.Sqrt (((o1x - o2x) * (o1x - o2x)) + ((o1y - o2y) * (o1y - o2y)));

			bool col = distance < reimuRad + yingRad;
			if (col == true) {
				AudioSource.PlayClipAtPoint (crash, Camera.main.transform.position);

				// Move the ship to the centre of the screen
				vehiclePosition = new Vector3 (0, 0, 0); 
				// Remove all velocity from the ship
				GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
				gameController.DecrementLives ();

			}

		}
	}
    //Collisions with the smaller asteroid
	void CircleCollisionYang(GameObject[] list) {
		foreach (GameObject o in list) {
			float o2x = o.transform.position.x;
			float o2y = o.transform.position.y;
			float o1x = gameObject.transform.position.x;
			float o1y = gameObject.transform.position.y;

			float distance = Mathf.Sqrt (((o1x - o2x) * (o1x - o2x)) + ((o1y - o2y) * (o1y - o2y)));

			bool col = distance < reimuRad + yangRad;
			if (col == true) {
				AudioSource.PlayClipAtPoint (crash, Camera.main.transform.position);

				// Move the ship to the centre of the screen
				vehiclePosition = new Vector3 (0, 0, 0); 
				// Remove all velocity from the ship
				GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
				gameController.DecrementLives ();
			}

		}
	}


    //Bullet initialization
	void ShootBullet(){
		// Spawn a bullet
		Instantiate(bullet, new Vector3(transform.position.x,transform.position.y, 0), transform.rotation);
		        // Play a shoot sound
		        AudioSource.PlayClipAtPoint (shoot, Camera.main.transform.position);
	}
}