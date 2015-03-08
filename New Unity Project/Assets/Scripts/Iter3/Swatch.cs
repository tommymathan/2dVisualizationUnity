using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Swatch : MonoBehaviour {
	public GlobalSettings gs;
	public GameObject go;
	public GameObject[] mice;
	// Use this for initialization
	void Start () {
		gs = GameObject.FindGameObjectWithTag("GlobalSettingsObject").GetComponent<GlobalSettings>();
		mice = GameObject.FindGameObjectsWithTag("Mouse");
	}
	
	// Update is called once per frame
	void Update () {
		go.GetComponent<Image>().color = new Color(gs.gLineR, gs.gLineG, gs.gLineB);
	}

	public void UpdateSelectedLineColors(){
		gs.globalLineUpdateFlag = true;
	}
}
