using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public struct DataObject
{
	public List<String> labels;
	public List<List<float>> incomingData;
}

public class DataBuilder
{	
	string path = Environment.CurrentDirectory + @"\Assets\DataSets\";
	char[] delimiters = new char[] { ' ', ',' };
	DataObject dataObject;
	bool columnWise = false;
	string[] fileLines;
	
	
	public DataBuilder (String incPath)
	{
		//Later will set the file path when the constructor is called
		//path += "forestFires.csv"; //Put your file into the datasets folder to test
		if (incPath == null || incPath.Equals("")) {
						path += "forestFiresFullDataSet.csv"; //Put your file into the datasets folder to test
				} else {

						path = incPath;
						parseDataIntoDataObject ();
				}
		
		
	}//End of databuilder constructor
	public DataBuilder ()
	{
		//Later will set the file path when the constructor is called
		path += "forestFires.csv"; //Put your file into the datasets folder to test
		
		
		parseDataIntoDataObject ();
		
		
	}//End of databuilder constructor
	
	private void parseDataIntoDataObject(){
		//Bring the file in via a file reader, put lines into an array
		fileLines = System.IO.File.ReadAllLines (path);
		//Construct the dataObject
		makeDataObject ();
		setUpDataObject ();				
		
		//temp variables needed for the parsing loop
		float tempFloat = 0.0f;
		int count = 0;
		string[] delimitedLine;
		
		//Actual parsing occurs here
		foreach (string dataLine in fileLines) {
			delimitedLine = dataLine.Split (delimiters);
			foreach (string dataElement in delimitedLine) {
				
				//Debug.Log("Attempting to add" + dataElement);
				if (float.TryParse (dataElement, out tempFloat)) {
					//  Debug.Log("Temp float is" + tempFloat);
					//  Debug.Log("count is" + count);
					tempFloat = normalizationFunction(tempFloat);
					dataObject.incomingData [count].Add (tempFloat);	
					if (columnWise)
						count++;
				}					
			}
			if (dataObject.incomingData [count].Count > 0){
			count = columnWise ? 0 : count+1;
			}
			
			
		}//End of for each
	}

	private float normalizationFunction(float temp)
	{
		return (temp / 724) * 40;
	}
	//Returns an organized representation of a csv file
	public DataObject getDataObject ()
	{
		return dataObject;
	}
	
	//Set up the dataObject, be careful about your dataset if it has more headings than vectors you will have 
	//an error. At the moment the function builds the dataObject as if there will be less headings and more 
	//vectors. If that is not the case you can temporarily reverse the equality signs around in the add range 
	//and the columnWise setter.
	
	private void setUpDataObject(){
		string[] delimitedLine;	
		List<String> temp1 = new List<String> ();
		List<String> temp2 = new List<String> ();
		
		//Check for row labels
		delimitedLine = fileLines [0].Split (delimiters);
		foreach (string rowHeading in delimitedLine) {
			temp1.Add (rowHeading);
		}
		
		//Check for column labels
		foreach (string columnHeading in fileLines) {
			delimitedLine = columnHeading.Split (delimiters);
			temp2.Add (delimitedLine [0]);
		}
		
		//Add the labels from the list which had the fewest items added to it,
		//rarely would we see a dataset that had more labels than vectors
		// Debug.Log("temp 1 count is" + temp1.Count + "Temp 2 count is" + temp2.Count);


		dataObject.labels.AddRange (temp1.Count < temp2.Count ? temp1 : temp2);
		
		//if there are more column labels than row labels we are row wise
		columnWise = (temp1.Count > temp2.Count);
		
		//Make lists to contain each of the incoming vectors
		for (int i = 0; i < (temp1.Count > temp2.Count ? temp1.Count : temp2.Count); i++) {
			dataObject.incomingData.Add (new List<float> ());
		}
		
	}
	private void makeDataObject(){
		dataObject = new DataObject ();
		dataObject.incomingData = new List<List<float>> ();
		dataObject.labels = new List<string> ();
	}
}//end class
