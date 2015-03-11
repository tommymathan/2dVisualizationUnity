using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ShiftedCordinateDrawer : MonoBehaviour
{
	
	private static int ANIMATIONSPEED = 10; //Determines the number of frames it takes to render a segment of the line
	
	static Material lineMaterial;
	static float givenXMax;
	static float givenYMax; // x and y max are used to determine camera perspective size
	static float camOrtho;  // camera ortho will tell the camera how much area to display in Unity units
	private float orginX;
	private float orginY;
	private List <Color32> col;
	
	
	//////////////////////////////////////Debug Variables/////////////////
	private List <List <float>> dataSet = new List <List <float>> ();
	private int currentFrame;
	//public GUIText currentFText;
	static List<float> givenData;
	
	//////////////////////////////////////////////////////////////////////	
	
	
	
	// Initalize camera and values
	void Start ()
	{
		////////////////////////Debug code///////////////////////
		givenData = new List<float> (new float[] {0,0,1,1,2,4,3,1,10,1});
		for (int i = 0; i < 100; i++) {
			givenData.Add (Random.value);
		}
		
		List<float> dataList1 = new List<float> (new float[] {0,0,1,1,2,4,3,1,10,1});
		List<float> dataList2 = new List<float> (new float[] {0,1,2,3,4,5,6,3,14,6});
		List<float> dataList3 = new List<float> (new float[] {0,3,5,3,3,1,6,1,2,8});
		List<float> dataList4 = new List<float> (new float[] {1,1,7,3,4,2,5,3,7,3});
		
		
		dataSet.Add (dataList1);
		dataSet.Add (dataList2);
		dataSet.Add (dataList3);
		dataSet.Add (dataList4);
		////////////////////////////////////////////////////////////////////////
		
		col = new List<Color32> ();
		setColorsForNumberOfVectors ();				
		currentFrame = 0;
		//currentFText.text = "Current Frame: " + currentFrame.ToString ();
		GatherMaxValues ();		
		//setup camera
		camOrtho = givenXMax; //fix this (it assumes that the the largest value in the data will be an X value)
		this.GetComponent<Camera>().orthographicSize = camOrtho / 2;
		this.GetComponent<Camera>().transform.position = new Vector3 (givenXMax / 2, givenYMax / 2, this.GetComponent<Camera>().transform.position.z);
		
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Track current frame for debugging purposed REMOVE FOR SHIPPING VERSION
		currentFrame++;
		//currentFText.text = "Current Frame: " + currentFrame.ToString ();
		
		//Zoom functionality
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) { // forward
			this.GetComponent<Camera>().orthographicSize++;
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) { // back
			this.GetComponent<Camera>().orthographicSize--;
		}
	}
	
	
	//Drawing of lines is done in this method
	void OnPostRender ()
	{
		
		
		beginDraw ();
		drawGridLines ();
		splitAndDrawIndividualVectors ();
		//End drawing
		GL.End ();
	}
	
	void splitAndDrawIndividualVectors ()
	{
		int colorIndex = 0;
		foreach (List<float> vector in dataSet) {		
			animatedDrawShiftedPair (vector, col [colorIndex]);
			colorIndex++;
		}
		
		
		
	}
	//Set up drawing area, create the line material then detemine the drawing shape then color
	void beginDraw ()
	{
		CreateLineMat ();
		lineMaterial.SetPass (0);
		GL.Begin (GL.LINES);
		GL.Color (Color.white);
		
	}
	
	
	//draws a standard line representation of the data
	void drawLine (int cursor)
	{
		for (int i = 0; i < cursor; i+=2) {
			GL.Vertex3 (givenData [i], givenData [i + 1], 0f);
			GL.Vertex3 (givenData [i + 2], givenData [i + 3], 0f);
		}
		
		
		
	}
	//Draws gridlines so that we can debug more easily, draw in grey then switch back to white
	void drawGridLines ()
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
	
	//draws a shifted pair representation of the line
	//re-writing to draw small number of lines at a time
	
	void drawShiftedPair (int cursor)
	{
		
		orginX = 0;
		orginY = 0;
		
		for (int i = 0; i < cursor; i+=2) {
			orginX = givenData [i] + orginX;
			orginY = givenData [i + 1] + orginY;
			
			GL.Vertex3 (orginX, orginY, 0f);
			GL.Vertex3 ((givenData [i + 2] + orginX), (givenData [i + 3] + orginY), 0f);
			
			Vector3 point1 = new Vector3 (orginX, orginY, 0f);
			Vector3 point2 = new Vector3 ((givenData [i + 2] + orginX), (givenData [i + 3] + orginY), 0f);
			
			//Experimental code attempting to draw arrows
			GL.End ();
			drawArrowHead (point1, point2);
			GL.Begin (GL.LINES);
			GL.Color (Color.white);
			
		}
		
	}
	
	void drawShiftedPair (int cursor, List<float> currentVector, Color c)
	{
		
		orginX = 0;
		orginY = 0;
		
		for (int i = 0; i < cursor; i+=2) {
			orginX = currentVector [i] + orginX;
			orginY = currentVector [i + 1] + orginY;
			
			GL.Vertex3 (orginX, orginY, 0f);
			GL.Vertex3 ((currentVector [i + 2] + orginX), (currentVector [i + 3] + orginY), 0f);
			
			Vector3 point1 = new Vector3 (orginX, orginY, 0f);
			Vector3 point2 = new Vector3 ((currentVector [i + 2] + orginX), (currentVector [i + 3] + orginY), 0f);
			
			//Experimental code attempting to draw arrows
			GL.End ();
			drawArrowHead (point1, point2);
			GL.Begin (GL.LINES);
			GL.Color (c);
			
		}
		
	}
	//Tony's arrow drawing method 
	void drawArrowHead (Vector3 point1, Vector3 point2)
	{
		float arrowHeadSize = 0.3f;
		
		//vector along line
		Vector3 vec = new Vector3 ();
		vec.x = point2.x - point1.x;
		vec.y = point2.y - point1.y;
		vec.z = point2.z - point1.z;
		
		vec.Normalize ();
		
		//Perpendicular vector
		Vector3 perp1 = new Vector3 ();
		Vector3 perp2 = new Vector3 ();
		perp1.Set (-vec.y, vec.x, 0);
		perp2.Set (vec.y, -vec.x, 0);
		
		//Point behind endpoint
		Vector3 point3 = new Vector3 ();
		point3 = point2 - (vec * arrowHeadSize);
		
		
		//Point on of the corners of the triangle
		Vector3 point4 = new Vector3 ();
		Vector4 point5 = new Vector3 ();
		point4 = point3 + (perp2 * arrowHeadSize);
		point5 = point3 + (perp1 * arrowHeadSize);
		
		
		GL.Begin (GL.TRIANGLES);
		GL.Color (Color.red);
		GL.Vertex3 (point2.x, point2.y, 0.0f);
		GL.Vertex3 (point4.x, point4.y, 0.0f);
		GL.Vertex3 (point5.x, point5.y, 0.0f);
		GL.End ();
		
	}
	/////////////////////////UTILITY Non-Drawing FUNCTIONS///////////////////////////////////////////
	
	//Draw the shifted pair graph at 1 line per ANIMATIONSPEED frames(E.g. 1 line per 10 frames)
	//To do this we draw partial graphs untill we have the whole graph
	void animatedDrawShiftedPair ()
	{
		if ((currentFrame / ANIMATIONSPEED) < givenData.Count - 2) {
			drawShiftedPair (currentFrame / ANIMATIONSPEED);
		} else
			drawShiftedPair (givenData.Count - 2);		
	}
	
	void animatedDrawShiftedPair (List<float> currentVector, Color c)
	{	
		if (c != null)
			GL.Color (c);
		if ((currentFrame / ANIMATIONSPEED) < currentVector.Count - 2) {
			drawShiftedPair (currentFrame / ANIMATIONSPEED, currentVector, c);
		} else
			drawShiftedPair (currentVector.Count - 2, currentVector, c);	
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
	
	//Find the maximum x and y that we will reach durring the shifted pairs by adding all x and y values together
	//The sum of each axis's values represents the maximum value for each axis
	void GatherMaxValues ()
	{
		givenXMax = 0;
		givenYMax = 0;
		
		//assumuming x's are 0->even numbered items and y's are odd numbered items
		for (int i = 0; i< givenData.Count; i++) {
			if (i % 2 == 0) {
				givenXMax += givenData [i];
			} else {
				givenYMax += givenData [i];
			}
		}
		
		Debug.Log ("The max X value in this list is: " + givenXMax + "" +
		           "\nThe max Y value in this list is: " + givenYMax);
	}
	
	//Creates random colors for each vector, may have to increase the variance if too many vectors are the
	//same color
	void setColorsForNumberOfVectors ()
	{
		for (int i = 0; i < dataSet.Count; i++) {
			Random.Range (0, 1);
			col.Add (new Color (Random.value, Random.value, 
			                    Random.value, 255));
		}
	}
	
}