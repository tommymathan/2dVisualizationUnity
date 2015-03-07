using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

public class Item {
	public string name;

}
public class CreateScrollList : MonoBehaviour {


	public GameObject TableTextValue;
	public List<Item> itemList;

	public Transform contentPanel;

	// Use this for initialization
	void Start () {
		PopulateList ();
	}

	void PopulateList() {
		foreach (var item in itemList) {
			GameObject newButton = Instantiate (TableTextValue) as GameObject;
			textValueButton button = newButton.GetComponent <textValueButton> ();
			button.nameLabel.text = item.name;
			newButton.transform.SetParent(contentPanel);
		}
	}

}
