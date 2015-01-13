using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class VisZero : MonoBehaviour {

	public GameObject textPrefab;
	static Material lineMaterial;
	static List<float> givenData;
	static float givenXMax;
	static float givenYMax; // x and y max are used to determine camera perspective size
	static float camOrtho;  // camera ortho will tell the camera how much area to display in Unity units
	private float orginX;
	private float orginY;
	// Use this for initialization
	void Start () {
		//setup list
		givenData = new List<float>(new float[] {0,0,1,1,2,4,3,1,10,1});
		GatherMaxValues();

		//setup camera
		camOrtho = givenXMax; //fix this (it assumes that the the largest value in the data will be an X value)
		this.camera.orthographicSize = camOrtho/2;
		this.camera.transform.position = new Vector3(givenXMax/2, givenYMax/2, this.camera.transform.position.z);

		//setup test text
		DrawText(0f,0f,0f);

	}
	
	// Update is called once per frame
	void Update () {

	}
	/// <summary>
	/// Draws graph
	/// </summary>
	void OnPostRender(){

		initalizeDrawSettings ();
		drawGridLines ();
		drawLine ();
		drawShiftedPair ();

		GL.End();
	}

	void initalizeDrawSettings(){
		CreateLineMat();
		lineMaterial.SetPass( 0 );
		GL.Begin( GL.LINES );
		GL.Color( Color.white );

		}
	//draws a standard line representation of the data
	void drawLine(){

		for(int i = 0; i < givenData.Count-2; i+=2){
			GL.Vertex3(givenData[i], givenData[i+1], 0f);
			GL.Vertex3 (givenData[i+2], givenData[i+3], 0f);
		}
	
	}
	//Draws gridlines so that we can debug more easily, draw in grey then switch back to white
	void drawGridLines(){
		GL.Color (Color.grey);
		for (int i = 0; i < (int)Mathf.Ceil(givenXMax); i++) {
			GL.Vertex3((float)i, 0f,0f);
			GL.Vertex3((float)i,givenYMax,0f);			
				}
		for (int i = 0; i < (int)Mathf.Ceil(givenYMax); i++) {
			GL.Vertex3(0f, (float)i,0f);
			GL.Vertex3(givenXMax,(float)i,0f);			
		}

		GL.Color (Color.white);
		}

	//draws a shifted pair representation of the line
	void drawShiftedPair(){

		orginX = 0;
		orginY = 0;

		for(int i = 0; i < givenData.Count-2; i+=2){
			orginX = givenData[i] + orginX;
			orginY = givenData[i+1] + orginY;

			GL.Vertex3( orginX, orginY, 0f);
			GL.Vertex3 ((givenData[i+2]+orginX), (givenData[i+3]+orginY), 0f);

			Vector3 point1 = new Vector3(orginX, orginY, 0f);
			Vector3 point2=  new Vector3((givenData[i+2]+orginX), (givenData[i+3]+orginY), 0f);

			//Experimental code attempting to draw arrows
			GL.End();
			drawArrowHead(point1,point2);
			GL.Begin(GL.LINES);
			GL.Color (Color.white);
		}

	}

	//Tony's arrow drawing method 
	void drawArrowHead(Vector3 point1, Vector3 point2)
	{
		float arrowHeadSize = 0.3f;
		
		//vector along line
		Vector3 vec = new Vector3();
		vec.x = point2.x - point1.x;
		vec.y = point2.y - point1.y;
		vec.z = point2.z - point1.z;
		
		vec.Normalize();
		
		//Perpendicular vector
		Vector3 perp1 = new Vector3();
		Vector3 perp2 = new Vector3();
		perp1.Set(-vec.y, vec.x, 0);
		perp2.Set(vec.y, -vec.x, 0);
		
		//Point behind endpoint
		Vector3 point3 = new Vector3();
		point3 = point2 - (vec * arrowHeadSize);
		
		
		//Point on of the corners of the triangle
		Vector3 point4 = new Vector3();
		Vector4 point5 = new Vector3();
		point4 = point3 + (perp2 * arrowHeadSize);
		point5 = point3 + (perp1 * arrowHeadSize);
		
		
		GL.Begin(GL.TRIANGLES);
		GL.Color (Color.red);
		GL.Vertex3(point2.x, point2.y, 0.0f);
		GL.Vertex3(point4.x, point4.y, 0.0f);
		GL.Vertex3(point5.x, point5.y, 0.0f);
		GL.End();
		
	}


	static void CreateLineMat(){
		if(!lineMaterial){
			lineMaterial = new Material( "Shader \"Lines/Colored Blended\" {" +
			                            "SubShader { Pass { " +
			                            "    Blend SrcAlpha OneMinusSrcAlpha " +
			                            "    ZWrite Off Cull Off Fog { Mode Off } " +
			                            "    BindChannels {" +
			                            "      Bind \"vertex\", vertex Bind \"color\", color }" +
			                            "} } }" );
		}
		
		lineMaterial.hideFlags = HideFlags.HideAndDontSave;
		lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
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

	void DrawText(float x, float y, float z){
		GameObject originText = (GameObject)GameObject.Instantiate(textPrefab);
		TextMesh t = originText.GetComponent<TextMesh>();
		t.text = "("+x+","+y+")"; //sets the actual text
		originText.transform.position = new Vector3(x,y,z);
		t.name = "("+x+","+y+")"; //sets the name of the gameobject in unity's viewer
	}
}
