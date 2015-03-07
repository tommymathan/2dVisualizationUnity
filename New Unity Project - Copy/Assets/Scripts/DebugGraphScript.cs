using UnityEngine;
using System.Collections;

public class DebugGraphScript : MonoBehaviour {
	public float[] dataPoints;
	public GameObject dataPointPrefab;
	private ArrayList visualizedPoints;
	private LineRenderer lineR;

	// Use this for initialization
	void Start () {
		visualizedPoints = new ArrayList();
		lineR = gameObject.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		//if there is a new set of datapoints to deal with
		//update this graph's list of data points
		if(DataParserScript.GetDataUpdated()){
			dataPoints = DataParserScript.GetDataPointsAsFloatArray();
			Debug.Log("data fetched");
			//since we are testing normal 2d graphing, make sure there are an even number of points
			if(dataPoints.Length%2==0){
				foreach(GameObject go in visualizedPoints){
					Destroy(go);
				}
				visualizedPoints.Clear();
				lineR.SetVertexCount(0);

				SetupDebugGraph();
				DrawDebugGraph();
			}
			else{
				Debug.Log("Warning, data was updated but there are no an even number of points in the dataset!");
			}
		}
	}

	public void SetupDebugGraph(){
		Debug.Log("adding point");
		for(int i = 0; i < dataPoints.Length; i+=2){
			visualizedPoints.Add(Instantiate(dataPointPrefab, new Vector2(dataPoints[i],dataPoints[i+1]), Quaternion.identity));
		}

		foreach(GameObject point in visualizedPoints){
			point.name = point.transform.position.x + " " + point.transform.position.y;
		}
	}

	public void DrawDebugGraph(){
		lineR.SetVertexCount(visualizedPoints.Count);

		//first arg for setposition is which vertex, the second arg is it's position
		for(int i = 0; i<visualizedPoints.Count; i++){
			GameObject thisPoint = (GameObject)visualizedPoints[i];
			thisPoint.transform.rotation = Quaternion.identity;
			lineR.SetPosition(i, thisPoint.transform.position);
		}
	}
}
