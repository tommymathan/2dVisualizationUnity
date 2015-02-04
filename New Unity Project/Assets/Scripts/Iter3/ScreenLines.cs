using UnityEngine;
using System.Collections;

public class ScreenLines : MonoBehaviour {
	private static Material lineMaterial;
	private float lBound;
	private float rBound;
	private float bBound;
	private float tBound;
	private Camera thisCamera;
	
	// Use this for initialization
	void Start () {
		lBound = -10.0f;
		rBound = 10.0f;
		bBound = -10.0f;
		tBound = 10.0f;
		
		thisCamera = gameObject.GetComponentInParent<Camera>();
	}
	
	// Update is called once per frame
	void Update(){
		lBound = Mathf.Floor( thisCamera.ViewportToWorldPoint(new Vector3(0,0,camera.nearClipPlane)).x);
		rBound = Mathf.Ceil(thisCamera.ViewportToWorldPoint(new Vector3(1,0,camera.nearClipPlane)).x);
		bBound = Mathf.Floor(thisCamera.ViewportToWorldPoint(new Vector3(0,0,camera.nearClipPlane)).y);
		tBound = Mathf.Ceil(thisCamera.ViewportToWorldPoint(new Vector3(0,1,camera.nearClipPlane)).y);
	}
	
	void OnPostRender () {
		
		CreateLineMat ();
		lineMaterial.SetPass (0);
		GL.Begin (GL.LINES);
		
		GL.Color (Color.blue);
		
		for (float tempLBound = lBound; tempLBound <= rBound; tempLBound++) {
			if(tempLBound!=0.0f){
				GL.Color (Color.grey);
			}
			else{
				GL.Color (Color.blue);
			}
			GL.Vertex3 (tempLBound, bBound, 0f);
			GL.Vertex3 (tempLBound, tBound, 0f);
		}
		
		for (float tempBBound = bBound; tempBBound <= tBound; tempBBound++) {
			if(tempBBound != 0.0f){
				GL.Color (Color.grey);
			}
			else{
				GL.Color (Color.blue);
			}
			GL.Vertex3 (lBound, tempBBound, 0f);
			GL.Vertex3 (rBound, tempBBound, 0f);
		}
		
		GL.End ();
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
