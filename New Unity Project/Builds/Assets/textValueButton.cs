using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class textValueButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	
	public Button button;
	public Text nameLabel;
	public int row;
	public bool isMouseOver = false;
	public GlobalSettings gs;
	public ColorBlock cb;
	public string vector; 

	void Start()
	{
		cb = button.colors;
		gs = GameObject.FindGameObjectWithTag ("GlobalSettingsObject").GetComponent<GlobalSettings> ();
		Debug.Log ("what is the vec" + vector);
		//vector = "OKAY"; 
	}

	public void OnPointerEnter(PointerEventData data)
	{
		//vector += Time.time;
		Debug.Log ("The vector is " + vector);

		if(vector != null)
		{
			GameObject.FindGameObjectWithTag("TableButtonVectorHeader").GetComponentInChildren<Text>().text =  vector;
		}


		gs.mouseOverDataTable= true;
		foreach (GameObject go in gs.camList) {

			if(go.transform.childCount > 1 && row > 0)
			{
				//gs.hoverList.Clear ();
				if(!gs.hoverList.Contains(go.transform.GetChild(row).gameObject))
				{
					gs.hoverList.Add(go.transform.GetChild(row).gameObject);
					if(!gs.colorRetainer.ContainsKey(go.transform.GetChild(row).gameObject)){
						gs.colorRetainer.Add(go.transform.GetChild(row).gameObject, go.transform.GetChild(row).gameObject.GetComponent<MeshRenderer>().material.color);
	
						//string[] currentVecotr = tableInputs.fileLines;

						//GameObject.FindGameObjectWithTag("TableButtonVectorHeader").GetComponent<Button>().nameLabel = 
					}
				}
			}
		}


	}

	public void setVectorProperty(string val)
	{	

		Debug.Log ("The incoming value is " + val);
		vector = val;
		Debug.Log ("The vector is " + vector);

		}
	
	/* 
	 * Mouse exited this object and is not hovering it
	 */
	public void OnPointerExit(PointerEventData data)
	{
		gs.mouseOverDataTable = false;
		gs.RevertColors();
		gs.hoverList.Clear ();
		gs.mouseOverUI = false;
	}
	public void Update()
	{

		if (gs.globalLineUpdateFlag ) {
			GameObject go = gs.camList[1];
			if(go.transform.childCount > 1){
			if(gs.selection.Contains(go.transform.GetChild(row).gameObject))
			cb.normalColor = new Color (gs.gLineR,gs.gLineG,gs.gLineB);
						button.colors = cb;
			}

		}
		//TODO: Make this reset the color of the elements of the table
		if (gs.globalLineColorReset){
		//	cb.normalColor = new Color (Color.white);
		//	button.colors = cb;
		//	gs.globalLineColorReset = false;
		}
	}
	public void OnPointerClick(PointerEventData data)
	{

	}


}



