using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CollocatedVis : Visualization {

	// Use this for initialization
	void Start () {
		List<float> test = new List<float> (new float[] {0,0,1,1,3,2,5,2,6,0,7,0});
		utilityObject.GetComponent<Utility>().DrawContiguousLineSegments(.02f, test);
		//gameObject.GetComponent<Camera> ().orthographicSize = 5;
		//gameObject.transform.position = new Vector3 (2f, 2f, gameObject.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
