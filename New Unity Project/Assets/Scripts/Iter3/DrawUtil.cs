//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18331
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections.Generic;


public class DrawUtil
{
	public GameObject currentVisObject;//used to store meshfilter and rendering data
	private Renderer meshRenderer; // this will refer to the MR for any spawned children
	private MeshFilter meshFilter; // this will refer to the MF for any spawned children
	
	private List<float> shiftedDataSet;
	
	
	private int zDist; //backdist for camera(has no affect on FOV in ortho mode
	
	//Incoming variables
	public float lineWidth;
	private List<float> incomingDataSet;
	private Camera curVisCamera;
	private int ANIMATIONSPEED = 100;
	private int currentVisualizationMethod;
	private int lineType;
	
	
	//Animation variables
	private int animationFrame;
	private int cursor;
	private float previousX;
	private float previousY;
	private bool animateOnUpdate;
	
	
	
	//collider variables use the drawutil to find verts
	private List<float> colliderDataSet;
	private bool collidersLoaded;
	
	
	
	public DrawUtil (float w, List<float> d ,Camera visCamera,int animationSpeed,int visSetting)
	{
		lineType = 0;
		ANIMATIONSPEED = animationSpeed;
		animationFrame = 0;
		lineWidth = w;
		incomingDataSet = d;
		animateOnUpdate = true;
		curVisCamera = visCamera;
		collidersLoaded=false;
		currentVisualizationMethod = visSetting;		
		checkIncomingData ();
		generalPointShifter ();
	}
	
	
	
	
	public Mesh AnimateCurrentFrame (int updateFrame, GameObject go)
	{
		if (currentVisObject == null) {
			currentVisObject = go;
			Debug.Log ("Null vis object, rebuilding...");
		}
		//if this is the first time the loop is executing we need to set the starting frame so that we know
		//how far we've come in this animation
		if (!animateOnUpdate){
			//animationFrame = updateFrame;
			animateOnUpdate = true;
		}
		//Partial list to display to user
		List<float> temp = new List<float> ();
		//Track the current frame relative to the time when the animation was called
		int currentRelativeFrame = updateFrame - animationFrame;
		float currentPrecentageAnimated = ((float)((currentRelativeFrame % ANIMATIONSPEED )) / ANIMATIONSPEED);
		//We add the first two vertexes to the array because we cannot animate a single point, we don't need to waste the time
		temp.Add (shiftedDataSet [0]);
		temp.Add (shiftedDataSet [1]);
		
		//The cursor will keep track of our current true animation frame, that is the number of key frames that have passed since the
		//animation method was called
		cursor = (currentRelativeFrame / ANIMATIONSPEED) *2;
		
		//We are done once the cursor is within 4 elements of the size of the incoming data
		if (cursor + 2 < shiftedDataSet.Count) {
			//for every two points in the data set we add them as a pair to the temporary dataset for display
			for (int j = 2; j < cursor+2; j+=2) {
				temp.Add (shiftedDataSet [j]);
				temp.Add (shiftedDataSet [j + 1]);
			}
			//We track the previous point so that we know where to animate from
			previousX = temp [temp.Count - 2];
			previousY = temp [temp.Count - 1];
			
			//The general idea here is to graph the previous x + % current x and
			//previous y + % current y
			
			temp.Add ((((shiftedDataSet [cursor + 2]) - previousX) * currentPrecentageAnimated)
			          + previousX);
			
			
			temp.Add ((((shiftedDataSet [cursor + 3])-previousY) * currentPrecentageAnimated )
			          + previousY);
			
			return DrawContiguousLineSegments (temp);
			
		} else {
			animateOnUpdate = false;
			return filteredCoordinates ();
		}
	}
	
	private void generalPointShifter(){
		
		switch (currentVisualizationMethod) {
		case 0:
			collocatedPointShifter ();
			break;
		case 1:
			shiftedPointShifter ();
			break;
		case 2:
			radialPointShifter();
			break;
		case 3:
			//
			break;
		case 4:
			//
			break;
		default:
			collocatedPointShifter ();
			break;
			
		}
	}
	private void collocatedPointShifter(){
		Debug.Log ("collocated point shifter called");
		shiftedDataSet = new List<float> ();
		float orginX = incomingDataSet [0]; //temp code for demo
		float orginY = incomingDataSet [1]; //temp code for demo
		shiftedDataSet.Add (incomingDataSet [0]);
		shiftedDataSet.Add (incomingDataSet [1]);
		for (int j = 2; j < incomingDataSet.Count; j+=2) {
			
			shiftedDataSet.Add( incomingDataSet [j] + orginX);
			shiftedDataSet.Add( incomingDataSet [j + 1] + orginY);
			
			orginX = shiftedDataSet [j]; 
			orginY = shiftedDataSet [j + 1]; 
		}
	}
	
