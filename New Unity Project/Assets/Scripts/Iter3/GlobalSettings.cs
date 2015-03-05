using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GlobalSettings : MonoBehaviour {

	public float gLineR;
	public float gLineB;
	public float gLineG;
	public float gLOLWidths;
	public DataManager dataManager;
	public bool onCameraSelectionScreen; // flag to tell if the user is currently looking at multiple visualization cameras
	public GameObject[] camList;
	public GameObject collocatedButton;
	public GameObject radialPairedButton;
	public GameObject shiftedPairedButton;
	public GameObject inlineDimensionsButton;
	public List<GameObject> camButtonList;
	// Use this for initialization
	void Start () {
		dataManager = GameObject.FindGameObjectWithTag("DataManagerTag").GetComponent<DataManager>();
		camList = GameObject.FindGameObjectsWithTag("MainCamera");
		camButtonList = new List<GameObject>(GameObject.FindGameObjectsWithTag ("CamButtons"));
		onCameraSelectionScreen = false;
		Debug.LogError("Visualization Selection Disabled, Press F1 to enable~chris");
		gLOLWidths = 0.04f;

		SetupCamButtons ();
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
		if(Input.GetMouseButtonUp(0)){
			if(onCameraSelectionScreen){//if the user is trying to select a visualization method
				if(Input.mousePosition.x <= Screen.width/2){
					//Debug.Log("On the Left");
					if(Input.mousePosition.y <= Screen.height/2){
						//Debug.Log("On the Bottom Left");
						for(int i = 0; i< camList.Length; i++){
							if(camList[i].name.Equals("CAM_ShiftedPaired")){
								ActivateCam(camList[i].GetComponent<Camera>());
							}
						}
					}
					else{
						//Debug.Log("On the Top Left");
						for(int i = 0; i< camList.Length; i++){
							if(camList[i].name.Equals("CAM_Collocated")){
								ActivateCam(camList[i].GetComponent<Camera>());
							}
						}
					}
				}
				else{
					//Debug.Log("On the Right");
					if(Input.mousePosition.y <= Screen.height/2){
						//Debug.Log("On the Bottom Right");
						for(int i = 0; i< camList.Length; i++){
							if(camList[i].name.Equals("CAM_InlineDimensions")){
								ActivateCam(camList[i].GetComponent<Camera>());
							}
						}
					}
					else{
						//Debug.Log("On the Top Right");
						for(int i = 0; i< camList.Length; i++){
							if(camList[i].name.Equals("CAM_RadialPaired")){
								ActivateCam(camList[i].GetComponent<Camera>());
							}
						}
					}
				}
			}
		}
		*/
	}

	public void setgLoLWidths(float val){
		gLOLWidths = val;
		dataManager.NotifyVizualizations(); //this is probably slow because it is i/o calling -> can we update vis without io?
	}

	public void ActivateCam(Camera cam){
		for(int i = 0; i< camList.Length; i++){
			DeactivateCam(camList[i].GetComponent<Camera>());
		}
		cam.enabled = true;
		SetCamAsFullFocus(cam);
	}

	public void DeactivateCam(Camera cam){
		cam.enabled = false;
	}

	public void SetCamAsFullFocus(Camera cam){

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
