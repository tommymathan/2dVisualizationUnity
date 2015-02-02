using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class Visualization: MonoBehaviour {
	protected GameObject visMeshObject; //object w/meshes returned by the line drawer
	private static List<float> givenData;
	public GameObject utilityObject;
	public GameObject MeshHolder;

	public void UpdateData(){
		Debug.Log ("Update called on " + this.gameObject.ToString());
	}

	public void CreateMeshMonster(){
		MeshHolder = new GameObject ();
		MeshHolder.AddComponent<MeshFilter> ();
		MeshHolder.AddComponent<MeshRenderer> ();
		MeshHolder.AddComponent<StayPut> ();
		MeshHolder = (GameObject) Instantiate(MeshHolder, gameObject.transform.position, Quaternion.identity);
		MeshHolder.transform.SetParent (gameObject.transform);
		MeshHolder.name = ""+gameObject.GetHashCode();
	}
}
