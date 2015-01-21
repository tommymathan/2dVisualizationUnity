using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class Visualization: MonoBehaviour {
	private static List<float> givenData;
	public GameObject utilityObject;

	public void UpdateData(){
		Debug.Log ("Update called on " + this.gameObject.ToString());
	}
}
