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
				if(Input.GetKey(KeyCode.LeftShift)){ //if they are holding shift, keep adding selected lines to the selectionlist
					foreach(GameObject go in hoverList){
							if(!selection.Contains(go)){
								selection.Add(go);
							}
					}
				}
				else{//if they weren't holding shift then clear the selection and treat this last click as them choosing everything that they want to work with
					selection.Clear();
					foreach(GameObject go in hoverList){
						if(!selection.Contains(go)){
							selection.Add(go);
						}
					}
				}
			}
		}
		LineSelectedChangeColor();
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("mouse hit: " +other.ToString());
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

	void LineSelectedChangeColor(){
		foreach (GameObject go in selection) {
			GlobalSettings gs = GameObject.FindGameObjectWithTag ("GlobalSettingsObject").GetComponent<GlobalSettings> ();
			Material passedMaterial = go.GetComponent<MeshRenderer> ().material;
			passedMaterial.color = new Color (gs.gLineR, gs.gLineG, gs.gLineB);
			//Debug.Log ("Red: " + gs.gLineR + "\nGreen" + gs.gLineG + "\nBlue" + gs.gLineB);
			
		}
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
