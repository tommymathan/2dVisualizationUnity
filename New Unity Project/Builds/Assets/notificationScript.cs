#if UNITY_STANDALONE_WIN
using UnityEngine;
using System.Collections;
using System.IO;


public class notificationScript : MonoBehaviour {

	/*
	 * Location of the unity file ---unity.csv---
	 */
	public string path = "C:/Users/Public/unity.csv";
	System.DateTime time;
	public string animation;

	/**
	 * Check for modification of the unity file
	 */
	void Start () {

		//Checks if file exists if not creates it
		if (!File.Exists (path))
			File.Create (path);
		else
			time = File.GetLastWriteTime (path);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("Object is: " + gameObject.name);
		//Get time and date of file 
		System.DateTime modTime = File.GetLastWriteTime (path);

		//Check to see if it has been modified
		if (modTime.Day != time.Day || modTime.Hour != time.Hour || modTime.Minute != time.Minute || modTime.Second != time.Second) {
			time = modTime;
			animObj (gameObject, 1);
		}

	}
	/**
	 * Action for okay button
	 */
	public void notificationOkayButton()
	{
		//Animate object
		//animObj (gameObject, -1);
		GameObject dataManagerObject = GameObject.FindGameObjectWithTag ("DataManagerTag");
		//TODO: CHange method name to reflect function
		dataManagerObject.GetComponent<DataManager> ().SetDataPath (path);

		/**
		 * Call data management to update
		 */
	}

	void animObj(GameObject g, int speed)
	{
		g.GetComponent<Animation>() [animation].speed = speed;
		
		//No queue of animation, Plays it now
		g.GetComponent<Animation>() [animation].weight = 1;
		g.GetComponent<Animation>() [animation].time = 0;
		g.GetComponent<Animation>().Play ();

		
	}
}
#endif 