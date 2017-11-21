using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuChange : MonoBehaviour {
	int index = 0;
	public int totalItems = 2;
	public float yOffset = 1f;
	public AudioClip select;
	public AudioClip menuSwitch;
	// Use this for initialization
	void Start () {
        //Simple animation for menu system
		GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-0.0f, 90.0f);
	}
	
	// Update is called once per frame
	void Update () {
        //Switches the menu item
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			AudioSource.PlayClipAtPoint (menuSwitch, Camera.main.transform.position);
			if (index < totalItems - 1) {
				index++;
				Vector2 position = transform.position;
				position.y -= yOffset;
				transform.position = position;
			}
		}
        //switches the menu item
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			AudioSource.PlayClipAtPoint (menuSwitch, Camera.main.transform.position);
				if (index > 0) {
					index--;
					Vector2 position = transform.position;
					position.y += yOffset;
					transform.position = position;
				}
			}
        //Start the Game
		if (Input.GetKeyDown (KeyCode.Return)) {
			if (index == 0) {
				SceneManager.LoadScene ("Asteroids");
				AudioSource.PlayClipAtPoint (select, Camera.main.transform.position);
			}
            //Quit the application
			if (index == 1) {
				AudioSource.PlayClipAtPoint (select, Camera.main.transform.position);
				Application.Quit ();
			}
		}
	}
}
