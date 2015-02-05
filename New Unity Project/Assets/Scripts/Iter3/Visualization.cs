using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class Visualization: MonoBehaviour {


	private static List<float> givenData;
	public GameObject vectorTemplate;
	public GameObject[] meshContainmentArray;
	public string dataPathVar;

	public void UpdateData(){
		Debug.Log ("Update called on " + this.gameObject.ToString());
	}

	public virtual void UpdateData(DataObject dataFromFile){

		Debug.Log ("Update called on " + this.gameObject.ToString());
	}

	public void AddCameraControls(){
		gameObject.AddComponent<MouseHandler> ();
	}
}

