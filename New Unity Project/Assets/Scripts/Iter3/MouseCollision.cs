using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseCollision : MonoBehaviour {

	public List<GameObject> selection;
	public List<GameObject> hoverList;
	float redShift = 0.4f;
	float blueShift = 0.4f;
	float greenShift = 0.4f;

	// Use this for initialization
	void Start () {
		selection = new List<GameObject>();
		hoverList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			if(hoverList.Count==0){//if you clicked on nothing, assume that the user doesn't want to have a selection highlighted anymore
				selection.Clear();
			}
			else{
				foreach(GameObject go in hoverList){
					if(!selection.Contains(go)){
						selection.Add(go);
					}
					RandomizeSelectionColors();
				}
				selection.Clear();
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		//Debug.Log ("mouse hit: " +other.ToString());
		hoverList.Add(other.gameObject);
		Color previousColor = other.gameObject.GetComponent<MeshRenderer>().material.color;
		previousColor.r += redShift;
		previousColor.b += blueShift;
		previousColor.g += greenShift;
		ChangeColor(other.gameObject, previousColor);
	}

	void OnTriggerExit(Collider other){
		hoverList.Remove(other.gameObject);
		Color previousColor = other.gameObject.GetComponent<MeshRenderer>().material.color;
		previousColor.r -= redShift;
		previousColor.b -= blueShift;
		previousColor.g -= greenShift;
		ChangeColor(other.gameObject, previousColor);
		//Debug.Log(selection.Count);
	}

	void ChangeColor(GameObject passedObject, Color color){
		Material passedMaterial = passedObject.GetComponent<MeshRenderer>().material;
		passedMaterial.color = color;
	}

	void RandomizeSelectionColors(){
		foreach(GameObject go in selection){
			ChangeColor(go, new Color(Random.Range(0.1f, 0.8f),Random.Range(0.1f, 0.8f),Random.Range(0.1f, 0.8f)));
			Color previousColor = go.GetComponent<MeshRenderer>().material.color;
			previousColor.r += redShift;
			previousColor.b += blueShift;
			previousColor.g += greenShift;
			go.GetComponent<MeshRenderer>().material.color = previousColor;
		}
	}
}
