using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MouseCollision : MonoBehaviour {

	//public HashSet<GameObject> selection;
	//public List<GameObject> hoverList;
	public GlobalSettings gs;
	public Color previousColor; //used for hover anim
	public Color baseColor;//used for selection anim
	public bool hoverColorDirection;
	public bool selectionColorDirection;
	public float nextActionTimeForHover;
	public float nextActionTimeForSelection;
	public float period;
	//public Dictionary<GameObject, Color> colorRetainer;
	public Vector2 lastMousePos; //used to detect if the user was dragging or not
	public Vector3 mouseDownPos;
	public Vector3 mouseColliderC;//used to revert back to old mouse collider settings
	public Vector3 mouseColliderS;//used to revert back to old mouse collider settings

	// Use this for initialization
	void Start () {
		previousColor = Color.red;
		baseColor = Color.gray;
		gs = GameObject.FindGameObjectWithTag ("GlobalSettingsObject").GetComponent<GlobalSettings> ();


	//	Debug.Log ("GSO is : " +gs.transform.parent.name);
		hoverColorDirection = false;
		selectionColorDirection = false;
		nextActionTimeForHover = 0f;
		nextActionTimeForSelection = 0f;
		period = 0.05f;
		//colorRetainer = new Dictionary<GameObject, Color>();
		mouseColliderC = gameObject.GetComponent<BoxCollider>().center;
		mouseColliderS = gameObject.GetComponent<BoxCollider>().size;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0)){ // if you clicked nothing and were't dicking with the UI, then assume the user wanted to deselect
			RevertMouseCollider();
			if((!gs.mouseOverUI)&&(gs.hoverList.Count==0)){
				RevertColors();
				gs.selection.Clear();
				updateSelectionInGlobalSettings();
			}
		}

		if(!gs.mouseOverUI){//if the mouse isn't over any UI element...
			if(Input.GetMouseButtonDown(0)){
				mouseDownPos = gameObject.transform.parent.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
				gs.globalLineUpdateFlag = false;
				if((lastMousePos.x==Input.mousePosition.x)&&(lastMousePos.y==Input.mousePosition.y)&&(gs.hoverList.Count==0)){//if you clicked on nothing, assume that the user doesn't want to have a selection highlighted anymore, only do this if they weren't drag-clicking
					RevertColors();
					gs.selection.Clear();
				}
				else{
					if(Input.GetKey(KeyCode.LeftShift)){ //if they are holding shift, keep adding selected lines to the selectionlist
						foreach(GameObject go in gs.hoverList){							
							gs.selection.Add(go);
						}
						updateSelectionInGlobalSettings();
					}
					else if(gs.hoverList.Count>0){//if they weren't holding shift then clear the selection and treat this last click as them choosing everything that they want to work with
						RevertColors();
						gs.selection.Clear();
						foreach(GameObject go in gs.hoverList){
							gs.selection.Add(go);
						}
						updateSelectionInGlobalSettings();
					}
				}
				lastMousePos = Input.mousePosition;

			}

			if(Input.GetMouseButton(0)){
				if(!gs.mouseOverUI){//if the mouse isn't over any UI element...
					DragFunc();
				}
			}

			if(gs.globalLineUpdateFlag){
				LineSelectedChangeColor();
			}
			HoverAnimation();
			SelectionAnimation();
		}
	}

	void updateSelectionInGlobalSettings()
	{
		HashSet<int> selectedVectors = new HashSet<int> ();
		int temp = -1;
		if (gs.selection.Count > 0) {
			foreach (GameObject go in gs.selection) {
				if(int.TryParse (go.name, out temp))selectedVectors.Add (temp);
				//Debug.Log ("We have added" + temp + " to the selected vector list");
			}
			gs.updateSelection (selectedVectors);
		}
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
				gs.hoverList.Add(go.transform.GetChild(index).gameObject);
				if(!gs.colorRetainer.ContainsKey(other.gameObject)){
					gs.colorRetainer.Add(go.transform.GetChild(index).gameObject, go.transform.GetChild(index).GetComponent<Renderer>().material.color);
				}
			}
		}
		gs.hoverList = gs.hoverList.Distinct().ToList();

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
				go.transform.GetChild(index).gameObject.GetComponent<Renderer>().material.color = gs.colorRetainer[other.gameObject];
				gs.hoverList.Remove(go.transform.GetChild(index).gameObject);
			}
		}
		//other.gameObject.GetComponent<Renderer>().material.color = colorRetainer[other.gameObject];

		foreach(GameObject go in gs.camList){
			if(gs.selection.Contains(go)){
				gs.selection.Remove(go);
			}
		}

		gs.hoverList.Clear ();
			updateSelectionInGlobalSettings();
	}
	void SelectionAnimation(){

		if(baseColor.g >=0.99f){
			selectionColorDirection =false;
		}else if(baseColor.g <0.4f){
			selectionColorDirection=true;
		}
		//Debug.Log (baseColor.g + " " + selectionColorDirection);

		if(Time.time>nextActionTimeForSelection){
			nextActionTimeForSelection+= period;
			if(selectionColorDirection){
				baseColor.r +=0.02f;
				baseColor.g +=0.02f;
				baseColor.b +=0.02f;
			}
			else{
				baseColor.r -=0.04f;
				baseColor.g -=0.04f;
				baseColor.b -=0.04f;
			}
		}

						foreach (GameObject go in gs.selection) {
								if (!gs.colorRetainer.ContainsKey (go)) {
										gs.colorRetainer.Add (go, go.GetComponent<MeshRenderer> ().material.color);
								}
								go.GetComponent<MeshRenderer> ().material.color = baseColor;
						}

	}

	void RevertColors(){
		foreach(GameObject go in gs.selection){
			go.GetComponent<MeshRenderer>().material.color = gs.colorRetainer[go];
		}
	}

	void HoverAnimation(){
		//roll the r value based on time
		if(previousColor.r >=1f){
			hoverColorDirection =false;
		}else if(previousColor.r <=0.4f){
			hoverColorDirection=true;
		}
		if(Time.time>nextActionTimeForHover){
			nextActionTimeForHover+= period;
			if(hoverColorDirection){
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
		foreach(GameObject go in gs.hoverList){
			if(!gs.selection.Contains(go)){
				go.GetComponent<MeshRenderer>().material.color = previousColor;
			}
		}
	}

	public void LineSelectedChangeColor(){
		Color workingColor = new Color (gs.gLineR, gs.gLineG, gs.gLineB);;
		foreach (GameObject go in gs.selection) {
			Material passedMaterial = go.GetComponent<MeshRenderer> ().material;
			passedMaterial.color = workingColor;
			//Debug.Log ("Red: " + gs.gLineR + "\nGreen" + gs.gLineG + "\nBlue" + gs.gLineB);
			gs.colorRetainer[go] = workingColor;
		}
		//gs.globalLineUpdateFlag = false;
		gs.selection.Clear ();
		updateSelectionInGlobalSettings();

	}

	void DragFunc(){ //drag selection func
		lastMousePos = gameObject.transform.parent.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
		//Debug.Log("Mouse went down at:" + mouseDownPos + "| Mouse is now at: " + lastMousePos);
		if (Mathf.Abs (lastMousePos.x - mouseDownPos.x) < 0.1) {
			gameObject.GetComponent<BoxCollider>().size = new Vector3(0.01f, 0.01f, 50f);
		} else {
			gameObject.GetComponent<BoxCollider>().size = new Vector3(Mathf.Abs(lastMousePos.x-mouseDownPos.x), Mathf.Abs(lastMousePos.y-mouseDownPos.y), 50f);
		}
		
		gameObject.GetComponent<BoxCollider>().center = new Vector3((mouseDownPos.x-lastMousePos.x)/2, (mouseDownPos.y-lastMousePos.y)/2, 0f);
		if(!gs.mouseOverUI){
			if(gs.hoverList.Count>0){
				RevertColors();
				gs.selection.Clear();
				foreach(GameObject go in gs.hoverList){
					if(!gs.selection.Contains(go)){
						gs.selection.Add(go);
					}
				}
				updateSelectionInGlobalSettings();
			}
		}
	}

	void RevertMouseCollider(){
		gameObject.GetComponent<BoxCollider>().center = mouseColliderC;
		gameObject.GetComponent<BoxCollider>().size = mouseColliderS;
	}

	void ChangeColor(GameObject passedObject, Color color){
		Material passedMaterial = passedObject.GetComponent<MeshRenderer>().material;
		passedMaterial.color = color;
	}

}
