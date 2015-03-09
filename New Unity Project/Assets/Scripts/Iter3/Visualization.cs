using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class Visualization: MonoBehaviour {
	
	
	private static List<float> givenData;
	public GameObject vectorTemplate;
	public GameObject[] meshContainmentArray;
	public string dataPathVar;
	
	protected static Material lineMaterial;
	protected static int DRAWINGSPEED = 100;
	
	protected Mesh mesh;
	
	protected bool animateOnLoad;
	protected bool collidersLoaded;
	protected bool dataUpdated; //tells the cam when to redraw stuff based on an update in global settings
	protected bool animationInProgress;
	
	protected int numberIncomingVectors;
	protected int numberValsPerVector;
	protected int animationCounter;
	protected int layer;
	protected int vizID;
	
	protected Color[] colors;
	protected Color visColor;
	
	protected GameObject globalSettingsObject;
	protected HashSet<int> animationQueue;
	protected DrawUtil[] drawingUtility;
	
	public void UpdateData(){
		
		
		Debug.Log ("Update called on " + this.gameObject.ToString());
	}
	
	public virtual void UpdateData(DataObject dataFromFile){
		
		Debug.Log ("Update called on " + this.gameObject.ToString());
	}
	public virtual void addLineToAnimate(HashSet<int> val){
		//Debug.Log ("Animation added to " + this.gameObject.ToString());
	}
	public void AddCameraControls(){
		gameObject.AddComponent<MouseHandler> ();
	}

	public virtual void updateAnimationSpeed(int val)
	{
		DRAWINGSPEED = val;
	}
}

