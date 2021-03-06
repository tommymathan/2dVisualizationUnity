﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GlobalSettings : MonoBehaviour {
	//Single line colors
	public float gLineR;
	public float gLineB;
	public float gLineG;

	//Global line widths
	public float gLOLWidths;

	//Global line colors
	public float globalLineR;
	public float globalLineG;
	public float globalLineB;
	public Dictionary<GameObject, Color> colorRetainer;
	public bool globalLineColorReset;
	public bool globalLineUpdateFlag;

	//Global UI Tint
	public float uiR;
	public float uiG;
	public float uiB;
	
	//Global line type 
	public int lineType;

	//Current selected lines indexes
	public HashSet<int> selectedLines;

	//Anitmation Speed
	public int animationSpeed = 1;

	//Normalization
	public int normalizationVal = 5;

	//Scale factor
	public float ScaleFactor = 1;
	public DataManager dataManager;

	//cam variables
	public bool onCameraSelectionScreen; // flag to tell if the user is currently looking at multiple visualization cameras
	public GameObject[] camList;
	public GameObject collocatedButton;
	public GameObject radialPairedButton;
	public GameObject shiftedPairedButton;
	public GameObject inlineDimensionsButton;
	public List<GameObject> camButtonList;
	public float doubleClickTimer;
	public float doubleClickRate;

	//Notification Object
	public GameObject loadingNotification;


	//Background Color + GraphLine Colors
	public float camR;
	public float camG;
	public float camB;
	public Color camLinesRegular;
	public Color camLinesOrigin;
	public Color camLinesDemarked;
	public float camLinesInterval;
	public float camLinesDemarkationInterval;

	//Mouse things
	public Vector3 mousePos;
	public GameObject mouseTextObject;
	public bool mouseOverUI;
	public bool mouseOverDataTable;
	public HashSet<GameObject> selection;
	public List<GameObject> hoverList;

	public Material lolMat;

	//Updating counter
	int counterLoad = 0;

	// Use this for initialization
	void Start () {

		dataManager = GameObject.FindGameObjectWithTag("DataManagerTag").GetComponent<DataManager>();
		camList = GameObject.FindGameObjectsWithTag("MainCamera");
		camButtonList = new List<GameObject>(GameObject.FindGameObjectsWithTag ("CamButtons"));
		onCameraSelectionScreen = false;
		doubleClickTimer = Time.time;
		doubleClickRate = 0.3f;

		//Debug.LogError("Visualization Selection Disabled, Press F1 to enable~chris");
		gLOLWidths = 0.04f;
		globalLineUpdateFlag = false;

		SetupCamButtons ();
		camR = 0.83912f;
		camG = 0.83912f;
		camB = 0.83912f;
		camLinesRegular = Color.grey;
		camLinesOrigin = Color.blue;
		camLinesDemarked = Color.magenta;
		camLinesInterval = 100000f;
		camLinesDemarkationInterval = 0f;
		colorRetainer = new Dictionary<GameObject, Color>();

		uiR = 1f;
		uiG = 1f;
		uiB = 1f;

		globalLineR = 1.0f;
		globalLineG = 1.0f;
		globalLineB = 1.0f;

		globalLineColorReset = false;
		mouseOverUI=false;
		mouseOverDataTable = false;
		selection = new HashSet<GameObject>();
		hoverList = new List<GameObject>();

		loadingNotification.GetComponent<Image> ().enabled = false;
		loadingNotification.transform.GetChild (0).GetComponent<Text> ().enabled = false;
		loadingNotification.transform.GetChild (1).GetComponent<Image> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F1)){
			onCameraSelectionScreen = true;
		}

		if(Input.GetKeyDown(KeyCode.F2)){
			DisplayQuadCams();
		}

		/*
		if (dataManager.updating == true) {
				loadingNotification.GetComponent<Image> ().enabled = true;
				loadingNotification.transform.GetChild (0).GetComponent<Text> ().enabled = true;
				loadingNotification.transform.GetChild (1).GetComponent<Image> ().enabled = true;
		} else {
			loadingNotification.GetComponent<Image> ().enabled = false;
			loadingNotification.transform.GetChild (0).GetComponent<Text> ().enabled = false;
			loadingNotification.transform.GetChild (1).GetComponent<Image> ().enabled = false;

		}

	*/

		CameraBackgroundColor ();
		UpdateMouseText();

	}
	

	public void setLoadingNotification()
	{
		if (loadingNotification != null) {
						loadingNotification.GetComponent<Image> ().enabled = true;
						loadingNotification.transform.GetChild (0).GetComponent<Text> ().enabled = true;
						loadingNotification.transform.GetChild (1).GetComponent<Image> ().enabled = true;
		}
	}

	public void removeLoadingNotification()
	{
		loadingNotification.GetComponent<Image> ().enabled = false;
		loadingNotification.transform.GetChild (0).GetComponent<Text> ().enabled = false;
		loadingNotification.transform.GetChild (1).GetComponent<Image> ().enabled = false;
	}


	public void UpdateMouseText(){
		//get this into 2 decimal places format
		float x = mousePos.x;
		float y = mousePos.y;
		x=x*100;
		y=y*100;
		x= Mathf.Round(x);
		y= Mathf.Round(y);
		x=x/100;
		y=y/100;
		mouseTextObject.transform.position = new Vector2(Input.mousePosition.x+50, Input.mousePosition.y-20);
		mouseTextObject.GetComponent<Text>().text = "("+x+","+y+")";
	}

	public void setgLoLWidths(string val){
		double temp = float.Parse (val);
		Debug.Log (val);
		gLOLWidths = (float)temp;
		
		dataManager.NotifyVizualizations(); //this is probably slow because it is i/o calling -> can we update vis without io?
	}
	
	//Change line color
	public void lineColorR(float val)
	{
		gLineR = val;
	}
	public void lineColorG(float val)
	{
		gLineG = val;
	}
	
	public void lineColorB(float val)
	{
		gLineB = val;
	}
	
	//Preset Colors
	public void lineRedColor()
	{
		gLineR = 1;
		gLineG = 0;
		gLineB = 0;
	}
	
	public void lineGreenColor()
	{
		gLineR = 0;
		gLineG = 1;
		gLineB = 0;
	}
	
	public void lineBlueColor()
	{
		gLineR = 0;
		gLineG = 0;
		gLineB = 1;
	}
	
	public void setLineType(int val)
	{
		GameObject dataManager = GameObject.FindGameObjectWithTag ("DataManagerTag");
		lineType = val;
		dataManager.GetComponent<DataManager> ().NotifyVizualizations ();
		
	}
	public void linePinkColor()
	{
		gLineR = 1;
		gLineG = 0;
		gLineB = 1;
	}
	public void lineYellowColor()
	{
		gLineR = 1;
		gLineG = 0.92f;
		gLineB = 0.016f;
	}
	public void lineOrangeColor()
	{
		gLineR = 1;
		gLineG = 0.647f;
		gLineB = 0;
	}
	
	public void camBackR(float val)
	{
		camR = val;
	}
	
	public void camBackG(float val)
	{
		camG = val;
	}
	
	public void camBackB(float val)
	{
		camB = val;
	}

	//Methods for global line color
	public void globalLineColorR(float val)
	{
		globalLineR = val;
		setGlobalLineColor ();
	}

	public void globalLineColorG(float val)
	{
		globalLineG = val;
		setGlobalLineColor ();
	}

	public void globalLineColorB(float val)
	{
		globalLineB = val;
		setGlobalLineColor ();
	}

	public void setGlobalLineColor()
	{	List<GameObject> ls = new List<GameObject>(GameObject.FindGameObjectsWithTag ("vector"));

		globalLineColorReset = true;
		for( int i = 0; i < ls.Count; i++)
		{
			ls[i].GetComponent<MeshRenderer>().material.color = new Color(globalLineR, globalLineG, globalLineB);

		}

		foreach (GameObject go in GameObject.FindGameObjectsWithTag("vector")) {
			if(colorRetainer.ContainsKey(go)){
				//change color
				colorRetainer[go] = new Color(globalLineR, globalLineG, globalLineB);;
			}
		}

	}

	public void AnimationSpeed(string val)
	{
		double temp = float.Parse (val);
		animationSpeed = (int)temp * 10;
		
		for( int i = 0; i < camList.Length; i++)
		{
			camList[i].GetComponent<Visualization>().updateAnimationSpeed(animationSpeed);
		}
	}

	public void normalizationFactor(string val)
	{
		normalizationVal = int.Parse (val);
	}

	public void scaleFactor(string val)
	{
		double temp = float.Parse (val);
		ScaleFactor = (float)temp;
	}

	public void updateSelection(HashSet<int> vals)
	{
		selectedLines = vals;
	}
	//Change camera back ground colors
	public void CameraBackgroundColor()
	{
		Color c = new Color (camR, camG, camB);
		
		for (int i = 0; i < camList.Length; i++) {
			if(camList[i].name.Equals("CAM_Collocated")){
				camList[i].GetComponent<Camera>().backgroundColor = c;
			}
			else if(camList[i].name.Equals("CAM_RadialPaired")){
				camList[i].GetComponent<Camera>().backgroundColor = c;
			}
			else if(camList[i].name.Equals("CAM_ShiftedPaired")){
				camList[i].GetComponent<Camera>().backgroundColor = c;
			}
			else if(camList[i].name.Equals("CAM_Parallel")){
				camList[i].GetComponent<Camera>().backgroundColor = c;
			}
		}
	}

	public void displayAllVisualizations()
	{
		DisplayQuadCams();
	}

	public void ActivateCam(Camera cam){
		//if the user clicks in here after dragging from another window you have to re-enable this cam immediately or the currently selected cam will be the old one
		foreach(GameObject otherCam in camList){
			otherCam.GetComponent<MouseHandler>().enabled = false;
		}

		cam.gameObject.GetComponent<MouseHandler>().enabled = true;

		if((Time.time-doubleClickTimer)<=doubleClickRate){
			for(int i = 0; i< camList.Length; i++){
				DeactivateCam(camList[i].GetComponent<Camera>());
			}
			cam.enabled = true;
			SetCamAsFullFocus(cam);
		}
		doubleClickTimer = Time.time;

	}

	public void DeactivateCam(Camera cam){
		cam.enabled = false;
	}

	public void SetCamAsFullFocus(Camera cam){
		onCameraSelectionScreen = false;
		for(int i = 0; i<camButtonList.Count; i++){
			Button button = camButtonList[i].GetComponent<Button>();
			button.interactable = false;
			camButtonList[i].SetActive(false);
		}

		cam.rect = new Rect(0,0,1,1);
		onCameraSelectionScreen = false;
		//Debug.LogError("Visualization Selection Disabled, Press F2 to return to vizSelect Screen~chris");


	}

	public void DisplayQuadCams(){
		//Debug.LogError("Visualization Selection Enabled by user~chris");
		onCameraSelectionScreen = true;

		for(int i = 0; i<camButtonList.Count; i++){
			camButtonList[i].SetActive(true);
			Button button = camButtonList[i].GetComponent<Button>();
			button.interactable = true;
		}

		for(int i = 0; i< camList.Length; i++){
			camList[i].GetComponent<Camera>().enabled = true;
			if(camList[i].name.Equals("CAM_Collocated")){
				camList[i].GetComponent<Camera>().rect = new Rect(0f,0.5f,0.5f,0.5f);
			}
			else if(camList[i].name.Equals("CAM_RadialPaired")){
				camList[i].GetComponent<Camera>().rect = new Rect(0.5f,0.5f,0.5f,0.5f);
			}
			else if(camList[i].name.Equals("CAM_ShiftedPaired")){
				camList[i].GetComponent<Camera>().rect = new Rect(0f,0f,0.5f,0.5f);
			}
			else if(camList[i].name.Equals("CAM_Parallel")){
				camList[i].GetComponent<Camera>().rect = new Rect(0.5f,0f,1f,0.5f);
			}
		}
	}

	public void SetupCamButtons(){
		collocatedButton.GetComponent<RectTransform> ().sizeDelta = new Vector2 (Screen.width / 2, Screen.height / 2);
		collocatedButton.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-Screen.width / 4, Screen.height / 4);

		radialPairedButton.GetComponent<RectTransform> ().sizeDelta = new Vector2 (Screen.width / 2, Screen.height / 2);
		radialPairedButton.GetComponent<RectTransform> ().anchoredPosition = new Vector2 ((Screen.width / 4), Screen.height / 4);

		shiftedPairedButton.GetComponent<RectTransform> ().sizeDelta = new Vector2 (Screen.width / 2, Screen.height / 2);
		shiftedPairedButton.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-(Screen.width / 4), -(Screen.height / 4));

		inlineDimensionsButton.GetComponent<RectTransform> ().sizeDelta = new Vector2 (Screen.width / 2, Screen.height / 2);
		inlineDimensionsButton.GetComponent<RectTransform> ().anchoredPosition = new Vector2 ((Screen.width / 4), -Screen.height / 4);
	}

	public void EnableCamManip(GameObject go){
		if(!Input.GetMouseButton(1)){//if the user is holding down the mouse button, then don't activate the hovered cam because they are probably dragging a vis cam out of its current ounds and don't want to be interrupted in that process
			foreach(GameObject otherCam in camList){
				otherCam.GetComponent<MouseHandler>().enabled = false;
				otherCam.transform.GetChild (0).GetComponent<BoxCollider>().enabled = false;
			}
			go.GetComponent<MouseHandler>().enabled = true;
			go.transform.GetChild (0).GetComponent<BoxCollider>().enabled = true;
		}
	}

	public void RevertColors(){
		foreach(GameObject go in hoverList){
			if(colorRetainer.ContainsKey(go)){
				go.GetComponent<MeshRenderer>().material.color = colorRetainer[go];
			}
		}
	}

	public void HideOthers(){
		if (selection.Count > 0) {
						foreach (GameObject go in GameObject.FindGameObjectsWithTag("vector")) {
								if (!selection.Contains (go)) {
										go.GetComponent<MeshRenderer> ().enabled = false;
								}
						}
				}
	}

	public void HideSelected(){
		if (selection.Count > 0) {
						foreach (GameObject go in selection) {
								go.GetComponent<MeshRenderer> ().enabled = false;
						}
				}	
	}

	public void ShowAll(){

		foreach(GameObject go in GameObject.FindGameObjectsWithTag("vector")){
				go.GetComponent<MeshRenderer>().enabled = true;
		}
	}
}
