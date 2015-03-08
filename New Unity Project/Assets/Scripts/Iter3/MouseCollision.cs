using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MouseCollision : MonoBehaviour {

	public List<GameObject> selection;
	public List<GameObject> hoverList;
	public GlobalSettings gs;
	public Color previousColor;
	public bool colorDirection;
	public float nextActionTime;
	public float period;
	public Dictionary<GameObject, Color> colorRetainer;

	// Use this for initialization
	void Start () {
		previousColor = Color.white;
		selection = new List<GameObject>();
		hoverList = new List<GameObject>();
		gs = GameObject.FindGameObjectWithTag ("GlobalSettingsObject").GetComponent<GlobalSettings> ();
		colorDirection = false;
		nextActionTime = 0f;
		period = 0.05f;
		colorRetainer = new Dictionary<GameObject, Color>();
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

		HoverAnimation();
	}

	void OnTriggerEnter(Collider other) {
		//find which index this vector is and select it from every vis type
		int index = 0;
		for(; index<other.transform.parent.childCount; index++){
			if(other.transform.parent.GetChild(index).name.Equals(other.name)){
				break;
			}
		}
		//add this item to the hoverlist, if it is the first time that this item has been hovered then add it to the dictionary so we can remember it's original color
		foreach(GameObject go in gs.camList){
			if(go.transform.childCount>1){
				hoverList.Add(go.transform.GetChild(index).gameObject);
				if(!colorRetainer.ContainsKey(other.gameObject)){
					colorRetainer.Add(go.transform.GetChild(index).gameObject, go.transform.GetChild(index).GetComponent<Renderer>().material.color);
				}
			}
		}
		hoverList = hoverList.Distinct().ToList();

	}

	void OnTriggerExit(Collider other){
		//find which index this vector is and remove it from every vis type
		int index = 0;
		for(; index<other.transform.parent.childCount; index++){
			if(other.transform.parent.GetChild(index).name.Equals(other.name)){
				break;
			}
		}
		foreach(GameObject go in gs.camList){
			if(go.transform.childCount>1){
				go.transform.GetChild(index).gameObject.GetComponent<Renderer>().material.color = colorRetainer[other.gameObject];
				hoverList.Remove(go.transform.GetChild(index).gameObject);
			}
		}
		//other.gameObject.GetComponent<Renderer>().material.color = colorRetainer[other.gameObject];
	}

	void HoverAnimation(){
		//roll the r value based on time
		if(previousColor.r >=1f){
			colorDirection =false;
		}else if(previousColor.r <=0.4f){
			colorDirection=true;
		}
		if(Time.time>nextActionTime){
			nextActionTime+= period;
			if(colorDirection){
				previousColor.r +=0.02f;
				//previousColor.g +=0.02f;
				//previousColor.b +=0.02f;
			}
			else{
				previousColor.r -=0.04f;
				//previousColor.g -=0.04f;
				//previousColor.b -=0.04f;
			}
		}
		foreach(GameObject go in hoverList){
			go.GetComponent<MeshRenderer>().material.color = previousColor;
		}
	}

	void LineSelectedChangeColor(){
		Color workingColor = new Color (gs.gLineR, gs.gLineG, gs.gLineB);;
		foreach (GameObject go in selection) {
			Material passedMaterial = go.GetComponent<MeshRenderer> ().material;
			passedMaterial.color = workingColor;
			//Debug.Log ("Red: " + gs.gLineR + "\nGreen" + gs.gLineG + "\nBlue" + gs.gLineB);
			colorRetainer[go] = workingColor;
		}
	}

	void ChangeColor(GameObject passedObject, Color color){
		Material passedMaterial = passedObject.GetComponent<MeshRenderer>().material;
		passedMaterial.color = color;
	}

}
