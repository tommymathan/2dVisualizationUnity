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

	void Start()
	{
		gs = GameObject.FindGameObjectWithTag ("GlobalSettingsObject").GetComponent<GlobalSettings> ();
	}

	public void OnPointerEnter(PointerEventData data)
	{
		gs.mouseOverUI = true;
		foreach (GameObject go in gs.camList) {

			if(go.transform.childCount > 1)
			{
				//gs.hoverList.Clear ();
				if(!gs.hoverList.Contains(go.transform.GetChild(row+1).gameObject))
				{
					gs.hoverList.Add(go.transform.GetChild(row+1).gameObject);
					if(!gs.colorRetainer.ContainsKey(go.transform.GetChild(row+1).gameObject)){
						gs.colorRetainer.Add(go.transform.GetChild(row+1).gameObject, go.transform.GetChild(row+1).gameObject.GetComponent<MeshRenderer>().material.color);
					}
				}
			}
		}


	}
	
	/* 
	 * Mouse exited this object and is not hovering it
	 */
	public void OnPointerExit(PointerEventData data)
	{
		gs.RevertColors();
		gs.hoverList.Clear ();
		gs.mouseOverUI = false;
	}

	public void OnPointerClick(PointerEventData data)
	{

	}


}



