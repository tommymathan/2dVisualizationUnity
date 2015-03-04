using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalSettings : MonoBehaviour {

	public float gLineR;
	public float gLineB;
	public float gLineG;
	public float gLOLWidths;
	public DataManager dataManager;
	public bool onCameraSelectionScreen; // flag to tell if the user is currently looking at multiple visualization cameras
	public GameObject[] camList;
	// Use this for initialization
	void Start () {
		dataManager = GameObject.FindGameObjectWithTag("DataManagerTag").GetComponent<DataManager>();
		camList = GameObject.FindGameObjectsWithTag("MainCamera");
		onCameraSelectionScreen = false;
		Debug.LogError("Visualization Selection Disabled, Press F1 to enable~chris");
		gLOLWidths = 0.04f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F1)){
			onCameraSelectionScreen = true;
		}

		if(Input.GetKeyDown(KeyCode.F2)){
			DisplayQuadCams();
		}

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
		cam.rect = new Rect(0,0,1,1);
		onCameraSelectionScreen = false;
		Debug.LogError("Visualization Selection Disabled, Press F2 to return to vizSelect Screen~chris");
	}

	public void DisplayQuadCams(){
		Debug.LogError("Visualization Selection Enabled by user~chris");
		onCameraSelectionScreen = true;
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
}
