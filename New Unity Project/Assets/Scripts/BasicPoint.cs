using UnityEngine;
using System.Collections;

public class BasicPoint : MonoBehaviour {
	public GameObject otherPoint;
	public Component thisLineR;
	// Use this for initialization
	void Start () {
		thisLineR = gameObject.GetComponent<LineRenderer> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 0.01f);
		if (thisLineR != null) {
			Debug.Log ("LineRenderer is assigned");
				}
		thisLineR.transform.position = new Vector3 (gameObject.transform.position.x,gameObject.transform.position.y);
	}
}
