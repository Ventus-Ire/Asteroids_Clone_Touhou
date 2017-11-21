using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Swaps the scene from the game over screen

		if (Input.GetKeyDown(KeyCode.C)) {
			SceneManager.LoadScene("Asteroids");
		}
		
	}
}