	private void radialPointShifter(){
		Debug.Log ("Radial point shifter called");
		shiftedDataSet = new List<float> ();
		float orginX = incomingDataSet [0]; 
		float orginY = incomingDataSet [1]; 
		
		for (int j = 2; j < incomingDataSet.Count; j+=2) {
			
			shiftedDataSet.Add (orginX);
			shiftedDataSet.Add (orginY);
			
			shiftedDataSet.Add( incomingDataSet [j]);
			shiftedDataSet.Add( incomingDataSet [j + 1]);
			
		}
	}
	
	private void shiftedPointShifter(){
		Debug.Log ("Shifted point shifter called");
		shiftedDataSet = new List<float> ();
		float orginX = 0; 
		float orginY = 0; 
		
		for (int j = 0; j < incomingDataSet.Count; j+=2) {
			
			shiftedDataSet.Add( incomingDataSet [j]+ orginX);
			shiftedDataSet.Add( incomingDataSet [j + 1]+orginY);
			
			orginX = ( orginX +1 ); 		
			orginY = ( orginY + 1);	
			
		}
	}
	
	
	
	public Mesh filteredCoordinates()
	{
		switch (currentVisualizationMethod) {
		case 0:
			return collocatedFilter (incomingDataSet);
			
		case 1:
			return shiftedFilter (incomingDataSet);
			
		case 2:
			return radialFilter (incomingDataSet);
			
		case 3:
			return collocatedFilter (incomingDataSet);
			
		case 4:
			return collocatedFilter (incomingDataSet);
			
		default:
			return collocatedFilter (incomingDataSet);
			
		}
		
	}
	public Mesh collocatedFilter(List<float> dataSet)
	{
		
		float orginX = 0;	
		float orginY = 0;	
		List<float> temp = new List<float>();
		for (int i=0; i < dataSet.Count; i+=2) {
			temp.Add (dataSet [i]+orginX);
			temp.Add(dataSet [i + 1] +orginY);
			
			orginX = (dataSet [i] + orginX) ; 		
			orginY = (dataSet [i + 1] + orginY );	
		}
		return DrawContiguousLineSegments (temp);
		
	}
	
	//If there is an uneven number of incoming data points then we pair the first point with itself
	private void checkIncomingData()
	{
		if (!(incomingDataSet.Count % 2 == 0))
			incomingDataSet.Insert (0, incomingDataSet [0]);
	}
	
	private Mesh shiftedFilter(List<float> dataSet)
	{
		
		float orginX = 0;	
		float orginY = 0;	
		List<float> temp = new List<float>();
		for (int i=0; i < dataSet.Count; i+=2) {
			temp.Add (dataSet [i]+orginX);
			temp.Add(dataSet [i + 1] +orginY);
			
			orginX = ( orginX +1 ); 		
			orginY = ( orginY + 1);	
		}
		return DrawContiguousLineSegments (temp);
		
	}
	
	/**
	 * Method that determines how the radial paired visualization is drawn.
	 * Each vector takes the first set of points as the origin and the rest of
	 * the points for that vector are drawn from that origin point.
	 * 
	 * dataset - the data set as a list of floats
	 */
	private Mesh radialFilter(List<float> dataSet)
	{
		//Current origin point
		float orginX = dataSet[0];	
		float orginY = dataSet [1];
		
		//temporary list to that stores the manipulated dataset to be drawn
		List<float> temp = new List<float>();
		
		//Start at 2 because the first two are the origin points
		for (int i = 2; i < dataSet.Count; i+=2) {
			
			//Add origin first
			temp.Add(orginX);
			temp.Add (orginY);
			
			//add end point to the vector
			temp.Add (dataSet[i]);
			temp.Add (dataSet[i+1]);
			
		}
		return DrawContiguousLineSegments (temp);
		
	}
	
