using UnityEngine;
using System.Collections;

public class ScreenLines : MonoBehaviour {
	private static Material lineMaterial;
	private float lBound;
	private float rBound;
	private float bBound;
	private float tBound;
	private float interval;
	private Camera thisCamera;

	private float demarkInterval; //used to choose how often a demarkation is drawn
	private Color normalColor;
	private Color originColor;
	private Color demarkationColor;

	// Use this for initialization
	void Start () {
		interval = 1;
		demarkInterval = 5f;
		Debug.Log ("Graph interval: " + interval + "|Demarkation Interval: " +demarkInterval);
		lBound = -10.0f;
		rBound = 10.0f;
		bBound = -10.0f;
		tBound = 10.0f;
		thisCamera = gameObject.GetComponentInParent<Camera>();
		normalColor = Color.grey;
		originColor = Color.blue;
		demarkationColor = Color.magenta;
	}
	
	// Update is called once per frame
	void Update(){
		lBound = Mathf.Floor( thisCamera.ViewportToWorldPoint(new Vector3(0,0,GetComponent<Camera>().nearClipPlane)).x);
		rBound = Mathf.Ceil(thisCamera.ViewportToWorldPoint(new Vector3(1,0,GetComponent<Camera>().nearClipPlane)).x);
		bBound = Mathf.Floor(thisCamera.ViewportToWorldPoint(new Vector3(0,0,GetComponent<Camera>().nearClipPlane)).y);
		tBound = Mathf.Ceil(thisCamera.ViewportToWorldPoint(new Vector3(0,1,GetComponent<Camera>().nearClipPlane)).y);
	}
	
	void OnPostRender () {
		
		CreateLineMat ();
		lineMaterial.SetPass (0);
		GL.Begin (GL.LINES);
		
		GL.Color (originColor);
		
		for (float tempLBound = lBound; tempLBound <= rBound; tempLBound++) {
			if((tempLBound!=0.0f) && !(tempLBound%demarkInterval==0) && (tempLBound%interval==0)){
				GL.Color (normalColor);
			}
			else if((tempLBound!=0.0f) && (tempLBound%demarkInterval==0)){
				GL.Color (demarkationColor);
			}
			else if(!(tempLBound%interval==0)){
				GL.Color (Color.clear);
			}
			else{
				GL.Color (originColor);
			}
			GL.Vertex3 (tempLBound, bBound, 0f);
			GL.Vertex3 (tempLBound, tBound, 0f);
		}
		
		for (float tempBBound = bBound; tempBBound <= tBound; tempBBound++) {
			if((tempBBound!=0.0f) && !(tempBBound%demarkInterval==0) && (tempBBound%interval==0)){
				GL.Color (normalColor);
			}
			else if((tempBBound!=0.0f) && (tempBBound%demarkInterval==0)){
				GL.Color (demarkationColor);
			}
			else if(!(tempBBound%interval==0)){
				GL.Color (Color.clear);
			}
			else{
				GL.Color (originColor);
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
			                             "SubShader { Tags { \"Queue\" = \"Transparent+2\" \"RenderType\"=\"Transparent\"}" +
			                             "Pass {" +
			                             "   BindChannels { Bind \"Color\",color }" +
			                             "   Blend SrcAlpha OneMinusSrcAlpha" +
			                             "   ZWrite Off Cull Off Fog { Mode Off }" +
			                             "} } }");
		}

		lineMaterial.hideFlags = HideFlags.HideAndDontSave;
		lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
	}
}
