using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class VisZero : MonoBehaviour {
	
	public GameObject textPrefab;
	public GameObject visObject;
	private Renderer meshRenderer;
	private MeshFilter meshFilter;
	private Mesh mesh;
	static Material lineMaterial;
	static List<float> givenData;
	static List<GameObject> meshCoords;
	static float givenXMax;
	static float givenYMax; // x and y max are used to determine camera perspective size
	static float camOrtho; // camera ortho will tell the camera how much area to display in Unity units
	public float lineWidth;
	public const int zDist = 10;
	
	// Use this for initialization
	void Start () {
		//setup list
		givenData = new List<float>(new float[] {0,0,1,1,3,2,5,2,6,0,7,0});
		SetupCamera();
		
		//setup test text
		CreateTextMesh(0f,0f,0f);
		
		//build line
		lineWidth = 0.05f;
		meshRenderer = visObject.GetComponent<MeshRenderer>();
		meshFilter = visObject.GetComponent<MeshFilter>();
		Debug.Log ("MeshRenderer is " + meshRenderer);
		meshCoords = new List<GameObject>();
		CreateMesh(lineWidth, givenData);
	}
	
	// Update is called once per frame
	void Update () {
		CheckKeys();
	}
	
	void OnPostRender(){
		GL_Lines(givenData);
	}
	
	void CreateMesh(float width, List<float> dataSet){
		Vector3 currentVector = new Vector3(0,0,0); //used to gather a direction of the line to properly set the edges of the generated quad
		Vector3 up = new Vector3(0,0,-10); //used for cross product
		Vector3 right = new Vector3(0,0,0); // used to push verts out from the lines to form quads
		
		List<Vector3> organizedData = new List<Vector3>();
		
		List<Vector3> organizedPoints = new List<Vector3>();
		List<Vector2> organizedPointUvs = new List<Vector2>();
		
		//put the data in this format {v3, v3, v3, v3};
		for(int i=0; i < dataSet.Count; i+=2){
			organizedData.Add(new Vector3(dataSet[i], dataSet[i+1], zDist));
			//Debug.Log ("Pair added " + givenData[i] + givenData[i+1]);
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
		
		ManageCoordinateTags (organizedData);
		
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
	
	static void CreateLineMat(){
		if(!lineMaterial){
			lineMaterial = new Material( "Shader \"Lines/Colored Blended\" {" +
			                            "SubShader { Pass { " +
			                            " Blend SrcAlpha OneMinusSrcAlpha " +
			                            " ZWrite Off Cull Off Fog { Mode Off } " +
			                            " BindChannels {" +
			                            " Bind \"vertex\", vertex Bind \"color\", color }" +
			                            "} } }" );
		}
		
		lineMaterial.hideFlags = HideFlags.HideAndDontSave;
		lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
	}
	
	GameObject CreateTextMesh(float x, float y, float z){
		GameObject originText = (GameObject)GameObject.Instantiate(textPrefab);
		TextMesh t = originText.GetComponent<TextMesh>();
		t.text = "("+x+","+y+")"; //sets the actual text
		originText.transform.position = new Vector3(x,y,z);
		t.name = "("+x+","+y+")"; //sets the name of the gameobject in unity's viewer
		return originText;
	}
	
	void GatherMaxValues(){
		givenXMax = float.MinValue;
		givenYMax = float.MinValue;
		
		//assumuming x's are 0->even numbered items and y's are odd numbered items
		for(int i = 0; i< givenData.Count; i++){
			if(i % 2 == 0){
				if(givenData[i] > givenXMax){
					givenXMax = givenData[i];
				}
			}
			else{
				if(givenData[i] > givenYMax){
					givenYMax = givenData[i];
				}
			}
		}
		
		Debug.Log ("The max X value in this list is: " + givenXMax + "" +
		           "\nThe max Y value in this list is: " + givenYMax);
	}
	
	void GL_Lines(List<float> dataSet){
		CreateLineMat();
		lineMaterial.SetPass( 0 );
		GL.Begin( GL.LINES );
		GL.Color( Color.white );
		for(int i = 0; i < dataSet.Count-2; i+=2){
			GL.Vertex3(dataSet[i], dataSet[i+1], 0f);
			GL.Vertex3 (dataSet[i+2], dataSet[i+3], 0f);
		}
		GL.End();
	}
	
	void CheckKeys(){
		if(Input.GetKeyDown(KeyCode.Space)){
			Debug.Log ("Changing Dataset");
			givenData = new List<float>();
			for(int i = 0; i<7; i++){
				givenData.Add(i);
				givenData.Add(Random.Range(0,5));
			}
			CreateMesh(lineWidth, givenData);
			SetupCamera();
		}
	}
	void ManageCoordinateTags(List<Vector3> given){
		//coordinate addition/deletion
		Debug.Log ("MeshCoords length " + meshCoords.Count());
		
		foreach (GameObject go in meshCoords) {
			Destroy (go);
		}
		meshCoords = new List<GameObject>();
		foreach(Vector3 point in given){
			GameObject coordObject = CreateTextMesh(point.x,point.y,point.z);
			meshCoords.Add(coordObject);
		}
	}
	
	void SetupCamera(){
		GatherMaxValues();
		
		//setup camera
		camOrtho = givenXMax; //fix this (it assumes that the the largest value in the data will be an X value), it assumes that the lowest x value is 0
		this.camera.orthographicSize = camOrtho/2;
		this.camera.transform.position = new Vector3(givenXMax/2, givenYMax/2, this.camera.transform.position.z);
	}
}