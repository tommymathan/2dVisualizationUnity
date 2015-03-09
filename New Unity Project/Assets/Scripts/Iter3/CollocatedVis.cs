using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CollocatedVis : Visualization
{
	
	
	// Use this for initialization
	void Start ()
	{
		//Initalize local variables
		animateOnLoad = true;
		animationCounter = 0;
		numberIncomingVectors = 0;
		animationQueue = new HashSet<int> ();
		layer = 8;
		vizID = 0;
		
		//hardcoded camera position/ortho
		Camera thisCam = gameObject.GetComponent<Camera> ();
		thisCam.transform.position = new Vector3 (5f, 5f, -15f);
		thisCam.orthographicSize = 5;
		collidersLoaded = true;
		
		visColor = Color.magenta;
		globalSettingsObject = GameObject.FindGameObjectWithTag("GlobalSettingsObject");
		dataUpdated = false;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (animateOnLoad) {
			animationCounter++;		
			if(animationInProgress)	animateVectorsInQueue();
		} 
		
		if (dataUpdated) {
			for (int i = 0; i < numberIncomingVectors; i++) {
				meshContainmentArray [i].GetComponent<MeshFilter> ().mesh 
					= drawingUtility [i].filteredCoordinates();
			}
			
			if (collidersLoaded == false) {
				Debug.Log ("updating vector colliders");
				GameObject[] vectorList = GameObject.FindGameObjectsWithTag ("vector");
				for (int i = 0; i<vectorList.Length; i++) {
					DrawUtil.ManageVectorColliders (vectorList [i]);
				}
				collidersLoaded = true;
			}
			dataUpdated = false;
		}
		
		if(Input.GetKeyDown(KeyCode.LeftAlt)){
			Debug.Log ("Shit! You pressed Left Alt!");
		}
	}
	private void animateVectorsInQueue(){
		int[] animation = animationQueue.ToArray ();
		if (animationQueue.Count > 0) {
			for (int j = 0; j < animationQueue.Count; j++) {
				Debug.Log ("we are now exectuing!" + animationQueue.Count + "<size of queue Counter > " + animationCounter);
				meshContainmentArray [animation[j]].GetComponent<MeshFilter> ().mesh 
					= drawingUtility [animation[j]].AnimateCurrentFrame (animationCounter, meshContainmentArray [animation [j]]);
				
			}
		}
		if (animationCounter >= DRAWINGSPEED * (numberValsPerVector / 2)) {
			animationInProgress = false;
			animationQueue.Clear();
		}
		
	}
	
	public override void addLineToAnimate(HashSet<int> val)
	{
		animationInProgress = true;
		int [] iter = val.ToArray ();
		foreach (int i in iter) {
			animationQueue.Add(i);
		}
		
		animateOnLoad = true;
		animationCounter = 0;
	}
	
	
	public override void UpdateData (DataObject dataFromFile)
	{
		MouseCollision mouseCollider = this.GetComponentInChildren<MouseCollision> ();
		mouseCollider.colorRetainer.Clear ();
		mouseCollider.hoverList.Clear ();
		mouseCollider.selection.Clear ();
		//Destroy every vector in this vis when updating data;
		for(int i = 0; i<meshContainmentArray.Count(); i++){
			DestroyImmediate(meshContainmentArray[i]);
		}
		
		//TODO: check to make sure data exists
		//Debug.Log ("I am finally called!");
		DataObject data = new DataObject ();
		data = dataFromFile;
		
		//Get the number of incoming vectors, we will need this number often
		numberIncomingVectors = data.incomingData.Count -1;
		numberValsPerVector = data.labels.Count - 1;
		
		meshContainmentArray = new GameObject[numberIncomingVectors];
		//Create an array of drawing utilitys, one for each game object we will be drawing
		
		drawingUtility = new DrawUtil[numberIncomingVectors];
		//Debug.Log ("number of incoming vectors is: " + numberIncomingVectors);
		
		for (int i = 0; i < numberIncomingVectors; i++) {
			
			drawingUtility [i] = new DrawUtil (0.03f, data.normalizedData [i], this.GetComponent<Camera>(),DRAWINGSPEED,vizID);
		}
		
		
		meshContainmentArray = new GameObject[numberIncomingVectors];		
		
		
		
		//When we do a list of objects we will add the game object to the first element of the array
		//for now we just use this loop to test		
		for (int i = 0; i < numberIncomingVectors; i++) {
			Shader unlit = Shader.Find("Unlit/Color");
			meshContainmentArray [i] = new GameObject ();
			meshContainmentArray [i].AddComponent<MeshFilter> ();
			meshContainmentArray[i].GetComponent<MeshFilter>().mesh.RecalculateNormals();
			meshContainmentArray [i].AddComponent<MeshRenderer> ();
			meshContainmentArray [i].AddComponent<StayPut> ();
			meshContainmentArray [i].transform.SetParent (gameObject.transform);
			meshContainmentArray[i].tag = "vector";
			meshContainmentArray [i].name = i.ToString();
			meshContainmentArray[i].GetComponent<Renderer>().material.shader = unlit;
			GlobalSettings gs = globalSettingsObject.GetComponent<GlobalSettings>();
			meshContainmentArray[i].GetComponent<MeshRenderer>().material.color = new Color(gs.globalLineR,gs.globalLineG,gs.globalLineB);

			meshContainmentArray[i].layer = layer; //8 is collocated visuals layer
			
			drawingUtility[i].lineWidth = globalSettingsObject.GetComponent<GlobalSettings>().gLOLWidths;
			dataUpdated = true;
			
		}
		
		if (animateOnLoad) {
			animationCounter = 0;
		}
		collidersLoaded = false;
	}
	
	
	
}