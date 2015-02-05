﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CollocatedVis : Visualization {
	
	
	//Debug code
	static float givenXMax;
	static float givenYMax; // x and y max are used to determine camera perspective size
	static Material lineMaterial;
	bool animateOnLoad;
	Mesh mesh;
	DrawUtil[] drawingUtility;
	
	int numbeOfIncomingVectors;
	int animationCounter;
	
	//////////////////////////////
	
	// Use this for initialization
	void Start () {
		//Debug code
		gameObject.AddComponent<MouseHandler>();
		gameObject.AddComponent<ScreenLines> ();
		animateOnLoad = true;
		givenXMax = 10;
		givenYMax = 10;
		animationCounter = 0;
		//test = new List<float> (new float[] {0,0,1,1,3,2,5,2,6,0,7,0});
		
		UpdateData ();
		//Set up the dataObject

		//hardcoded camera position/ortho
		Camera thisCam = gameObject.GetComponent<Camera> ();
		thisCam.transform.position = new Vector3 (5f, 5f, -15f);
		thisCam.orthographicSize = 5;
//		GameObject.FindGameObjectsWithTag ();

	}
	
	// Update is called once per frame
	void Update () {
		//Zoom functionality
		//////////////////////////
		//Graphics.DrawMesh(drawingUtility.AnimateCurrentFrame(counter), Vector3.zero, Quaternion.identity, lineMaterial, 0);
		
		animationCounter++;
		//Debug.Log ("There are this many meshcontainmentarrays" + meshContainmentArray.Count());

		for (int i = 0; i < numbeOfIncomingVectors; i++) {
			meshContainmentArray[i].GetComponent<MeshFilter> ().mesh 
				= drawingUtility[i].AnimateCurrentFrame (animationCounter);
		}
	}
	
	
	private List<float> getRandomFloatArray(){
		List<float> test = new List<float>();
		for (int i = 0; i < 1000; i++) {
			test.Add ((Random.value *10) % 10);
		}
		return test;
	}
	
	//real code - We may need to find some efficiency improvements here, there is a signifcant delay
	//when opening a large dataset. If that is not possible we can create a loading bar animation.
	public override void UpdateData(string dataPath){
		//TODO: check to make sure data exists
		Debug.Log("I am finally called!");
		DataBuilder data = new DataBuilder (dataPath);
		//Get the number of incoming vectors, we will need this number often
		numbeOfIncomingVectors = data.getDataObject ().incomingData.Count;
		meshContainmentArray = new GameObject[numbeOfIncomingVectors];
		//Create an array of drawing utilitys, one for each game object we will be drawing
		drawingUtility = new DrawUtil[numbeOfIncomingVectors];
		//Debug.Log("number of incoming vectors is: "+numbeOfIncomingVectors);
		for (int i = 0; i < numbeOfIncomingVectors; i++) {
			
			drawingUtility[i] = new DrawUtil (0.02f, getRandomFloatArray(), this.camera);
		}
		
		//Debug.Log(data.getDataObject().incomingData.First().First());
		List<float> tempList = new List<float>();

		for(int i = 0; i<data.getDataObject().incomingData.Count; i++){
				//Debug.Log(data.getDataObject().incomingData[i].ElementAt(0));
				tempList.Add(data.getDataObject().incomingData[i].ElementAt(0));
		}
	

		drawingUtility[0] = new DrawUtil (0.06f, tempList, this.camera);
		//drawingUtility[1] = new DrawUtil (0.06f, getRandomFloatArray(), this.camera);

		meshContainmentArray = new GameObject[numbeOfIncomingVectors];		
		
		
		//When we do a list of objects we will add the game object to the first element of the array
		//for now we just use this loop to test		
		for(int i = 0; i < numbeOfIncomingVectors; i++ ) {
			meshContainmentArray[i] = new GameObject ();
			meshContainmentArray[i].AddComponent<MeshFilter> ();
			meshContainmentArray[i].AddComponent<MeshRenderer> ();
			meshContainmentArray[i].AddComponent<StayPut> ();
			//meshContainmentArray[i] = (GameObject) Instantiate(vectorTemplate, gameObject.transform.position, Quaternion.identity);
			meshContainmentArray[i].transform.SetParent (gameObject.transform);
			meshContainmentArray[i].name = "Vector:" + i;
			
		}
		if(animateOnLoad){
			animationCounter = 0;
		}
	}

	//void CombineMeshes(){
	//	Debug.Log ("Child count is: "+MeshHolder.transform.childCount);
	//	Debug.Log ("Vert count: " + MeshHolder.transform.GetComponent<MeshFilter> ().mesh.vertexCount);
	//	if (MeshHolder.transform.childCount > 0) {
	//					MeshFilter[] meshFilters = MeshHolder.GetComponentsInChildren<MeshFilter> ();
	//					//	GameObject[] toBeDestoryed = GetComponentsInChildren<GameObject> ();
	//					CombineInstance[] combine = new CombineInstance[meshFilters.Length - 1];
	//					int i = 0;
	//					while (i<meshFilters.Length-1) {
	//
	//							combine [i].mesh = meshFilters [i].sharedMesh;
	//							combine [i].transform = meshFilters [i].transform.localToWorldMatrix;
	//							
	//							//meshFilters [i].gameObject.active = false;
	//							
	//							i++;
	//					}
	//					
	//					MeshHolder.transform.GetComponent<MeshFilter> ().mesh = new Mesh ();
	//					MeshHolder.transform.GetComponent<MeshFilter> ().mesh.CombineMeshes (combine);
	//					MeshHolder.transform.gameObject.SetActive (true);
	//						//List<GameObject> children = new List<GameObject>();
	//						//for(int x = 0; x < MeshHolder.transform.childCount; x++){
	//						//	children.Add (MeshHolder.transform.GetChild(x).gameObject);
	//						//}
	//						//foreach(GameObject go in children){
	//						//	Destroy (go);
	//						//}
	//		Debug.Log ("Vert count after: " + MeshHolder.transform.GetComponent<MeshFilter> ().mesh.vertexCount);
	//	}
	//}
	
	
	////////////////////////////////////////////////TEMP CODE FOR DEBUGGING///////////////////////////////////////
	static void CreateLineMat ()
	{
		if (!lineMaterial) {
			lineMaterial = new Material ("Shader \"Lines/Colored Blended\" {" +
			                             "SubShader { Pass { " +
			                             "    Blend SrcAlpha OneMinusSrcAlpha " +
			                             "    ZWrite Off Cull Off Fog { Mode Off } " +
			                             "    BindChannels {" +
			                             "      Bind \"vertex\", vertex Bind \"color\", color }" +
			                             "} } }");
		}
		
		lineMaterial.hideFlags = HideFlags.HideAndDontSave;
		lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
	}
	
}