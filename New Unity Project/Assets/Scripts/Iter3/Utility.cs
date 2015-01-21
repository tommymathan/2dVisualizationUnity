using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Utility : MonoBehaviour {
	private Renderer meshRenderer;
	private MeshFilter meshFilter; // used for mesh rendering
	private Mesh mesh; //the actual mesh in space
	private int zDist; //hack to move the camera back for easier debugging

	// Use this for initialization
	void Start () {
		meshFilter = gameObject.GetComponent<MeshFilter> ();
		meshRenderer = gameObject.GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DrawContiguousLineSegments(float width, List<float> dataSet){
		Vector3 currentVector = new Vector3(0,0,0); //used to gather a direction of the line to properly set the edges of the generated quad
		Vector3 up = new Vector3(0,0,-10); //used for cross product
		Vector3 right = new Vector3(0,0,0); // used to push verts out from the lines to form quads
		
		List<Vector3> organizedData = new List<Vector3>();
		
		List<Vector3> organizedPoints = new List<Vector3>();
		List<Vector2> organizedPointUvs = new List<Vector2>();
		
		//put the data in this format {v3, v3, v3, v3};
		for(int i=0; i < dataSet.Count; i+=2){
			organizedData.Add(new Vector3(dataSet[i], dataSet[i+1], zDist));
			//Debug.Log ("Pair added " + givenData[i] +  givenData[i+1]);
		}
		
		//now that the list of points is in order of occurrance, gather vectors for each line segment and create quads along those segments
		//a direction is targetPosition-currentposition
		for(int i=0; i<=organizedData.Count-2; i++){
			
			if(i<organizedData.Count-1){
				currentVector = new Vector3(organizedData[i+1].x-organizedData[i].x, organizedData[i+1].y-organizedData[i].y, organizedData[i+1].z-organizedData[i].z);
				//Debug.Log("Vector for section is " + currentVector);
			}
			else{
				//do nothing
			}
			//find right vector
			right = -Vector3.Cross(currentVector.normalized, up.normalized)*width;
			
			//add the points to the left and right of this point
			//and add the points to the left and the right of the next point(assuming it's normal matches this normal)
			organizedPoints.Add((organizedData[i]+right));
			organizedPoints.Add((organizedData[i]-right));
			organizedPoints.Add((organizedData[i+1]+right));
			organizedPoints.Add((organizedData[i+1]-right));
			organizedPointUvs.Add((Vector2)(organizedData[i]+right));
			organizedPointUvs.Add((Vector2)(organizedData[i]-right));
			organizedPointUvs.Add((Vector2)(organizedData[i+1]+right));
			organizedPointUvs.Add((Vector2)(organizedData[i+1]-right));
			
			
		}
		
		//ManageCoordinateTags (organizedData); RESTORE THIS LATER
		
		//meshfilter needs arrays to work
		Vector3[] newVerts = organizedPoints.ToArray();
		Vector2[] newUv = organizedPointUvs.ToArray();
		int[] newTriangles = new int[(organizedData.Count()-1)*6]; //each point needs a quad and each quad is 2 triangles made of 3 points
		
		//this is messy and stupid, is there an easier way?
		int firstIndex=0;
		int secondIndex=1;
		for(int i = 0; i<=newTriangles.Length-6; i+=6){
			newTriangles[i]=firstIndex;
			newTriangles[i+1] = ++firstIndex;
			newTriangles[i+2] = ++firstIndex;
			firstIndex+=2;
			newTriangles[i+3]= secondIndex;
			newTriangles[i+4]= secondIndex+2;
			newTriangles[i+5] = secondIndex+1;
			secondIndex+=4;
		}
		
		mesh = new Mesh();
		meshFilter.mesh = mesh;
		
		mesh.vertices = newVerts;
		mesh.uv = newUv;
		mesh.triangles = newTriangles;
	}

}
