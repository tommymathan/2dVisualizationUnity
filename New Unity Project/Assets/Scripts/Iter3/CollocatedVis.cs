using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CollocatedVis : Visualization {


	//Debug code
	static float givenXMax;
	static float givenYMax; // x and y max are used to determine camera perspective size
	static Material lineMaterial;
	//////////////////////////////

	// Use this for initialization
	void Start () {
		//Debug code
		givenXMax = 10;
		givenYMax = 10;
		List<float> test = new List<float> (new float[] {0,0,1,1,3,2,5,2,6,0,7,0});
		//for (int i = 0; i < 100; i++) {
		//	test.Add ((Random.value *10) % 10);
		//}
		//utilityObject.GetComponent<Utility>().AnimateContiguousLineSegments(.02f, test);
		visMeshObject = utilityObject.GetComponent<Utility>().DrawContiguousLineSegments(0.02f, test);
		//////////////////////////

		//gameObject.GetComponent<Camera> ().orthographicSize = 5;
		//gameObject.transform.position = new Vector3 (2f, 2f, gameObject.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		//Zoom functionality
		utilityObject.GetComponent<Utility> ().zoomFunction (this.camera);
	}

















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
