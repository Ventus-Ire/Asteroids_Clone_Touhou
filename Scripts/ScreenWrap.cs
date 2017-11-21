using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Change the location of the object
		        if(transform.position.x > 7){
			 
			            transform.position = new Vector3(-7, transform.position.y, 0);
			 
			        }
		        else if(transform.position.x < -7){
			            transform.position = new Vector3(7, transform.position.y, 0);
			        }
		 
		        else if(transform.position.y > 6){
			            transform.position = new Vector3(transform.position.x, -6, 0);
			        }
		 
		        else if(transform.position.y < -6){
			            transform.position = new Vector3(transform.position.x, 6, 0);
			        }
	}
}
