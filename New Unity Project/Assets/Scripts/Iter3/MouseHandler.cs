using UnityEngine;
using System.Collections;

public class MouseHandler : MonoBehaviour {
	private Camera myCam;
	private float mouseSensitivity;
	private Vector3 lastPosition;
	private GameObject mouseObj;
	private BoxCollider2D mouseCol;

	// Use this for initialization
	void Start () {
		myCam = gameObject.GetComponent<Camera> ();
		mouseSensitivity = 0.01f;
		lastPosition = new Vector3 (0.0f, 0.0f, 0.0f);
		mouseObj = new GameObject();
		mouseObj.AddComponent<BoxCollider2D> ();
		mouseCol = mouseObj.GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("Cam Loc is: "+transform.position.x +" "+ transform.position.y);
		mouseObj.transform.position = new Vector3 (Input.mousePosition.x, Input.mousePosition.y);
		//Debug.Log ("Mouse@ : "+ mouseObj.transform.position.x +", "+ mouseObj.transform.position.y);
		if (Input.GetMouseButtonDown (0)) {
			lastPosition = Input.mousePosition;
		}

		if (Input.GetMouseButton (0)) {
			Vector3 delta = Input.mousePosition - lastPosition;
			transform.Translate(-delta.x*mouseSensitivity, -delta.y * mouseSensitivity, 0f);
			lastPosition = Input.mousePosition;
		}
	}
}
