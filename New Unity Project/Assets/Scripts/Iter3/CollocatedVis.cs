﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CollocatedVis : Visualization {
	
	
	//Debug code
	static float givenXMax;
	static float givenYMax; // x and y max are used to determine camera perspective size
	static Material lineMaterial;
	Mesh mesh;
	DrawUtil[] drawingUtility;
	
	int numbeOfIncomingVectors;
	int counter;
	
	//////////////////////////////
	
	// Use this for initialization
	void Start () {
		//Debug code
		
		
		givenXMax = 10;
		givenYMax = 10;
		counter = 0;
		//test = new List<float> (new float[] {0,0,1,1,3,2,5,2,6,0,7,0});
		
		updateData ();
		//Set up the dataObject
		
	}
	
	// Update is called once per frame
	void Update () {
		//Zoom functionality
		//////////////////////////
		//Graphics.DrawMesh(drawingUtility.AnimateCurrentFrame(counter), Vector3.zero, Quaternion.identity, lineMaterial, 0);
		
		counter++;
		//Debug.Log ("currentCounter" + counter);
		for (int i = 0; i < numbeOfIncomingVectors; i++) {
			meshContainmentArray[i].GetComponent<MeshFilter> ().mesh 
				= drawingUtility[i].AnimateCurrentFrame (counter);
		}
		drawingUtility[0].zoomFunction (this.camera);
		
	}
	
	
	private List<float> getRandomFloatArray(){
		List<float> test = new List<float>();
		for (int i = 0; i < 10; i++) {
			test.Add ((Random.value *10) % 10);
		}
		return test;
	}
	
	//real code - We may need to find some efficiency improvements here, there is a signifcant delay
	//when opening a large dataset. If that is not possible we can create a loading bar animation.
	public void updateData(){
		//TODO: check to make sure data exists
		DataBuilder data = new DataBuilder ();
		//Get the number of incoming vectors, we will need this number often
		numbeOfIncomingVectors = data.getDataObject ().incomingData.Count;
		
		//Create an array of drawing utilitys, one for each game object we will be drawing
		drawingUtility = new DrawUtil[numbeOfIncomingVectors];
		
		for (int i = 0; i < numbeOfIncomingVectors; i++) {
			
			drawingUtility[i] = new DrawUtil (0.02f, getRandomFloatArray(), this.camera);
		}		
		createMeshMonster ();
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
	void OnPostRender ()
	{	
		beginDraw ();
		drawGridLines ();
		GL.End ();
	}
	void beginDraw ()
	{
		CreateLineMat ();
		lineMaterial.SetPass (0);
		GL.Begin (GL.LINES);
		GL.Color (Color.white);		
	}
	
	//Draws gridlines so that we can debug more easily, draw in grey then switch back to white
	public void drawGridLines ()
	{
		
		GL.Color (Color.grey);
		
		for (int i = 0; i < (int)Mathf.Ceil(givenXMax); i++) {
			GL.Vertex3 ((float)i, 0f, 0f);
			GL.Vertex3 ((float)i, givenYMax, 0f);			
		}
		
		for (int i = 0; i < (int)Mathf.Ceil(givenYMax); i++) {
			GL.Vertex3 (0f, (float)i, 0f);
			GL.Vertex3 (givenXMax, (float)i, 0f);			
		}
		
		
		GL.Color (Color.white);
		
	}
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