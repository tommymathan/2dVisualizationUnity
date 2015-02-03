using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class Visualization: MonoBehaviour {


	private static List<float> givenData;
	public GameObject vectorTemplate;
	public GameObject[] meshContainmentArray;
	
	public void UpdateData(){
		Debug.Log ("Update called on " + this.gameObject.ToString());
	}

	
	public void createMeshMonster(){
		vectorTemplate = new GameObject ();
		vectorTemplate.AddComponent<MeshFilter> ();
		vectorTemplate.AddComponent<MeshRenderer> ();
		vectorTemplate.AddComponent<StayPut> ();
		vectorTemplate = (GameObject) Instantiate(vectorTemplate, gameObject.transform.position, Quaternion.identity);
		vectorTemplate.transform.SetParent (gameObject.transform);
		vectorTemplate.name = "ParentVector";
	}
}

