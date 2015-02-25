using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseCollision : MonoBehaviour {

	public List<GameObject> selection;
	public List<GameObject> hoverList;
	GameObject globalSettingsObject;
	float redVal;
	float blueVal;
	float greenVal;
	float redShift;
	float blueShift;
	float greenShift;

	// Use this for initialization
	void Start () {
		globalSettingsObject = GameObject.FindGameObjectWithTag ("GlobalSettings");
		selection = new List<GameObject>();
		hoverList = new List<GameObject>();
		redVal = globalSettingsObject.GetComponent<GlobalSettings>().lineR;
		blueVal = globalSettingsObject.GetComponent<GlobalSettings>().lineB;
		greenVal = globalSettingsObject.GetComponent<GlobalSettings>().lineG;
		Debug.Log (redVal);
		redShift = 0.5f;
		blueShift = 0.5f;
		greenShift = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		HandleSelection ();

	}

	void OnTriggerEnter(Collider other) {
		//Debug.Log ("mouse hit: " +other.ToString());
			hoverList.Add (other.gameObject);
			Color previousColor = other.gameObject.GetComponent<MeshRenderer> ().material.color;
			previousColor.r -= redShift;
			previousColor.b -= blueShift;
			previousColor.g -= greenShift;
			ChangeColor (other.gameObject, previousColor);
	}

	void OnTriggerExit(Collider other){
		hoverList.Remove(other.gameObject);
		Color previousColor = other.gameObject.GetComponent<MeshRenderer>().material.color;
		previousColor.r += redShift;
		previousColor.b += blueShift;
		previousColor.g += greenShift;
		ChangeColor(other.gameObject, previousColor);
		//Debug.Log(selection.Count);
	}

	void ChangeColor(GameObject passedObject, Color color){
		Material passedMaterial = passedObject.GetComponent<MeshRenderer>().material;
		passedMaterial.color = color;
	}

	void RandomizeSelectionColors(){
		foreach(GameObject go in selection){
			ChangeColor(go, new Color(Random.Range(0.2f, 0.4f),Random.Range(0.2f, 0.4f),Random.Range(0.2f, 0.4f)));
			Color previousColor = go.GetComponent<MeshRenderer>().material.color;
			previousColor.r += redShift;
			previousColor.b += blueShift;
			previousColor.g += greenShift;
			go.GetComponent<MeshRenderer>().material.color = previousColor;
		}
	}

	void EditSelectedColors(){
		foreach(GameObject go in selection){
			ChangeColor(go, new Color(globalSettingsObject.GetComponent<GlobalSettings>().lineR,globalSettingsObject.GetComponent<GlobalSettings>().lineG,globalSettingsObject.GetComponent<GlobalSettings>().lineB));

		}
	}

	void HandleSelection(){
		if(Input.GetMouseButtonDown(0)){
			if(hoverList.Count==0){//if you clicked on nothing, assume that the user doesn't want to have a selection highlighted anymore
				selection.Clear();
			}
			else if(Input.GetKey(KeyCode.LeftShift)) { //if holding shift, then append the selection list with these new objects
				foreach(GameObject go in hoverList){
					if(!selection.Contains(go)){
						selection.Add(go);
					}
				}
				//selection.Clear();
			}
			else{ //unselect other things and just select the hovered items
				selection.Clear();
				foreach(GameObject go in hoverList){
					if(!selection.Contains(go)){
						selection.Add(go);
					}
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			//RandomizeSelectionColors();
			EditSelectedColors();
		}
	}

	public void UpdateRedVal(float given){
		redVal = given;
	}

	public void UpdateBlueVal(float given){
		blueVal = given;
	}

	public void UpdateGreenVal(float given){
		greenVal = given;
	}
}
