using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CollocatedVis : Visualization
{
	
	
		//Debug code
		static float givenXMax;
		static float givenYMax; // x and y max are used to determine camera perspective size
		static Material lineMaterial;
		bool animateOnLoad;
		Mesh mesh;
		DrawUtil[] drawingUtility;
		int numberIncomingVectors;
		int animationCounter;
		Color[] colors;
	
		//////////////////////////////
	
		// Use this for initialization
		void Start ()
		{
				//Debug code
				gameObject.AddComponent<MouseHandler> ();
				gameObject.AddComponent<ScreenLines> ();
				animateOnLoad = true;
				givenXMax = 10;
				givenYMax = 10;
				animationCounter = 0;
				numberIncomingVectors = 0;
				//test = new List<float> (new float[] {0,0,1,1,3,2,5,2,6,0,7,0});
		
				//UpdateData ();
				//Set up the dataObject

				//hardcoded camera position/ortho
				Camera thisCam = gameObject.GetComponent<Camera> ();
				thisCam.transform.position = new Vector3 (5f, 5f, -15f);
				thisCam.orthographicSize = 5;
//		GameObject.FindGameObjectsWithTag ();

		}
	
		// Update is called once per frame
		void Update ()
		{
				//Zoom functionality
				//////////////////////////
				//Graphics.DrawMesh(drawingUtility.AnimateCurrentFrame(counter), Vector3.zero, Quaternion.identity, lineMaterial, 0);
		
				animationCounter++;
				//Debug.Log ("There are this many meshcontainmentarrays" + meshContainmentArray.Count());
		
				for (int i = 0; i < numberIncomingVectors; i++) {
						meshContainmentArray [i].GetComponent<MeshFilter> ().mesh 
				= drawingUtility [i].AnimateCurrentFrame (animationCounter);


			//meshContainmentArray [i].GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
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
				//TODO: check to make sure data exists
				//Debug.Log ("I am finally called!");
				DataObject data = dataFromFile;

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
						meshContainmentArray [i].name = "Vector:" + i;
			
				}

				if (animateOnLoad) {
						animationCounter = 0;
				}
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
	
}