﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Utility : MonoBehaviour
{
		private Renderer meshRenderer;
		private MeshFilter meshFilter; // used for mesh rendering
		private Mesh mesh; //the actual mesh in space
		private int zDist; //hack to move the camera back for easier debugging
		
		//Incoming variables
		private float lineWidth;
		private List<float> incomingDataSet;

		//Animation variables
		private int frameCounter;
		private int animationFrame;
		private int cursor;
		private float previousX;
		private float previousY;
		private bool animateOnUpdate;
		private int ANIMATIONSPEED = 10;


		// Use this for initialization
		void Start ()
		{
				meshFilter = gameObject.GetComponent<MeshFilter> ();
				meshRenderer = gameObject.GetComponent<MeshRenderer> ();

				//Initalize variables
				frameCounter = 0;
				animateOnUpdate = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
				frameCounter++;

				if (animateOnUpdate ) {
						AnimateCurrentFrame ();		
				}
				if (!animateOnUpdate)
						DrawContiguousLineSegments (lineWidth, incomingDataSet);

		}

		public void AnimateContiguousLineSegments (float w, List<float> d)
		{
				animateOnUpdate = true;
				//Track when we start so we can preform opperations relative to the starting frame
				animationFrame = frameCounter;
				lineWidth = w;
				incomingDataSet = d;
		}


		private void AnimateCurrentFrame ()
		{

				//Partial list to display to user
				List<float> temp = new List<float> ();
				
				//We add the first two vertexes to the array because we cannot animate a single point, we don't need to waste the time
				temp.Add (incomingDataSet [0]);
				temp.Add (incomingDataSet [1]);

				//The cursor will keep track of our current true animation frame, that is the number of key frames that have passed since the 
				//animation method was called
				cursor = ((frameCounter - animationFrame) / ANIMATIONSPEED);

				//magic
				if (cursor + 4 < incomingDataSet.Count) {

						//for every two points in the data set we add them as a pair to the temporary dataset for display
						for (int j = 2; j < cursor+2; j+=2) {
								temp.Add (incomingDataSet [j]);
								temp.Add (incomingDataSet [j + 1]);								
						}
						//We track the previous point so that we know where to animate from
						previousX = temp [temp.Count - 2];
						previousY = temp [temp.Count - 1];


					//If we are on an even frame we need to animate the second half the segment
						if (cursor % 2 == 0) {
								temp.Add ((((incomingDataSet [cursor + 2] - previousX) * 
										((float)((frameCounter % ANIMATIONSPEED - 1) / 2) / ANIMATIONSPEED)) +
										((incomingDataSet [cursor + 2] - previousX) * 0.5f))
				          				+ previousX);


							temp.Add ((((incomingDataSet [cursor + 3] - previousY) * 
				            			((float)((frameCounter % ANIMATIONSPEED - 1) / 2) / ANIMATIONSPEED)) +
										((incomingDataSet [cursor + 3] - previousY) * 0.5f)
										) + previousY);
						}
					//If we are on an odd frame we need to animate the first half of the segment
						if (cursor % 2 == 1) {
								temp.Add ((incomingDataSet [cursor + 3] - previousX) * 
										((float)((frameCounter % ANIMATIONSPEED - 1) / 2) / ANIMATIONSPEED)
				          				+ previousX);
								temp.Add ((incomingDataSet [cursor + 4] - previousY) * 
										((float)((frameCounter % ANIMATIONSPEED - 1) / 2) / ANIMATIONSPEED)
				        			  + previousY);
						}	
						DrawContiguousLineSegments (lineWidth, temp);
				} else
						animateOnUpdate = false;

		}

		public void DrawContiguousLineSegments (float width, List<float> dataSet)
		{
				Vector3 currentVector = new Vector3 (0, 0, 0); //used to gather a direction of the line to properly set the edges of the generated quad
				Vector3 up = new Vector3 (0, 0, -10); //used for cross product
				Vector3 right = new Vector3 (0, 0, 0); // used to push verts out from the lines to form quads
		
				List<Vector3> organizedData = new List<Vector3> ();
		
				List<Vector3> organizedPoints = new List<Vector3> ();
				List<Vector2> organizedPointUvs = new List<Vector2> ();
		
				//put the data in this format {v3, v3, v3, v3};
				for (int i=0; i < dataSet.Count; i+=2) {
						organizedData.Add (new Vector3 (dataSet [i], dataSet [i + 1], zDist));
						//Debug.Log ("Pair added " + givenData[i] +  givenData[i+1]);
				}
		
				//now that the list of points is in order of occurrance, gather vectors for each line segment and create quads along those segments
				//a direction is targetPosition-currentposition
				for (int i=0; i<=organizedData.Count-2; i++) {
			
						if (i < organizedData.Count - 1) {
								currentVector = new Vector3 (organizedData [i + 1].x - organizedData [i].x, organizedData [i + 1].y - organizedData [i].y, organizedData [i + 1].z - organizedData [i].z);
								//Debug.Log("Vector for section is " + currentVector);
						} else {
								//do nothing
						}
						//find right vector
						right = -Vector3.Cross (currentVector.normalized, up.normalized) * width;
			
						//add the points to the left and right of this point
						//and add the points to the left and the right of the next point(assuming it's normal matches this normal)
						organizedPoints.Add ((organizedData [i] + right));
						organizedPoints.Add ((organizedData [i] - right));
						organizedPoints.Add ((organizedData [i + 1] + right));
						organizedPoints.Add ((organizedData [i + 1] - right));
						organizedPointUvs.Add ((Vector2)(organizedData [i] + right));
						organizedPointUvs.Add ((Vector2)(organizedData [i] - right));
						organizedPointUvs.Add ((Vector2)(organizedData [i + 1] + right));
						organizedPointUvs.Add ((Vector2)(organizedData [i + 1] - right));
			
			
				}
		
				//ManageCoordinateTags (organizedData); RESTORE THIS LATER
		
				//meshfilter needs arrays to work
				Vector3[] newVerts = organizedPoints.ToArray ();
				Vector2[] newUv = organizedPointUvs.ToArray ();
	

				int[] newTriangles = new int[(organizedData.Count () - 1) * 6]; //each point needs a quad and each quad is 2 triangles made of 3 points


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
		
				mesh = new Mesh ();
				meshFilter.mesh = mesh;
		
				mesh.vertices = newVerts;
				mesh.uv = newUv;
				mesh.triangles = newTriangles;
		}

		///////////////////////////////////////////////////////WARNING DO NOT CROSS THIS POINT/////////////////////////////////////// 



		public void zoomFunction (Camera input)
		{
				if (Input.GetAxis ("Mouse ScrollWheel") > 0) { // forward
						input.orthographicSize++;
				}
				if (Input.GetAxis ("Mouse ScrollWheel") < 0) { // back
						input.orthographicSize--;
				}
		
		}




}
