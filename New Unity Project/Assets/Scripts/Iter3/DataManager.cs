using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DataManager : MonoBehaviour {
	private static List<float> rawData; // holds the raw data from the parser
	private static List<Visualization> vizList; //will hold the list of Visualizations to be called when data is updated
	private bool dataUpdated;
	private string dataPath;

	// Use this for initialization
	void Start () {
		dataUpdated = true;

		//register all visualizations with this data manager
		List <GameObject> cameras = new List<GameObject>();
		vizList = new List<Visualization> ();
		cameras = GameObject.FindGameObjectsWithTag ("MainCamera").ToList();
		for (int i = 0; i< cameras.Count(); i++) {
			vizList.Add(cameras[i].gameObject.GetComponent<Visualization>());
		}
		Debug.Log (vizList.Count () + " Visualizations are registered with the DataManager");
		dataPath = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (dataUpdated) {
			NotifyVizualizations();
		}
	}

	void NotifyVizualizations(){

		for (int i = 0; i<vizList.Count(); i++) {
			vizList[i].UpdateData();
		}
		dataUpdated = false;
	}

	public void SetDataPath(string givenPath){
		dataPath = givenPath;
	}
}
