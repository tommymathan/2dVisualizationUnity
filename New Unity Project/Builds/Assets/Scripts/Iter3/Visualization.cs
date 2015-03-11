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

	public void updateVisMethod(){

				if (animateOnLoad) {
						animationCounter++;		
						if (animationInProgress)
								animateVectorsInQueue ();
				} 
	
				if (dataUpdated) {
						for (int i = 0; i < numberIncomingVectors; i++) {
								meshContainmentArray [i].GetComponent<MeshFilter> ().sharedMesh.Clear ();
								meshContainmentArray [i].GetComponent<MeshFilter> ().mesh.Clear ();
								meshContainmentArray [i].GetComponent<MeshFilter> ().mesh 
				= drawingUtility [i].filteredCoordinates ();
						}
		
						if (collidersLoaded == false) {
								Debug.Log ("updating vector colliders");
								GameObject[] vectorList = GameObject.FindGameObjectsWithTag ("vector");
								for (int i = 0; i<vectorList.Length; i++) {
										DrawUtil.ManageVectorColliders (vectorList [i]);
								}
								collidersLoaded = true;
						}
						dataUpdated = false;
				}
	
				if (Input.GetKeyDown (KeyCode.LeftAlt)) {
						Debug.Log ("Shit! You pressed Left Alt!");
				}
		}

	protected virtual void animateVectorsInQueue(){
				int[] animation = animationQueue.ToArray ();
				if (animationQueue.Count > 0) {
						for (int j = 0; j < animationQueue.Count; j++) {
								//Debug.Log ("we are now exectuing!" + animationQueue.Count + "<size of queue Counter > " + animationCounter);
								meshContainmentArray [animation [j]].GetComponent<MeshFilter> ().sharedMesh.Clear ();
								meshContainmentArray [animation [j]].GetComponent<MeshFilter> ().mesh.Clear ();
								meshContainmentArray [animation [j]].GetComponent<MeshFilter> ().mesh 
					= drawingUtility [animation [j]].AnimateCurrentFrame (animationCounter, meshContainmentArray [animation [j]]);
				
						}
				}
				if (animationCounter >= (DRAWINGSPEED * (numberValsPerVector / 2))-DRAWINGSPEED) {
						animationInProgress = false;
						animationQueue.Clear ();
				}
		}


}

