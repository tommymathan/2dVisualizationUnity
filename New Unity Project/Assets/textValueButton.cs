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
		gs.hoverList.Clear ();
		//Debug.Log ("This is the button row" + row);
		gs.mouseOverUI = false;
		isMouseOver = true;
		foreach (GameObject go in gs.camList) {

			if(go.transform.childCount > 1)
			{
				//gs.hoverList.Clear ();
				if(!gs.hoverList.Contains(go))
				   {

						gs.hoverList.Add(go.transform.GetChild(row+1).gameObject);
				}
			}
				}


	}
	
	/* 
	 * Mouse exited this object and is not hovering it
	 */
	public void OnPointerExit(PointerEventData data)
	{
		Debug.Log ("WE are exitiing");
		//gs.hoverList.re;
		for (int i = 0; i < gs.hoverList.Count(); i++) {
			gs.hoverList.RemoveAt(i);
				}
		gs.hoverList.Clear ();
		gs.mouseOverUI = false;
		isMouseOver = false;
				
	}

	public void OnPointerClick(PointerEventData data)
	{

	}


}