	public Mesh DrawContiguousLineSegments (List<float> dataSet)
	{
		Mesh mesh; //temp mesh
		
		object[] verts = new object[2];
		Vector3 currentVector = new Vector3 (0, 0, 0); //used to gather a direction of the line to properly set the edges of the generated quad
		Vector3 up = new Vector3 (0, 0, -10); //used for cross product
		Vector3 forward = new Vector3 (1, 1, -10); //used for cross product
		Vector3 right = new Vector3 (0, 0, 0); // used to push verts out from the lines to form quads
		
		Vector3 coneVector = new Vector3 (0, 0, 0);
		Vector3 arrow = new Vector3 (0, 0, 0);
		
		List<Vector3> organizedData = new List<Vector3> ();
		
		List<Vector3> organizedPoints = new List<Vector3> ();
		List<Vector2> organizedPointUvs = new List<Vector2> ();
		
		//put the data in this format {v3, v3, v3, v3};
		for (int i=0; i < dataSet.Count; i+=2) {
			organizedData.Add (new Vector3 (dataSet [i], dataSet [i + 1], zDist));
			
			//			Debug.Log ("Pair added " + dataSet[i] +" / " + dataSet[i+1]);
		}
		
		//now that the list of points is in order of occurrance, gather vectors for each line segment and create quads along those segments
		//a direction is targetPosition-currentposition
		checkForLineType ();
		
		for (int i=0; i<=organizedData.Count-2; i++) {
			
			if (i < organizedData.Count - 1) {
				currentVector = new Vector3 (organizedData [i + 1].x - organizedData [i].x, organizedData [i + 1].y - organizedData [i].y, organizedData [i + 1].z - organizedData [i].z);
				//Debug.Log("Vector for section is " + currentVector);
			} else {
				//do nothing
			}
			//find right vector
			right = -Vector3.Cross (currentVector.normalized, up.normalized) * lineWidth;
			coneVector = -Vector3.Cross (currentVector.normalized, up.normalized) * (lineWidth/3);
			
			//add the points to the left and right of this point
			//and add the points to the left and the right of the next point(assuming it's normal matches this normal)
			organizedPoints.Add ((organizedData [i] + right));
			organizedPoints.Add ((organizedData [i] - right));
			
			
			//Cone line type
			if(lineType == 2){
				organizedPoints.Add ((organizedData [i + 1] + coneVector));
				organizedPoints.Add ((organizedData [i + 1] - coneVector));
				
				organizedPointUvs.Add ((Vector2)(organizedData [i + 1] + coneVector));
				organizedPointUvs.Add ((Vector2)(organizedData [i + 1] - coneVector));
				
			}
			//Arrow line type
			else if (lineType == 1)
			{	
				organizedPoints.Add ((organizedData [i + 1] + right));
				organizedPoints.Add ((organizedData [i + 1] - right));
				
				organizedPointUvs.Add ((Vector2)(organizedData [i + 1] + right));
				organizedPointUvs.Add ((Vector2)(organizedData [i + 1] - right));
				
			}
			//No arrows line type
			else{
				organizedPoints.Add ((organizedData [i + 1] + right));
				organizedPoints.Add ((organizedData [i + 1] - right));
				
				organizedPointUvs.Add ((Vector2)(organizedData [i + 1] + right));
				organizedPointUvs.Add ((Vector2)(organizedData [i + 1] - right));
			}
			organizedPointUvs.Add ((Vector2)(organizedData [i] + right));
			organizedPointUvs.Add ((Vector2)(organizedData [i] - right));
			
			
			
		}
		
		//ManageCoordinateTags (organizedData); RESTORE THIS LATER
		//meshfilter needs arrays to work
		Vector3[] newVerts = organizedPoints.ToArray ();
		Vector2[] newUv = organizedPointUvs.ToArray ();
		
		int[] newTriangles = new int[(organizedData.Count - 1) * 6]; //each point needs a quad and each quad is 2 triangles made of 3 points
		
		
		//this is messy and stupid, is there an easier way?
		int firstIndex = 0;
		int secondIndex = 1;
		for (int i = 0; i<=newTriangles.Length-6; i+=6) {
			newTriangles [i] = firstIndex;
			newTriangles [i + 1] = ++firstIndex;
			newTriangles [i + 2] = ++firstIndex;
			firstIndex += 2;
			newTriangles [i + 3] = secondIndex;
			newTriangles [i + 4] = secondIndex + 2;
			newTriangles [i + 5] = secondIndex + 1;
			secondIndex += 4;
		}
		
		mesh = new Mesh();
		//meshFilter.mesh = mesh;
		
		mesh.vertices = newVerts;
		mesh.uv = newUv;
		mesh.triangles = newTriangles;
		
		if (!collidersLoaded) {
			colliderDataSet = dataSet;
			//ManageColliders(colliderDataSet);
			collidersLoaded = true;
		}
		//Debug.Log (mesh.vertexCount);
		return mesh;
	}
	private void checkForLineType(){
		GameObject globalSettings = GameObject.FindGameObjectWithTag ("GlobalSettingsObject");
		lineType = globalSettings.GetComponent<GlobalSettings> ().lineType;
	}
	
	
	
	public static void ManageVectorColliders(GameObject theObject){
		Mesh theMesh = theObject.GetComponent<MeshFilter> ().mesh;
		theObject.AddComponent<MeshCollider> ();
		MeshCollider thisCollider = theObject.GetComponent<MeshCollider> ();
		//thisCollider.isTrigger = true;
		thisCollider.sharedMesh = null; //for some reason you have to make this null before you assign it /*shrug*/
		thisCollider.sharedMesh = theMesh;
		//Debug.Log (theObject.name);
		//Debug.Log (theMesh.vertexCount);
		
	}
	
	public void ManageColliders(List<float> passedList){
		for(int i = 0; i < passedList.Count; i+=2){
			//Debug.Log(givenList[i]+ " " +givenList[i+1]);
			GameObject vert = new GameObject();
			vert.name = ""+passedList[i]+","+passedList[i+1];
			vert.transform.position = new Vector3(passedList[i], passedList[i+1], -15);
			vert.AddComponent<BoxCollider>();
			
			//vert.AddComponent<Rigidbody>();
			//Rigidbody rb = vert.GetComponent<Rigidbody>();
			//rb.useGravity = false;
			
			BoxCollider bc = vert.GetComponent<BoxCollider>();
			bc.isTrigger = true;
			vert.transform.parent = currentVisObject.transform;
		}
	}
}

