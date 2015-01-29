using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class Visualization: MonoBehaviour {
	protected GameObject visMeshObject; //object w/meshes returned by the line drawer
	private static List<float> givenData;
	public GameObject utilityObject;

	public void UpdateData(){
		Debug.Log ("Update called on " + this.gameObject.ToString());
	}
}
