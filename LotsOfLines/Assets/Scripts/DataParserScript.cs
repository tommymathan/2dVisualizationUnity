using UnityEngine;
using System.Collections;

public class DataParserScript : MonoBehaviour {
	private string stringToParse;
	private char[] delimiters;
	private static float[] parsedFloats;

	//dataUpdated will flip to true when something has changed in the dataparser
	//this way, data is only pulled from the parser when something new happens
	private static bool dataUpdated;

	// Use this for initialization
	void Start () {
		delimiters = new char[] {' ', ','};
		dataUpdated=false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//every frame, set dataUpdated to false
		//it will only be set true on frames where something changes
		if(GetDataUpdated()){
			Debug.Log("Data was updated on frame " +Time.frameCount);
		}

		SetDataUpdated(false);
	}

	public string ParseThis(string given){
		return "Parse Succeeded?: " + ChopUp(given);
	}

	public bool ChopUp(string given){
		bool successVal = true;
		//split the given text using the specified delimiters
		string[] splitData = given.Split(delimiters);

		//allocate enough space to hold the given data in a float array
		parsedFloats = new float[splitData.Length];

		//push the given string tokens to the float array
		int count = 0;
		foreach(string s in splitData){
			if(float.TryParse(s, out parsedFloats[count])){
				count++;
				SetDataUpdated(true);
			}
			else{
				Debug.Log(s+" is not a float!");
				successVal=false;
				break;
			}
		}

		//temp return val
		return successVal;
	}

	public static bool GetDataUpdated(){
		return dataUpdated;
	}
	public static float[] GetDataPointsAsFloatArray(){
		return parsedFloats;
	}

	private void SetDataUpdated(bool given){
		dataUpdated = given;
	}
}
