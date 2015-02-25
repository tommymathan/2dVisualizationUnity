using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CollocatedVis : Visualization
{
	
	
		//Debug code
		public float lineWidth;
		static Material lineMaterial;
		bool animateOnLoad;
		bool collidersLoaded;
		Mesh mesh;
		DrawUtil[] drawingUtility;
		int numberIncomingVectors;
		int animationCounter;
		Color[] colors;
		int counter = 0;
		Color visColor;
		//////////////////////////////
	
		// Use this for initialization
		void Start ()
		{
				//Debug code
				gameObject.AddComponent<MouseHandler> ();
				gameObject.AddComponent<ScreenLines> ();
				animateOnLoad = true;

				animationCounter = 0;
				numberIncomingVectors = 0;
				//test = new List<float> (new float[] {0,0,1,1,3,2,5,2,6,0,7,0});
		
				//UpdateData ();
				//Set up the dataObject

				//hardcoded camera position/ortho
				Camera thisCam = gameObject.GetComponent<Camera> ();
				thisCam.transform.position = new Vector3 (5f, 5f, -15f);
				thisCam.orthographicSize = 5;
				collidersLoaded = true;
				//GameObject.FindGameObjectsWithTag ();
				visColor = Color.magenta;
				lineWidth = 2f;
		}
	
		// Update is called once per frame
		void Update ()
		{
				//Zoom functionality
				//////////////////////////
				//Graphics.DrawMesh(drawingUtility.AnimateCurrentFrame(counter), Vector3.zero, Quaternion.identity, lineMaterial, 0);
			if (animateOnLoad) {
						animationCounter++;
						//Debug.Log ("There are this many meshcontainmentarrays" + meshContainmentArray.Count());						
						//meshContainmentArray [i].GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
				} else {
					animationCounter = 10000;
				}
		if (Input.GetKeyDown ("space")) {
						for (int i = 0; i < numberIncomingVectors; i++) {
								drawingUtility[i].lineWidth = lineWidth;
								// DestroyImmediate( meshContainmentArray [i].GetComponent<MeshFilter> ().sharedMesh);
								meshContainmentArray [i].GetComponent<MeshFilter> ().mesh 
								= drawingUtility [i].AnimateCurrentFrame (100000, meshContainmentArray [i]);
								if(meshContainmentArray[i].GetComponent<MeshCollider>() != null){
									meshContainmentArray[i].GetComponent<MeshCollider>().sharedMesh = meshContainmentArray [i].GetComponent<MeshFilter> ().mesh;
								}
								Debug.Log ("redrawing a line with width" + drawingUtility[i].lineWidth);
						}
				
						counter++;
						if (counter > 0 && counter <2) {
								Debug.Log ("updating vector colliders");
								GameObject[] vectorList = GameObject.FindGameObjectsWithTag ("vector");

								for (int i = 0; i<vectorList.Length; i++) {
										DrawUtil.ManageVectorColliders (vectorList [i]);
								}
								collidersLoaded = true;
						}
		}
	}
	
	private List<float> getRandomFloatArray ()
		{
				List<float> test = new List<float> ();
				for (int i = 0; i < 1000; i++) {
						test.Add ((Random.value * 10) % 10);
				}
				return test;
		}
	
		//real code - We may need to find some efficiency improvements here, there is a signifcant delay
		//when opening a large dataset. If that is not possible we can create a loading bar animation.
		public override void UpdateData (DataObject dataFromFile)
		{


		GameObject[] vectorList = GameObject.FindGameObjectsWithTag ("vector");		
		for (int i = 0; i<vectorList.Length; i++) {
			Destroy(vectorList [i]);
		}
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
					drawingUtility [i] = new DrawUtil (lineWidth, data.incomingData [i], this.camera,1);
				}

				meshContainmentArray = new GameObject[numberIncomingVectors];		
				
				//templist used for colliders
				//List<float> tempList = new List<float>();
				//Debug.Log (data.incomingData.Count);
				//
				//for(int i = 0; i<data.incomingData.Count; i++){
				//	Debug.Log(data.incomingData[i].ElementAt(0));
				//	//tempList.Add(data.incomingData[i].ElementAt(0));
				//}


		
				//When we do a list of objects we will add the game object to the first element of the array
				//for now we just use this loop to test		
				for (int i = 0; i < numberIncomingVectors; i++) {
						Shader unlit = Shader.Find("Self-Illumin/Diffuse");
						meshContainmentArray [i] = new GameObject ();
						meshContainmentArray [i].AddComponent<MeshFilter> ();
						meshContainmentArray[i].GetComponent<MeshFilter>().mesh.RecalculateNormals();
						meshContainmentArray [i].AddComponent<MeshRenderer> ();
						meshContainmentArray [i].AddComponent<StayPut> ();
						meshContainmentArray [i].transform.SetParent (gameObject.transform);
						meshContainmentArray[i].tag = "vector";
						meshContainmentArray [i].name = "Vector:" + i;
						meshContainmentArray[i].renderer.material.shader = unlit;
						meshContainmentArray[i].GetComponent<MeshRenderer>().material.color = visColor;
						

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
		collidersLoaded = false;
	}

	
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

		public void UpdateLineWidth(float f){
			lineWidth = f;
			Debug.Log ("Updating lineWidth..." + this.gameObject.ToString() + f);
		}
}