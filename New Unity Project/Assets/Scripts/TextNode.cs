using UnityEngine;
using System.Collections;

public class TextNode : MonoBehaviour {
	private bool lockedToMouse;
	// Use this for initialization
	void Start () {
		lockedToMouse = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter(){
		Debug.Log ("Moused Over Node: " +gameObject.GetComponentInChildren<TextMesh>().text);
	}

	void OnMouseDown(){
		Debug.Log ("Dragging Node: "+gameObject.GetComponentInChildren<TextMesh>().text);
		//Debug.Log ("Camera is at :" +Camera.main.ScreenToWorldPoint());
	}

	void OnMouseUp(){
		Debug.Log ("Dragging for Node: "+gameObject.GetComponentInChildren<TextMesh>().text + " done");
	}

	public Vector3 getPosition(){
		return gameObject.transform.position;
	}
}
