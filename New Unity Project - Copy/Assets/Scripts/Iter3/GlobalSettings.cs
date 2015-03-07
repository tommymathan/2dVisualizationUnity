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

	//Anitmation Speed
	public float animationSpeed = 1;

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
	//Background Color
	public float camR;
	public float camG;
	public float camB;

	// Use this for initialization
	void Start () {

		dataManager = GameObject.FindGameObjectWithTag("DataManagerTag").GetComponent<DataManager>();
		camList = GameObject.FindGameObjectsWithTag("MainCamera");
		camButtonList = new List<GameObject>(GameObject.FindGameObjectsWithTag ("CamButtons"));
		onCameraSelectionScreen = false;
		doubleClickTimer = Time.time;
		doubleClickRate = 0.3f;

		Debug.LogError("Visualization Selection Disabled, Press F1 to enable~chris");
		gLOLWidths = 0.04f;

		SetupCamButtons ();
		camR = 0.83912f;
		camG = 0.83912f;
		camB = 0.83912f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F1)){
			onCameraSelectionScreen = true;
		}

		if(Input.GetKeyDown(KeyCode.F2)){
			DisplayQuadCams();
		}

		CameraBackgroundColor ();
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
	}

	public void globalLineColorG(float val)
	{
		globalLineG = val;
	}

	public void globalLineColorB(float val)
	{
		globalLineB = val;
	}

	public void AnimationSpeed(string val)
	{
		double temp = float.Parse (val);
		animationSpeed = (float)temp;
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
			else if(camList[i].name.Equals("CAM_InlineDimensions")){
				camList[i].GetComponent<Camera>().backgroundColor = c;
			}
		}
	}

	public void displayAllVisualizations()
	{
		DisplayQuadCams();
	}

	public void ActivateCam(Camera cam){
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
		Debug.LogError("Visualization Selection Disabled, Press F2 to return to vizSelect Screen~chris");


	}

	public void DisplayQuadCams(){
		Debug.LogError("Visualization Selection Enabled by user~chris");
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
			else if(camList[i].name.Equals("CAM_InlineDimensions")){
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
}
