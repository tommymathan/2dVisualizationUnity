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

	void OnPostRender(){
		CreateLineMat();
		lineMaterial.SetPass( 0 );
		GL.Begin( GL.LINES );
		GL.Color( Color.white );
		for(int i = 0; i < givenData.Count-2; i+=2){
			GL.Vertex3(givenData[i], givenData[i+1], 0f);
			GL.Vertex3 (givenData[i+2], givenData[i+3], 0f);
		}
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
