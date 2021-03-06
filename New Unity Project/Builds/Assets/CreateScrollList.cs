﻿
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

public class Item {
	public string name;
	
}
public class CreateScrollList : MonoBehaviour{
	
	
	public GameObject TableTextValue;
	public DataBuilder dataItems;
	public List<Item> itemList;
	public List<GameObject> currentItems;
	public GameObject ContentPanel;
	public int currentCounter = 0;
	public Transform contentPanel;
	
	// Use this for initialization
	void Start () {
		//currentCounter = GameObject.FindGameObjectWithTag ("DataManagerTag").GetComponent<DataManager> ().updateCounter;
		//DataBuilder tableInputs = GameObject.FindGameObjectWithTag("DataManagerTag").GetComponent<DataManager>().dataParser;
		//DataObject currentData = tableInputs.getDataObject ();
		//List<String> dimensions = currentData.labels;
		//int numberOfColumns = dimensions.Count ();
		PopulateList (5);
	}
	
	void Update(){
		int dataCounter = GameObject.FindGameObjectWithTag ("DataManagerTag").GetComponent<DataManager> ().updateCounter;
		//Debug.Log (dataCounter);
		if (currentCounter != dataCounter) {
			//dataItems = 
			//string[] currentValues = GameObject.FindGameObjectWithTag("DataManagerTag").GetComponent<DataManager>().dataParser.fileLines;
			currentCounter = dataCounter;
			Debug.Log("you've found me Tommy");
			DataBuilder tableInputs = GameObject.FindGameObjectWithTag("DataManagerTag").GetComponent<DataManager>().dataParser;
			DataObject currentData = tableInputs.getDataObject ();
			List<String> dimensions = currentData.labels;
			List<List <float>> multiData = currentData.incomingData;
			int numberOfColumns = dimensions.Count ();
			PopulateList (1, dimensions, tableInputs.fileLines);
			
		}
		
	}
	
	void PopulateList(int columns) {
		for (int i = 0; i < currentItems.Count(); i++) {
			DestroyImmediate(currentItems.ElementAt(i));
		}
		currentItems.Clear ();
		//foreach (var item in itemList) {
		//	GameObject newButton = Instantiate (TableTextValue) as GameObject;
		//	currentItems.Add(newButton);
		//	textValueButton button = newButton.GetComponent <textValueButton> ();
		//	button.nameLabel.text = item.name;
		//	newButton.transform.SetParent(contentPanel);
		//	ContentPanel.GetComponent<GridLayoutGroup>().constraintCount = columns;
		//}
		
	}


	void PopulateList(int columns, List<String> columnHeader, string[] currentData) {
		
		for (int i = 0; i < currentItems.Count(); i++) {
			DestroyImmediate(currentItems.ElementAt(i));
		}
		currentItems.Clear ();



		for(int k = 0; k < currentData.Count() ; k++)
		{
			//currentData[tableCursor * length + i] 
			//tableCursor 
			//next -> tableCursor++ once that gets to the end you disable 
			//if 
			GameObject newButton = Instantiate (TableTextValue) as GameObject;
			currentItems.Add(newButton);
			textValueButton button = newButton.GetComponent <textValueButton> ();
			button.row = k;
			button.name = "this is: " + k;
			button.tag = "DataElement";

			button.nameLabel.text = "  Vector :" + k;
			button.setVectorProperty(currentData.ElementAt(k));

			newButton.transform.SetParent(contentPanel);

		}
		ContentPanel.GetComponent<GridLayoutGroup>().constraintCount = columns;
		string test = currentData [2];
		Debug.Log ("test length:" + currentData.Count());
		ContentPanel.GetComponent<GridLayoutGroup> ().cellSize = new Vector2 (260, 30f);
		
	
		
		/*
		foreach(List<float> mainData in currentData)
		{
			foreach(float ma in mainData){
				GameObject newButton = Instantiate (TableTextValue) as GameObject;
				currentItems.Add(newButton);
				textValueButton button = newButton.GetComponent <textValueButton> ();
				button.nameLabel.text = "" + ma;
				newButton.transform.SetParent(contentPanel);
				ContentPanel.GetComponent<GridLayoutGroup>().constraintCount = columns;
			}
		}
		*/
	}

	void PopulateList(int columns, List<String> columnHeader, List<List <float>> currentData) {

		for (int i = 0; i < currentItems.Count(); i++) {
			DestroyImmediate(currentItems.ElementAt(i));
		}
		currentItems.Clear ();
		
		for(int i = columnHeader.Count(); i > 0 ; i--)
		{
			GameObject newButton = Instantiate (TableTextValue) as GameObject;
			currentItems.Add(newButton);
			textValueButton button = newButton.GetComponent <textValueButton> ();
			button.nameLabel.text = columnHeader.ElementAt(i-1);
			newButton.transform.SetParent(contentPanel);
			ContentPanel.GetComponent<GridLayoutGroup>().constraintCount = columns;
		}




		/*
		foreach(List<float> mainData in currentData)
		{
			foreach(float ma in mainData){
				GameObject newButton = Instantiate (TableTextValue) as GameObject;
				currentItems.Add(newButton);
				textValueButton button = newButton.GetComponent <textValueButton> ();
				button.nameLabel.text = "" + ma;
				newButton.transform.SetParent(contentPanel);
				ContentPanel.GetComponent<GridLayoutGroup>().constraintCount = columns;
			}
		}
		*/
	}
	
	
}


 