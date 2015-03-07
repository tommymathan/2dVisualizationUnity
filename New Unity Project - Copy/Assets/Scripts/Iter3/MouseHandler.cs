using UnityEngine;
using System.Collections;

public class MouseHandler : MonoBehaviour {
	public Camera myCam;
	private float mouseSensitivity;
	private Vector3 lastPosition;
	private GameObject mouseObj;
	private BoxCollider mouseCol;
	private float mouseColliderSize;
	
	// Use this for initialization
	void Start () {
		mouseColliderSize = 0.1f;
		myCam = gameObject.GetComponent<Camera> ();
		mouseSensitivity = 0.01f;
		lastPosition = new Vector3 (0.0f, 0.0f, 0.0f);
		mouseObj = new GameObject();

		mouseObj.AddComponent<Rigidbody>();
		Rigidbody rb = mouseObj.GetComponent<Rigidbody>();
		rb.useGravity = false;

		mouseObj.AddComponent<BoxCollider> ();
		mouseCol = mouseObj.GetComponent<BoxCollider> ();
		mouseObj.name = "Mouse Loc";
		mouseObj.tag = "Mouse";

		mouseCol.size = new Vector3(0.1f,0.1f,50f);
		mouseCol.isTrigger = true;

		mouseObj.AddComponent<MouseCollision>();

	}
	
	// Update is called once per frame
	void Update () {
		PanFunction();
		ZoomFunction();
		//Debug.Log (mouseObj.transform.position);
		//Debug.Log ("Cam Loc is: "+transform.position.x +" "+ transform.position.y);
		mouseObj.transform.position = myCam.ScreenToWorldPoint(Input.mousePosition);
		//Debug.Log ("Mouse@ : "+ mouseObj.transform.position.x +", "+ mouseObj.transform.position.y);
	}
	
	public void PanFunction(){
		
		if (Input.GetMouseButtonDown (0)) {
			lastPosition = Input.mousePosition;
		}
		
		if (Input.GetMouseButton (0)) {
			Vector3 delta = Input.mousePosition - lastPosition;
			transform.Translate(-delta.x*mouseSensitivity, -delta.y * mouseSensitivity, 0f);
			lastPosition = Input.mousePosition;
		}
	}
	
	public void ZoomFunction(){
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) { // forward
			myCam.orthographicSize++;
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) { // back
			if((myCam.orthographicSize <= 1.0) && (myCam.orthographicSize>0.1)){ //dont allow the ortho size to hit 0 because it freaks the f out
				myCam.orthographicSize-=0.1f;
			}
			else if(myCam.orthographicSize>0.1){
				myCam.orthographicSize--;
			}
		}
		
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("test from mouse");
	}
}
