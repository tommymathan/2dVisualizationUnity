using UnityEngine;
using System.Collections;

public class MouseCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("mouse hit: " +other.ToString());
		//other.GetComponentInParent
	}

	void OnTriggerExit(Collider other){
		Debug.Log ("mouse left: " +other.ToString());
	}
}
