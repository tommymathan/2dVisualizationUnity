using UnityEngine;
using System.Collections;

public class RadialVis :  Visualization {

	//Declared variables
	bool animateOnLoad;
	bool collidersLoaded;
	int numberIncomingVectors;
	int animationCounter;
	DrawUtil[] drawingUtility;

	// Use this for initialization
	void Start () {
	
		//Initilize mouse and background lines
		gameObject.AddComponent<MouseHandler> ();
		gameObject.AddComponent<ScreenLines> ();

		//Initialize 
		numberIncomingVectors = 0;
		animationCounter = 0;
		animateOnLoad = true;
		collidersLoaded = false;

		//Set current camera, position it and set it to orthographic mode
		Camera cam = gameObject.GetComponent<Camera>();
		cam.transform.position = new Vector3 (5f, 5f, -15f);
		cam.orthographicSize = 5;
	}
	
	// Update is called once per frame
	void Update () {
		//Zoom functionality
//		if (animateOnLoad) {
//			animationCounter++;
//		} else {
//			animationCounter = 10000;
//		}
//		if (animationCounter < 10) {
//			//for the number of vectors draw them
//			for (int i = 0; i < numberIncomingVectors; i++) {
//				//Send data to be drawn <<< data sent to drawing Uitility class
//				//Method animateCurrentFrame is to decide which visualization is to be drawn
//				meshContainmentArray [i].GetComponent<MeshFilter> ().mesh 
//					= drawingUtility [i].AnimateCurrentFrame (100000, meshContainmentArray [i]);
//			}
//		}
//
//		//Set colliders
//		if ((Time.time > 6 )&& (!collidersLoaded)) {
//			Debug.Log ("updating vector colliders");
//			GameObject[] vectorList = GameObject.FindGameObjectsWithTag("vector");
//			
//			for(int i = 0; i<vectorList.Length; i++){
//				DrawUtil.ManageVectorColliders(vectorList[i]);
//			}
//			collidersLoaded = true;
//		}
	}

	//real code - We may need to find some efficiency improvements here, there is a signifcant delay
	//when opening a large dataset. If that is not possible we can create a loading bar animation.
	public override void UpdateData (DataObject dataFromFile)
	{
		//TODO: check to make sure data exists
		//Debug.Log ("I am finally called!");
		DataObject data = new DataObject ();
		data = dataFromFile;
		
		//Get the number of incoming vectors, we will need this number often
		numberIncomingVectors = data.incomingData.Count -1;
		
		meshContainmentArray = new GameObject[numberIncomingVectors];
		//Create an array of drawing utilitys, one for each game object we will be drawing
		
		drawingUtility = new DrawUtil[numberIncomingVectors];
		//Debug.Log ("number of incoming vectors is: " + numberIncomingVectors);
		
		for (int i = 0; i < numberIncomingVectors; i++) {
			
			drawingUtility [i] = new DrawUtil (0.04f, data.incomingData [i], this.camera,550);
		}
		
		for (int i = numberIncomingVectors/2; i < numberIncomingVectors; i++) {
			
			drawingUtility [i] = new DrawUtil (0.02f, data.incomingData [i-(numberIncomingVectors/2)], this.camera,1);
		}
		
		
		meshContainmentArray = new GameObject[numberIncomingVectors];		
		
		
		//When we do a list of objects we will add the game object to the first element of the array
		//for now we just use this loop to test		
		for (int i = 0; i < numberIncomingVectors; i++) {
			meshContainmentArray [i] = new GameObject ();
			meshContainmentArray [i].AddComponent<MeshFilter> ();
			meshContainmentArray [i].AddComponent<MeshRenderer> ();
			meshContainmentArray [i].AddComponent<StayPut> ();
			meshContainmentArray [i].transform.SetParent (gameObject.transform);
			meshContainmentArray[i].tag = "vector";
			meshContainmentArray [i].name = "Vector:" + i;
			
			//if(i==0){
			//	DrawUtil.ManageColliders(tempList, meshContainmentArray[i]);
			//	Mesh theMesh = meshContainmentArray[i].GetComponent<MeshFilter>().mesh;
			//	Debug.Log (theMesh.ToString() + " " + theMesh.vertexCount);
			//	meshContainmentArray[i].GetComponent<MeshCollider>().sharedMesh = null;
			//	meshContainmentArray[i].GetComponent<MeshCollider>().sharedMesh = theMesh;
			//}
		}
		
		//right now this just parents the list to the first vector
		//this may need some changing, there is no update on sharedmesh
		
		
		
		if (animateOnLoad) {
			animationCounter = 0;
		}
	}

	
}
