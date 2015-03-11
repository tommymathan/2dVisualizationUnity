using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DataManager : MonoBehaviour {
	
	public static List<Visualization> vizList; //will hold the list of Visualizations to be called when data is updated
	
	private static List<float> rawData; // holds the raw data from the parser
	private bool dataUpdated;
	private string dataPath;
	private DataObject dataSet;
	public DataBuilder dataParser;
	public int updateCounter;
	public bool updating = false;
	
	// Use this for initialization
	void Start () {
		++updateCounter;
		updating = true;
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

		updating = false;
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
		++updateCounter;
		updating = true;

		GlobalSettings gs = GameObject.FindGameObjectWithTag ("GlobalSettingsObject").GetComponent<GlobalSettings> ();
		gs.setLoadingNotification ();

		dataParser = new DataBuilder (dataPath);
		dataSet = new DataObject ();
		dataSet = dataParser.getDataObject();
		//TODO: Method to request vectors to graph from user using GUI 
		//Method returns array of ints describing selected vectors
		int[] temp = new int[dataSet.labels.Count];
		//		for (int k =0; k < temp.Length; k++){
		//			if (k%3==0)
		//			temp [k] = 1;
		//
		//		}
		
		
		for (int i =0; i < dataSet.incomingData.Count-1; i++) {
			for(int j= (dataSet.incomingData[i].Count-1); j >=0 ; j--){				
				if(temp[j]== 1) 
				{
					dataSet.normalizedData[i].RemoveAt(j);
				}				
			}
		}

		updating = false;
		gs.removeLoadingNotification ();
		
		//Assert that dataSet has more than 4 vectors left after removal
	}
	public void addAnimationToViz()
	{
		GlobalSettings gs = GameObject.FindGameObjectWithTag ("GlobalSettingsObject").GetComponent<GlobalSettings> ();
		for (int i = 0; i<vizList.Count(); i++) {
			vizList[i].addLineToAnimate(gs.selectedLines);
		}
	}
	public void NotifyVizualizations(){
		dataUpdated = false;
		updating = true;

		GlobalSettings gs = GameObject.FindGameObjectWithTag ("GlobalSettingsObject").GetComponent<GlobalSettings> ();
		gs.setLoadingNotification ();

		for (int i = 0; i<vizList.Count(); i++) {
			vizList[i].UpdateData(dataSet);
		}
		++updateCounter;
		updating = false;

		gs.removeLoadingNotification ();
	}
	
	public void SetDataPath(string givenPath){
		dataPath = givenPath;
		parseDataFile();
		Debug.Log ("data path is: "+ givenPath);
		NotifyVizualizations ();
	}
}
