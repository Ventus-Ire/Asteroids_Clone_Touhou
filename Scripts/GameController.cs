using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject asteroid;
	 
	    private int score;
	    private int hiscore;
	    private int asteroidsRemaining;
	    private int lives;
	    private int wave;
	    private int increaseEachWave = 2;
	 
	    public Text scoreText;
	    public Text livesText;
	    public Text waveText;
	    public Text hiscoreText;
	 
	    // Use this for initialization
	    void Start () {
		 
		        hiscore = PlayerPrefs.GetInt ("hiscore", 0);
		        BeginGame ();
		    }
	 
	    // Update is called once per frame
	    void Update () {
		Debug.Log (asteroidsRemaining);
		        // Quit if player presses escape
		        if (Input.GetKey("escape"))
			            Application.Quit();
		 
		    }
	 // initializes the start of the game
	    void BeginGame(){
		 
		        score = 0;
		        lives = 3;
		        wave = 1;
		 
		        // Prepare the HUD
		        scoreText.text = "SCORE:" + score;
		        hiscoreText.text = "HISCORE: " + hiscore;
		        livesText.text = "LIVES: " + lives;
		        waveText.text = "WAVE: " + wave;
		 
		        SpawnAsteroids();
		    }
    //Spawns the number of asteroids, varies depending on the level.
    void SpawnAsteroids(){
		 
		        DestroyExistingAsteroids();
		 
		        // Decide how many asteroids to spawn
		        // If any asteroids left over from previous game, subtract them
		        asteroidsRemaining = (wave * increaseEachWave);
		 
		        for (int i = 0; i < asteroidsRemaining; i++) {
			 
			            // Spawn an asteroid
			            Instantiate(asteroid,
				new Vector3(Random.Range(-6.0f, 6.0f),
					                    Random.Range(-6.0f, 6.0f), 0),
				                Quaternion.Euler(0,0,Random.Range(-0.0f, 359.0f)));
			 
			        }
		 
		        waveText.text = "WAVE: " + wave;
		    }
	    //Increases the score for an object that calls this method by 20
	    public void IncrementScore20(){
		        score +=  20;
		 
		        scoreText.text = "SCORE:" + score;
		 
		        if (score > hiscore) {
			            hiscore = score;
			            hiscoreText.text = "HISCORE: " + hiscore;
			 
			            // Save the new hiscore
			            PlayerPrefs.SetInt ("hiscore", hiscore);
			        }
		 
		        // Has player destroyed all asteroids?
		        if (asteroidsRemaining < 1) {
			 
			            // Start next wave
			            wave++;
			            SpawnAsteroids();
			 
			        }
		    }
    //Increases the score for an object that calls this method by 50
    public void IncrementScore50(){
		score +=  50;

		scoreText.text = "SCORE:" + score;

		if (score > hiscore) {
			hiscore = score;
			hiscoreText.text = "HISCORE: " + hiscore;

			// Save the new hiscore
			PlayerPrefs.SetInt ("hiscore", hiscore);
		}

		// Has player destroyed all asteroids?
		if (asteroidsRemaining < 1) {

			// Start next wave
			wave++;
			SpawnAsteroids();

		}
	}
	    //Decreases the number of lives a player has by 1
	    public void DecrementLives(){
		        lives--;
		        livesText.text = "LIVES: " + lives;
		 
		        // Has player run out of lives?
		        if (lives < 1) {
			            // Restart the game
			            //BeginGame();
			SceneManager.LoadScene ("GameOver");
						
			        }
		    }
	//Decreases the number of remaining asteroids in the background 
    public void DecrementAsteroids(){
		        asteroidsRemaining--;

    }
    //Splits up the larger asteroids and tracks it
	public void SplitAsteroid(){
		        // Two extra asteroids
		        // - big one
		        // + 3 little ones
		        // = 2
		        asteroidsRemaining+=2;
		 
		    }
    //Destorys any asteroid that may be lingering in the scene.
	    void DestroyExistingAsteroids(){
		        GameObject[] asteroids =
			            GameObject.FindGameObjectsWithTag("LargeAsteroid");
		 
		        foreach (GameObject current in asteroids) {
			            GameObject.Destroy (current);
			        }
		 
		        GameObject[] asteroids2 =
			            GameObject.FindGameObjectsWithTag("SmallAsteroid");
		 
		        foreach (GameObject current in asteroids2) {
			            GameObject.Destroy (current);
			        }
		    }
	 
}