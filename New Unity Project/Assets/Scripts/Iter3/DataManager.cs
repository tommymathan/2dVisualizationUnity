using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DataManager : MonoBehaviour {
	private static List<float> rawData; // holds the raw data from the parser
	private static List<Visualization> vizList; //will hold the list of Visualizations to be called when data is updated
	private bool dataUpdated;
	private string dataPath;
	private DataObject dataSet;
	private DataBuilder dataParser;

	// Use this for initialization
	void Start () {
		dataUpdated = false;
		dataParser = new DataBuilder ();
		dataSet = new DataObject ();
		dataSet = dataParser.getDataObject();

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
		//TODO: Check for new excel file every five secs?
		checkNewFile ();

		if (dataUpdated) {
			NotifyVizualizations();
		}
	}

	void checkNewFile()
	{
		}
	void parseDataFile(){
		dataParser = new DataBuilder (dataPath);
		dataSet = new DataObject ();
		dataSet = dataParser.getDataObject();
		}

	void NotifyVizualizations(){
		dataUpdated = false;
		for (int i = 0; i<vizList.Count(); i++) {
			vizList[i].UpdateData(dataSet);
		}
		

	}

	public void SetDataPath(string givenPath){
		dataPath = givenPath;
		parseDataFile();
		Debug.Log ("data path is: "+ givenPath);
		NotifyVizualizations ();
	}
}
